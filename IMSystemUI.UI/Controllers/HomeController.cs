using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IMSystemUI.Domain;

namespace IMSystemUI.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientExtensions _client;
        public HomeController(ILogger<HomeController> logger , IHttpClientExtensions client)
        {
            _logger = logger;
            _client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DashboardData()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> DashboardData()
        //{
        //    var today = DateTime.Today;

        //    var lastWeek = today.AddDays(-6);

        //    var list = await _client.GetAllAsync<ItemEmployeeAssignment>();

        //    var data = new List<int>();

        //    for (var date = lastWeek; date <= today; date = date.AddDays(1))
        //    {
        //        var itemsTransferred = list
        //            .Where(item => item.DateTaken == date)
        //            .Count();

        //        data.Add(itemsTransferred);
        //    }

        //    return View(data);
        //}


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