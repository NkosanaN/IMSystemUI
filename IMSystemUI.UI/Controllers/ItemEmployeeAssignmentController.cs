using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace IMSystemUI.UI.Controllers
{
    public class ItemEmployeeAssignmentController : BaseController
    {
        private readonly IItemEmployeeAssignmentService _itemEmployeeAssignmentSrv;
        private readonly IItemService _itemSrv;
        private readonly IUserService _userSrv;

        public IEnumerable<SelectListItem>? ItemsList { get; set; }
        public IEnumerable<SelectListItem>? UserList { get; set; }

        public ItemEmployeeAssignmentController(IItemEmployeeAssignmentService itemEmployeeAssignmentSrv, IItemService itemSrv, IUserService userSrv)
        {
            _itemEmployeeAssignmentSrv = itemEmployeeAssignmentSrv;
            _itemSrv = itemSrv;
            _userSrv = userSrv;
        }

        // GET: ItemEmployeeAssignmentController
        public async Task<ActionResult> Index()
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync(Token);

            return View(data);
        }

        // GET: ItemEmployeeAssignmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id, Token);

            return View(data);
        }
        public async Task<ActionResult> ViewItemTransferHistory(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync(Token);

            var query = data.Where(x => x.ItemId == id).ToList();
            ViewBag.StoreAssetId = id;
            return View(query);
        }
        public async Task<ActionResult> PrintToPdfAfterCreateITransfer(Guid item, Guid getITransferId)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync(Token);

            var query = data.Where(x => x.ItemId == item && x.AssigmentId == getITransferId).ToList();

            var potrait = new ViewAsPdf(query)
            {
                FileName = "itemtransfer.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
            return potrait;
        }

        public async Task<ActionResult> PrintToPdf(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync(Token);

            var query = data.Where(x => x.ItemId == id).ToList();

            var potrait = new ViewAsPdf(query)
            {
                FileName = "itemtransfer.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
            return potrait;
        }
        // GET: ItemEmployeeAssignmentController/Create
        public async Task<ActionResult> Create()
        {
            var loadItems = await _itemSrv.GetAllItemsAsync(Token);

            var loadUsers = await _userSrv.GetAllUsersAsync(Token);

            var dataUser = loadUsers
                .AsQueryable()
                .Select(x => new { Value = x.Id, Text = $"{x.Firstname} - ({x.Lastname})" })
                .ToList();

            var dataItem = loadItems
                .AsQueryable()
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

            var model = new ItemEmployeeAssignment
            {
                DateTaken = DateTime.Now,
                DateReturned = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                IsReturned = false
            };

            return View(model);
        }

        // POST: ItemEmployeeAssignmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemEmployeeAssignment model)
        {
            try
            {
                //should we decrease Qty if we perform iTransfer
                //oh all the items are unique  even tho they might belong in one group
                //i.e hammers will deffer with serial no :  xx-11
                //..........................................xx-22
                //using this method will help us to track extract item which is in repair 

                model.IssuerById = CreatedById;
                model.ReceiverById = model.ReceiverById;

              var getITransferId = await _itemEmployeeAssignmentSrv.CreateItemEmployeeAssignmentAsync(model, Token);

                Notify("iTransfer","Successful created new item transfer .", type: NotificationType.success);

                //await PrintToPdfAfterCreateITransfer(model.ItemId, getITransferId.ItemId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                const string msg = ResponseMessageCodes.ErrorMsg;
                var errorDescription = ResponseMessageCodes.ErrorDictionary[msg];

                Notify("iTransfer",errorDescription, type: NotificationType.error);
            }

            var loadItems = await _itemSrv.GetAllItemsAsync(Token);

            var loadUsers = await _userSrv.GetAllUsersAsync(Token);

            var dataUser = loadUsers
                .AsQueryable()
                .Select(x => new { Value = x.Id, Text = $"{x.Firstname} - ({x.Lastname})" })
                .ToList();

            var dataItem = loadItems
                .AsQueryable()
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

        // GET: ItemEmployeeAssignmentController/Edit/5
        public async Task<ActionResult> Edit(Guid id, Guid itemId)
        {
            var data = await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id, Token);

            if (!data.DateReturned.HasValue) data.DateReturned = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            var loadItems = await _itemSrv.GetAllItemsAsync(Token);

            var loadUsers = await _userSrv.GetAllUsersAsync(Token);

            var dataUser = loadUsers
                .AsQueryable()
                .Select(x => new { Value = x.Id, Text = $"{x.Firstname} - ({x.Lastname})" })
                .ToList();

            var dataItem = loadItems
                .AsQueryable()
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
            
            return View(data);
        }

        // POST: ItemEmployeeAssignmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemEmployeeAssignmentController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id, Token);

            return View(data);
        }

        // POST: ItemEmployeeAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _itemEmployeeAssignmentSrv.RemoveItemEmployeeAssignmentAsync(id, Token);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ReturnItem(Guid id, string note)
        {
            var data = await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id, Token);

            data.IssuerById = data.IssuerBy!.Id.ToString();
            data.ReceiverById = data.ReceiverBy!.Id.ToString();
            data.IsReturned = true;
            data.Condition = note;

            var r = await _itemEmployeeAssignmentSrv.ReturnItem(data, Token);

            Notify("iItem", "Item  has been booked for Repair .", type: NotificationType.info);

            return Ok(new { success = r });
        }
    }
}
