using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace IMSystemUI.UI.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _idepartmentSrv;
        public DepartmentController(IDepartmentService idepartmentSrv)
        {
            _idepartmentSrv = idepartmentSrv;
        }

        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            var data = await _idepartmentSrv.GetAllDepartmentsAsync();

            return View(data);
        }

        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _idepartmentSrv.GetAllDepartmentAsync(id);

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
                const string msg = ResponseMessageCodes.SuccessDepartment;
                var success = ResponseMessageCodes.SuccefullDictionary[msg];

                await _idepartmentSrv.CreateDepartmentAsync(model);
                Notify(success, type: NotificationType.success);

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
            var data = await _idepartmentSrv.GetAllDepartmentAsync(id);

            return View(data);
        }

        //// POST: DepartmentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(Guid id, Department model)
        //{
        //    try
        //    {
        //        await _idepartmentSrv.UpdateAsync(id, model);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: DepartmentController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _idepartmentSrv.GetAllDepartmentAsync(id);

            return View(data);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Department model)
        {
            try
            {
                await _idepartmentSrv.RemoveDepartmentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
