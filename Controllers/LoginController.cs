﻿//using Microsoft.IdentityModel.Web;
using ProjektASPNET.Helpers;
using ProjektASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
//using System.IdentityModel.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjektASPNET.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginView(string registered = null)
        {
            if (registered == "true")
                @ViewBag.Message = "Zarejestrowano pomyślnie!";

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
                if (role != "NOTLOGGED")
                {
                    var ticket = new FormsAuthenticationTicket(model.Login, true, 3000);
                    string encrypt = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                    cookie.Expires = DateTime.Now.AddHours(1000);
                    cookie.HttpOnly = true;

                    Response.Cookies.Add(cookie);

                    return RedirectToAction("../Home/Index");

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
                if (register == RegisterStatus.LoginAlreadyTaken)
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

        public ActionResult LogoutView()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}