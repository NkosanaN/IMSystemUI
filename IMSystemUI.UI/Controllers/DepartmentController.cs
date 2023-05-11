using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMSystemUI.UI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHttpClientExtensions _client;
        public DepartmentController(IHttpClientExtensions client)
        {
            _client = client;
        }

        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            var data = await _client.GetAllAsync<Department>();
            return View(data);
        }

        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _client.GetByIdAsync<Department>(id);
            return View(data);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department model)
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

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await _client.GetByIdAsync<Department>(id);
            return View(data);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Department model)
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

        // GET: DepartmentController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _client.GetByIdAsync<Department>(id);
            return View(data);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Department model)
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
