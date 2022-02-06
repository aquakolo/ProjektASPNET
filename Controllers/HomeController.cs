using ProjektASPNET.Helpers;
using ProjektASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjektASPNET.Controllers
{
    public class HomeController : Controller
    {
        // niezalogowany
        public ActionResult Index()
        {
            List<ProductModel> list = DBHelper.GetInstance().GetProducts();
            var model = new HomeModel{ Search = "", Products = list};
            return View(model);
        }

        // zalogowany
        //[Authorize(Roles = "ADMIN,  USER")]
        [HttpPost]
        public ActionResult Index(HomeModel model)
        {
            List<ProductModel> list = DBHelper.GetInstance().GetProducts();
            if (String.IsNullOrEmpty(model.Search))
            {
                model.Products = list;
                return View(model);
            }
            model.Products = DBHelper.GetInstance().GetProducts(model.Search);
            return View(model);
        }

        public ActionResult ProductSeen(int productId)
        {
            //@ViewBag.LastAction = "";
            var DB = DBHelper.GetInstance();
            ProductModel product = DB.GetProduct(productId);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //[Authorize(Roles = "ADMIN,  USER")]
        public ActionResult CartView()
        {
            //@ViewBag.LastAction = "";
            //var SessionHelper = ProjektASPNET.Helpers.SessionHelper.GetInstance(); 
            var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
            OrderModel cart = DB.GetUserCart(0); // tu będzie inne niż 0 
            return View(cart);
        }

       // [Authorize(Roles = "ADMIN,  USER")]
        public ActionResult DeleteFromCartView(ProductModel product)
        {
           if (String.IsNullOrEmpty(product.Name))
            {
                var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
                var prod = DB.GetProduct(product.ID);
                return View(prod);
            }
            else
            {
                var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
                DB.removeFromCart(0, product);
                return RedirectToAction("CartView");
            }
        }

        //[Authorize(Roles = "ADMIN,  USER")]
        public ActionResult AddToCartView(int id)
        {
            //@ViewBag.LastAction = "";
            var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
            var prod = DB.GetProduct(id);
            return View(prod);
        }

        //[Authorize(Roles = "ADMIN,  USER")]
        [HttpPost]
        [ActionName("AddToCartView")]
        public ActionResult AddToCartViewPost(int id)
        {
            var DB = ProjektASPNET.Helpers.DBHelper.GetInstance();
            var product = DB.GetProduct(id);
            DB.AddToCart(0, product); // tu też będzie inne niż 0
                                          //@ViewBag.LastAction = "Dodano produkt do koszyka pomyślnie";
                return RedirectToAction("Index");
        }
    }
}