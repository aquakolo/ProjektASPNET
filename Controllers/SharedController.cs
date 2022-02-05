using ProjektASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektASPNET.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public ActionResult Header()
        {
            //var Helper = Helpers.Helper.GetInstance();
            List<ActionLinkModel> list = new List<ActionLinkModel>();
            list.Add(new ActionLinkModel("Strona główna", "../Home/Index"));
            //if(Helper.GetUserRole()=="Admin")
            //{
            //  list.Add(new ActionLinkModel("Lista zamówień", "../Admin/OrdersView"));
            //  list.Add(new ActionLinkModel("Lista użytkowników", "../Admin/UsersView"));
            //  list.Add(new ActionLinkModel("Lista produktów", "../Admin/ProductsView"));
            //  list.Add(new ActionLinkModel("Lista koszyków", "../Admin/CartsView"));
            //}
            //if()//niezalogowny
            //{
            // list.Add(new ActionLinkModel("Logowanie", "../Login/LoginView"));
            // list.Add(new ActionLinkModel("Rejestracja", "../Login/LoginView"));
            //}

            return View();
        }
    }
}