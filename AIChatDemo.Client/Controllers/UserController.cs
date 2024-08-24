using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AIChatDemo.Client.DTOs;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace AIChatDemo.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notfyService;

        public UserController(HttpClient httpClient, INotyfService notfyService, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _notfyService = notfyService;
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Users/Login", model);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<UserDto>();
                    if (result != null)
                    {
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Lax,
                            Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                        };

                        Response.Cookies.Append("UserId", result.Id.ToString(), cookieOptions);
                        Response.Cookies.Append("FullName", result.FirstName+" "+result.LastName, cookieOptions);
                        _notfyService.Success("Giriş başarılı");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        _notfyService.Error("Giriş başarısız lütfen bilgileriniz kontrol ediniz.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    _notfyService.Error("Giriş başarısız lütfen bilgileriniz kontrol ediniz.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Users/CreateUser", model);
                if (response.IsSuccessStatusCode)
                {
                     _notfyService.Success("Kayıt başarılı giriş sayfasına yönlendiriliyorsunuz.");
                     return RedirectToAction("Login", "User");
                }
            }
            _notfyService.Error("Kayıt başarısız lütfen tekrar deneyin.");
            return RedirectToAction("Register", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("FullName");
            return RedirectToAction("Index", "Home");
        }
    }
}
