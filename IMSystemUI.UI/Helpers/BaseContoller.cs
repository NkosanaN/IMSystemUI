using IMSystemUI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMSystemUI.UI.Helpers
{
    public class BaseController : Controller
    {
        public enum NotificationType { error, success, warning, info }

        public void Notify(string message,
            string title = "Sweet Alert Toastr Demo",
            NotificationType type = NotificationType.success)
        {
            var ms = new
            {
                msg = message,
                title = title,
                icon = type.ToString(),
                type = type.ToString(),
                provider = GetProvider()
            };
            TempData["Message"] = JsonConvert.SerializeObject(ms);
        }

        public string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            var value = configuration["NotificationProvider"];
            return value;
        }

        //public string Token => !string.IsNullOrEmpty(HttpContext.Session.GetString("SessionToken"))
        //    ? HttpContext.Session.GetString("SessionToken")
        //    : Constant.token;
        public string Token => Constant.token;
        public string CreatedById => HttpContext.Session.GetString("SessionName");
        

    }
}
