using ProjektASPNET.Helpers;
using ProjektASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektASPNET.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginView(string registered = null)
        {
            if (registered == "true") 
                @ViewBag.Message = "Zarejestrowano pomyślnie";

            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult LoginView(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var DB = DBHelper.GetInstance();
                string role = DB.GetUserRole(model); // Helpers
                if (role != "NotLogged")
                {
                    HomeModel user = new HomeModel{ ID = 123, UserRole = role };
                    // ciasteczko user musi być przekazany w ciastku
                    return RedirectToAction("../Home/Index"); // return tam, skąd przyszli bądź do strony głównej

                }

                ModelState.AddModelError("", "Nieprawidłowe hasło lub login. Proszę, spróbuj ponownie.");
            }
            return View(model);
        }

        //GET : Register
        public ActionResult RegisterView()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterView(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var DB = DBHelper.GetInstance();
                var register = DB.RegisterUser(model);
                if (register == RegisterStatus.OK)
                {
                    return RedirectToAction("LoginView", new { registered = "true" });
                }
                if(register == RegisterStatus.LoginAlreadyTaken)
                {
                    ModelState.AddModelError("", "Taki login już istnieje.");
                }
                if (register == RegisterStatus.LoginOrPasswordTooLong)
                {
                    ModelState.AddModelError("", "Login lub hasło jest za długie.");
                }
                if (register == RegisterStatus.PasswordsNotEqual)
                {
                    ModelState.AddModelError("", "Hasła się nie zgadzają.");
                }
            }
            return View(model);
        }
    }
}