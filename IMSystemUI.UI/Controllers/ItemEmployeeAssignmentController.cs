using DocumentFormat.OpenXml.Math;
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
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync();

            return View(data);
        }

        // GET: ItemEmployeeAssignmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id);

            return View(data);
        }
        public async Task<ActionResult> ViewItemTransferHistory(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync();

            var query = data.Where(x => x.ItemId == id).ToList();
            ViewBag.StoreAssetId = id;

            return View(query);
        }

        public async Task<ActionResult> PrintToPdf(Guid id)
        {
            var data =
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync();

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
            var loadItems = await _itemSrv.GetAllItemsAsync();

            var loadUsers = await _userSrv.GetAllUsersAsync();

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
                //string s = "72901290-6252-4b60-b8fe-c07d9d73029c";

                //Guid.(s, r )
                //model.IssuerBy!.Id = 
                await _itemEmployeeAssignmentSrv.CreateItemEmployeeAssignmentAsync(model);

                Notify($"Successful created new item transfer to {model.ReceiverBy!.Firstname}-{model.ReceiverBy.Lastname} .", type: NotificationType.success);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                const string msg = ResponseMessageCodes.ErrorMsg;
                var errorDescription = ResponseMessageCodes.ErrorDictionary[msg];

                Notify(errorDescription, type: NotificationType.error);

                return View();
            }
        }

        // GET: ItemEmployeeAssignmentController/Edit/5
        public async Task<ActionResult> Edit(Guid id, Guid itemId)
        {
            var loadItems = await _itemSrv.GetAllItemsAsync();

            var loadUsers = await _userSrv.GetAllUsersAsync();

            var dataUser = loadUsers
                .AsQueryable()
                .Select(x => new { Value = x.Id, Text = $"{x.Firstname} - ({x.Lastname})"})
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

            var data = await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id);

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
                await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentAsync(id);

            return View(data);
        }

        // POST: ItemEmployeeAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _itemEmployeeAssignmentSrv.RemoveItemEmployeeAssignmentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
