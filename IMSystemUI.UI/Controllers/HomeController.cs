using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IMSystemUI.Domain;
using IMSystemUI.UI.Helpers;

namespace IMSystemUI.UI.Controllers
{
    public class HomeController : BaseController
    {
        public List<ItemCount> ChartData { get; set; } = default!;
        private readonly IItemService _itemSrv;
        private readonly IItemEmployeeAssignmentService _itemEmployeeAssignmentSrv;

        public HomeController(IItemService itemSrv , IItemEmployeeAssignmentService itemEmployeeAssignmentSrv)
        {
            _itemSrv = itemSrv;
            _itemEmployeeAssignmentSrv = itemEmployeeAssignmentSrv;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DashboardData()
        {
            var itemCount = new List<ItemCount>();

            var data = await _itemSrv.GetAllItemsAsync(Token);

            var getSelect = data!.GroupBy(_ => _.ShelveBy!.ShelfTag)
                 .Select(g => new
                 {
                     Name = g.Key,
                     Count = g.Count()
                 })
                 .OrderByDescending(cp => cp.Count)
                 .ToList();

            foreach (var item in getSelect)
            {
                itemCount.Add(new ItemCount
                {
                    Name = item.Name,
                    Count = item.Count
                });
            }

            ChartData = itemCount;

            ViewBag.TotalNoItem = GetStats().Result.Item1;
            ViewBag.TotalNoTransfer = GetStats().Result.Item2;
            ViewBag.TotalNoMostUsedItems = GetStats().Result.Item3;
            ViewBag.TotalNoItemRepair = GetStats().Result.Item4;


            return View(ChartData);
        }

        public async Task<(int, int, int, int)> GetStats()
        {
            var item = await _itemSrv.GetAllItemsAsync(Token);
            var itemCount = item.Count();
            
            var getItemsNotReturned =  await _itemEmployeeAssignmentSrv.GetAllItemEmployeeAssignmentsAsync(Token);
            var NoItemNoReturned = getItemsNotReturned
                .Where(x => x.IsReturned == false).Count();


            var TotalNoItem = itemCount;
            var getItemsRepair = await _itemSrv.GetAllItemsAsync(Token);
            var TotalItemRepair  =  getItemsRepair.Where(x => x.DueforRepair == true).Count();

            return (TotalNoItem, NoItemNoReturned, 5, TotalItemRepair);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}