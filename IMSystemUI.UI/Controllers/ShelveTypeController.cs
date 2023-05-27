using DocumentFormat.OpenXml.Math;
using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace IMSystemUI.UI.Controllers
{
    public class ShelveTypeController : BaseController
    {
        private readonly IShelveTypeService _shelvetypeSrv;
        public ShelveTypeController(IShelveTypeService shelvetypeSrv)
        {
            _shelvetypeSrv = shelvetypeSrv;
        }

        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            var data = await _shelvetypeSrv.GetAllShelveTypesAsync();

            return View(data);
        }

        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _shelvetypeSrv.GetAllShelveTypeAsync(id);

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
        public async Task<ActionResult> Create(ShelveType model)
        {
            try
            {
                await _shelvetypeSrv.CreateShelveTypeAsync(model);

                Notify("shelve info .", "Successful created new shelve type .", type: NotificationType.success);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                const string msg = ResponseMessageCodes.ErrorMsg;
                var errorDescription = ResponseMessageCodes.ErrorDictionary[msg];

                Notify(errorDescription, type: NotificationType.error);

                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await _shelvetypeSrv.GetAllShelveTypeAsync(id);
            return View(data);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid ShelfId, ShelveType model)
        {
            try
            {
                await _shelvetypeSrv.UpdateShelveTypeAsync(ShelfId, model);

                Notify("shelve info .", "Successful updated shelve type.", type: NotificationType.info);

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
            var data = await _shelvetypeSrv.GetAllShelveTypeAsync(id);
            return View(data);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, ShelveType model)
        {
            try
            {
                await _shelvetypeSrv.RemoveShelveTypeAsync(id);

                Notify("shelve info .", "Successful removed shelve type.", type: NotificationType.info);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
