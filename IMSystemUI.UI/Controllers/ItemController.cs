using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Item = IMSystemUI.Domain.Item;
using ClosedXML.Excel;

namespace IMSystemUI.UI.Controllers
{
    public class ItemController : Controller
    {
        private readonly IHttpClientExtensions _client;

        public ItemController(IHttpClientExtensions client)
        {
            _client = client;
        }

        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            var data = await _client.GetAllAsync<Item>();
            return View(data);
        }

        // GET: DepartmentController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await _client.GetByIdAsync<Item>(id);
            return View(data);
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
            worksheet.Cell(2, 1).Value = "Serialno";
            worksheet.Cell(2, 2).Value = "Name";
            worksheet.Cell(2, 3).Value = "Description";
            worksheet.Cell(2, 4).Value = "ItemTag";
            worksheet.Cell(2, 2).Value = "DueforRepair";

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

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            var model = new Item
            {
                DatePurchased = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
            };

            return View(model);
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Item model)
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
            var data = await _client.GetByIdAsync<Item>(id);
            return View(data);
        }

        // POST: DepartmentController/Edit/5
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

        // GET: DepartmentController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var data = await _client.GetByIdAsync<Item>(id);
            return View(data);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
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
