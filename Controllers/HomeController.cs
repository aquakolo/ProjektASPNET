using ProjektASPNET.Helpers;
using ProjektASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektASPNET.Controllers
{
    public class HomeController : Controller
    {
        // niezalogowany
        public ActionResult Index()
        {
            List<ProductModel> model = DBHelper.GetInstance().GetProducts();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int productId)
        {
            return View(model);
        }

        // zalogowany
        [HttpPost]
        public ActionResult Index(HomeModel model)
        {
            return View(model);
        }

        public ActionResult CartView()
        {
            //var SessionHelper = ProjektASPNET.Helpers.SessionHelper.GetInstance(); 
            var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
            var cart = new OrdersModel();// DB.GetCart(SessionHelper.GetUserID());
            return View(cart);
        }

        public ActionResult DeleteFromCartView(int productID)
        { 
            var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
            var product = new ProductModel();//DB.GetProduct(productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteFromCartView(ProductModel model)
        {
            //var SessionHelper = ProjektASPNET.Helpers.SessionHelper.GetInstance();
            // usuwanie z bazy danych

            return RedirectToAction("CartView");
        }
    }
}