using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace IMSystemUI.UI.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IHttpClientExtensions _client;
        public SupplierController(IHttpClientExtensions client)
        {
            _client = client;
        }

        // GET: SupplierController
        public async Task<ActionResult> Index()
        {
            var data = await _client.GetAllAsync<Supplier>();
            return View(data);
        }

        // GET: SupplierController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _client.GetByIdAsync<Supplier>(id);
            return View(data);
        }

        // GET: SupplierController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Supplier model)
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

        // GET: SupplierController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await _client.GetByIdAsync<Supplier>(id);
            return View(data);
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Supplier model)
        {
            try
            {
                await _client.UpdateAsync(id, model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _client.GetByIdAsync<Supplier>(id);
            return View(data);
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Supplier model)
        {
            try
            {
                await _client.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
