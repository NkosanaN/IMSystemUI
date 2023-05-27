using DocumentFormat.OpenXml.Office2010.Excel;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;
using IMSystemUI.Domain;
using IMSystemUI.UI.Helpers;

namespace IMSystemUI.UI.Controllers
{
    public class HomeController : BaseController
    {
        public List<ItemCount> ChartData { get; set; } = default!;
        public const string SessionKeyName = "_id";
        private readonly IItemService _itemSrv;


        //  private readonly IItemService _itemSrv;

        public HomeController(IItemService itemSrv)
        {
            _itemSrv = itemSrv;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DashboardData()
        {

            Notify("Successful created order", "Successful created order", type: NotificationType.success);


            var itemCount = new List<ItemCount>();

            var data = await _itemSrv.GetAllItemsAsync();

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

            //var id =   HttpContext.Session.GetString(SessionKeyName);


            return View(ChartData);
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