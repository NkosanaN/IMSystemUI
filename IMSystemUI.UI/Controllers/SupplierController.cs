using DocumentFormat.OpenXml.EMMA;
using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace IMSystemUI.UI.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierSrv;
        public SupplierController(ISupplierService supplierSrv)
        {
            _supplierSrv = supplierSrv;
        }

        // GET: SupplierController
        public async Task<ActionResult> Index()
        {
            var data = await _supplierSrv.GetAllSupplierAsync(Token);
            return View(data);
        }

        // GET: SupplierController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _supplierSrv.GetSupplierAsync(id, Token);

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
                await _supplierSrv.CreateSupplierAsync(model, Token);

                Notify("Successful created new supplier .", type: NotificationType.success);

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

        // GET: SupplierController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await _supplierSrv.GetSupplierAsync(id, Token);



            return View(data);
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid SupplierId, Supplier model)
        {
            try
            {
                await _supplierSrv.UpdateSupplierAsync(SupplierId, model, Token);

                Notify("supplier info .", "Successful updated supplier.", type: NotificationType.info);

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

        // GET: SupplierController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _supplierSrv.GetSupplierAsync(id, Token);

            return View(data);
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Supplier model)
        {
            try
            {
                await _supplierSrv.RemoveSupplierAsync(id, Token);
                
                Notify("Successful removed supplier .", type: NotificationType.info);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
