using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Item = IMSystemUI.Domain.Item;
using ClosedXML.Excel;
using IMSystemUI.UI.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using IMSystemUI.Domain;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.EMMA;

namespace IMSystemUI.UI.Controllers
{
    public class ItemController : BaseController
    {
        private IEnumerable<SelectListItem>? ItemsList { get; set; }
        private IEnumerable<SelectListItem>? UserList { get; set; }
        private IEnumerable<SelectListItem>? ShelveList { get; set; }

        //public const string SessionKeyName = "_shelveType";

        private readonly IItemService _itemSrv;
        private readonly ISupplierService _supplierSrv;
        private readonly IUserService _userSrv;
        private readonly IShelveTypeService _shelvetypeSrv;

        public ItemController(
            IItemService itemSrv,
            ISupplierService supplierSrv,
            IUserService userSrv,
            IShelveTypeService shelvetypeSrv)
        {
            _itemSrv = itemSrv;
            _supplierSrv = supplierSrv;
            _userSrv = userSrv;
            _shelvetypeSrv = shelvetypeSrv;
        }

        // GET: ItemController
        public async Task<ActionResult> Index()
        {
            var data = await _itemSrv.GetAllItemsAsync(Token);
            return View(data);
        }

        // GET: ItemController
        public async Task<ActionResult> ItemLinked(Guid shelfId, string shalftag)
        {
            var data = await _itemSrv.GetAllItemsAsync(Token);

            var linkedItem = data.Where(x => x.ShelveBy!.ShelfId == shelfId).ToList();

            ViewBag.shalftag = shalftag;

            return View(linkedItem);
        }

        // GET: ItemController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _itemSrv.GetAllItemAsync(id, Token);

            return View(data);
        }

        public async Task<ActionResult> BookingForRepair(Guid id)
        {
            var loadItems = await _itemSrv.GetAllItemsAsync(Token);

            // var loadSupplier = await _supplierSrv.GetAllSupplierAsync();

            var loadUsers = await _userSrv.GetAllUsersAsync(Token);

            var dataUser = loadUsers
                .AsQueryable()
                .Select(x => new { Value = x.Id, Text = x.DisplayName })
                .ToList();

            var dataItem = loadItems
                .AsQueryable()
                .Where(c => c.ItemId == id)
                .Select(x => new { Value = x.ItemId, Text = x.Name })
                .ToList();

            UserList = dataUser.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            ItemsList = dataItem.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            ViewBag.issuerList = UserList;
            ViewBag.itemList = ItemsList;

            return View();
        }

        public async Task<ActionResult> DownloadExcelTemplate()
        {
            // Create a new workbook
            var workbook = new XLWorkbook();

            // Add a new worksheet to the workbook
            var worksheet = workbook.Worksheets.Add("Template");

            // Set the column headers
            worksheet.Cell(1, 1).Value = "Default Item Template Please don't Remove any Title Cell";
            worksheet.Range(1, 1, 1, 3).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Merge the first row across all columns and center the text
            worksheet.Range(1, 1, 1, 3).Style.Font.FontColor = XLColor.Red; // Set the font color of the merged cell to red
            worksheet.Cell(2, 1).Value = "Serial no";
            worksheet.Cell(2, 2).Value = "Name";
            worksheet.Cell(2, 3).Value = "Description";
            worksheet.Cell(2, 4).Value = "ItemTag";
            worksheet.Cell(2, 2).Value = "Due for Repair";

            // Auto-fit the columns
            worksheet.Columns().AdjustToContents();

            // Save the workbook to a temporary file
            //var tempFilePath = Path.GetTempFileName();
            var tempFilePath = Path.GetTempFileName() + ".xlsx";

            workbook.SaveAs(tempFilePath);

            // Read the file into a byte array
            var data = System.IO.File.ReadAllBytes(tempFilePath);

            // Set the content type and file name
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment; filename=Template.xlsx");

            // Return the Excel file as a download
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Template.xlsx");
        }

        // GET: AccountController/Create
        public async Task<ActionResult> Create()
        {
            var loadShelves = await _shelvetypeSrv.GetAllShelveTypesAsync(Token);

            var loadUsers = await _userSrv.GetAllUsersAsync(Token);

            var dataShelves = loadShelves
                .AsQueryable()
                .Select(x => new { Value = x.ShelfId, Text = x.ShelfTag })
                .ToList();

            var dataUser = loadUsers
                .AsQueryable()
                //.Select(x => new { Value = x.Id, Text = $"{x.DisplayName} - ({x.Lastname})" })
                .Select(x => new { Value = x.Id, Text = $"{x.DisplayName}" })
                .ToList();

            ShelveList = dataShelves.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            UserList = dataUser.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            var model = new Item
            {
                DatePurchased = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
            };

            ViewBag.createdById = UserList;

            ViewBag.shelveList = ShelveList;

            return View(model);
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item model)
        {
            try
            {
                //Guid.TryParse(CreatedById, out Guid CreatedByid);
                model.CreatedById = model.CreatedBy!.Id;
                model.ItemId = Guid.NewGuid();
                model.ItemTag = "Empty";
                model.ShelfId = model.ShelveBy!.ShelfId;
                await _itemSrv.CreateItemAsync(model, Token);

                Notify("Item", "Successful Add item.", type: NotificationType.success);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                const string msg = ResponseMessageCodes.ErrorMsg;
                var errorDescription = ResponseMessageCodes.ErrorDictionary[msg];

                Notify("Item", errorDescription, type: NotificationType.error);
            }

            var loadShelves = await _shelvetypeSrv.GetAllShelveTypesAsync(Token);

            var loadUsers = await _userSrv.GetAllUsersAsync(Token);

            var dataShelves = loadShelves
                .AsQueryable()
                .Select(x => new { Value = x.ShelfId, Text = x.ShelfTag })
                .ToList();

            var dataUser = loadUsers
               .AsQueryable()
               //.Select(x => new { Value = x.Id, Text = $"{x.DisplayName} - ({x.Lastname})" })
               .Select(x => new { Value = x.Id, Text = $"{x.DisplayName}" })
               .ToList();

            ShelveList = dataShelves.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            UserList = dataUser.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            ViewBag.createdById = UserList;
            ViewBag.shelveList = ShelveList;

            return View();
        }

        public async Task<ActionResult> BookRepair(Guid id, string note)
        {
            var data = await _itemSrv.GetAllItemAsync(id, Token);

            //data.DueforRepair = true;
            //data.RepairMessage = note;

            var r = await _itemSrv.BookRepair(data, Token);

            Notify("Item", $"Item [{data.Name}] has been booked for Repair.", type: NotificationType.info);

            return Ok(new { success  = r });
        }

        // GET: ItemController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var loadShelves = await _shelvetypeSrv.GetAllShelveTypesAsync(Token);

            var dataShelves = loadShelves
                .AsQueryable()
                .Select(x => new { Value = x.ShelfId, Text = x.ShelfTag })
                .ToList();

            ShelveList = dataShelves.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            ViewBag.shelveList = ShelveList;

            var data = await _itemSrv.GetAllItemAsync(id, Token);
            return View(data);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid ItemId, Item item)
        {
            try
            {
                item.ShelfId = item.ShelveBy.ShelfId;

                await _itemSrv.UpdateItemAsync(ItemId, item, Token);

                Notify("Item", "Successful Updated item.", type: NotificationType.info);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                const string msg = ResponseMessageCodes.ErrorMsg;
                var errorDescription = ResponseMessageCodes.ErrorDictionary[msg];

                Notify("Item", errorDescription, type: NotificationType.error);
            }

            return View();

        }

        // GET: ItemController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _itemSrv.GetAllItemAsync(id, Token);
            return View(data);
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid ItemId, IFormCollection collection)
        {
            try
            {
                await _itemSrv.RemoveItemAsync(ItemId, Token);
                Notify("Item", "Successful Delete item.", type: NotificationType.info);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
