using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace IMSystemUI.UI.Controllers
{
    public class ItemEmployeeAssignmentController : Controller
    {
        private readonly IHttpClientExtensions _client;
        public IEnumerable<SelectListItem> ItemsList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
        public ItemEmployeeAssignmentController(IHttpClientExtensions client)
        {
            _client = client;
        }

        // GET: ItemEmployeeAssignmentController
        public async Task<ActionResult> Index()
        {
            var data = await _client.GetAllAsync<ItemEmployeeAssignment>();
            return View(data);
        }

        // GET: ItemEmployeeAssignmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
           var data = await _client.GetByIdAsync<ItemEmployeeAssignment>(id);
            return View(data);
        }
        public async Task<ActionResult> ViewItemTransferHistory(Guid id)
        {
            var data = await _client.GetAllAsync<ItemEmployeeAssignment>();
            var query = data.Where(x => x.ItemId == id).ToList();
            ViewBag.StoreAssetId = id;
            return View(query);
        }

        public async Task<ActionResult> PrintToPdf(Guid id)
        {
            var data = await _client.GetAllAsync<ItemEmployeeAssignment>();
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
           var loadItems = await  _client.GetAllAsync<Item>();
           
           //var loadUsers = await _client.GetAllAsync<User>();

           // var dataUser = loadUsers.AsQueryable().Select(x => new { Value = x.Id, Text = x.DisplayName }).ToList();
            var dataItem = loadItems.AsQueryable().Select(x => new { Value = x.ItemId , Text = x.Name }).ToList();

            //UserList = dataUser.Select(i => new SelectListItem
            //{
            //    Text = i.Text,
            //    Value = i.Value!.ToString()
            //});

            ItemsList = dataItem.Select(i => new SelectListItem
            {
                Text = i.Text,
                Value = i.Value!.ToString()
            });

            // ViewBag.issuerList = UserList;
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
                await _client.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemEmployeeAssignmentController/Edit/5
        public async Task<ActionResult> Edit(Guid id , Guid itemId)
        {
            //var loadItems = await _client.GetAllAsync<Item>();

            //var dataItem = loadItems
            //    .AsQueryable()
            //    .Where(x=>x.ItemId == itemId)
            //    .Select(x => new { Value = x.ItemId, Text = x.Name })
            //    .ToList();

            //ItemsList = dataItem.Select(i => new SelectListItem
            //{
            //    Text = i.Text,
            //    Value = i.Value!.ToString()
            //});

            // ViewBag.issuerList = UserList;
            ViewBag.itemList = ItemsList;


            var data = await _client.GetByIdAsync<ItemEmployeeAssignment>(id);
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemEmployeeAssignmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
