using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using IMSystemUI.UI.Helpers;

namespace IMSystemUI.UI.Controllers
{
    //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-7.0
    public class AccountController : BaseController
    {
        //private readonly HttpClient _httpClient;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public const string SessionKeyName = "_id";

        //public AccountController(/*IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor*/)
        //{
        //    //_httpClient = httpClientFactory.CreateClient();
        //    //_httpContextAccessor = httpContextAccessor;
        //}
        private readonly IUserService _userSrv;
        public AccountController(IUserService userSrv)
        {
            _userSrv = userSrv;
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login user)
        {
            //var apiBaseUrl = _configuration.GetValue<string>("ApiBaseUrl");
            //var apiEndpoint = _configuration.GetValue<string>("ApiEndpoint");

            //var apiBaseUrl = "http://localhost:5293/api";
            //var apiEndpoint = "Account/login";

            //// Call the API to authenticate the user and get a JWT token
            //var credentials = new { email = user.Email, password = user.Password };
            //var credentialsJson = JsonConvert.SerializeObject(credentials);
            //var content = new StringContent(credentialsJson, Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync($"{apiBaseUrl}/{apiEndpoint}", content);
            //var responseJson = await response.Content.ReadAsStringAsync();
            //var json = (JObject)JsonConvert.DeserializeObject(responseJson);

            //if (json!["token"] == null)
            //{
            //    ModelState.AddModelError("", "Invalid credentials");
            //    return View();
            //}

            //var token = json["token"]!.ToString();

            //if (string.IsNullOrEmpty(token))
            //{
            //    ModelState.AddModelError("", "Invalid credentials");
            //    return View();
            //}

            //// Decrypt the JWT token to authenticate the user
            //var handler = new JwtSecurityTokenHandler();
            //var jwt = handler.ReadJwtToken(token);
            //var claims = jwt.Claims;
            //var identity = new ClaimsIdentity(claims, "JWT");

            ////_httpContextAccessor.HttpContext!.Request.Headers.Add("Authorization", $"Bearer {token}");

            //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            //var authProperties = new AuthenticationProperties
            //{
            //    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            //    IsPersistent = true,
            //    IssuedUtc = DateTimeOffset.UtcNow
            //};

            //HttpContext!.Session.SetString("token", token);
            //HttpContext.Session.SetString(SessionKeyName, identity.Claims.ElementAt(1).Value.ToString());
            return RedirectToAction("DashboardData", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(Register user)
        {
            try
            {
                user.Username = user.Email;
                await _userSrv.RegisterUser(user);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

          
            return View("Msg");
        }
    }
}
