using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MLGroupShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if ((username == "admin" && password == "123") ||
     (username == "user" && password == "111"))
            {
                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role,
            username == "admin" ? "Admin" : "User")
    };

                var identity = new ClaimsIdentity(
                    claims,
                    "CookieAuth");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                 "CookieAuth",
                  principal,
                  new AuthenticationProperties
                  {
                        IsPersistent = false
                   });

                if (username == "admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }
            }

            ViewBag.Error = "Неверный логин или пароль";

            return View();
        }

       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");

            return RedirectToAction("Login");
        }
    }
}