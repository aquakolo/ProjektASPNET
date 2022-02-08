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

        [Authorize(Roles = "ADMIN,  USER")]
        public ActionResult CartView()
        {
            var DB = DBHelper.GetInstance();
            OrderModel cart = DB.GetUserCart(DB.GetIdFromLogin(User.Identity.Name));
            return View(cart);
        }

        [Authorize(Roles = "ADMIN,  USER")]
        public ActionResult DeleteFromCartView(ProductModel product)
        {
           if (String.IsNullOrEmpty(product.Name))
            {
                var DB = DBHelper.GetInstance();
                var prod = DB.GetProduct(product.ID);
                return View(prod);
            }
            else
            {
                var DB = DBHelper.GetInstance();
                DB.removeFromCart(DB.GetIdFromLogin(User.Identity.Name), product);
                return RedirectToAction("CartView");
            }
        }

        [Authorize(Roles = "ADMIN,  USER")]
        public ActionResult AddToCartView(int id)
        {
            var DB = DBHelper.GetInstance();
            var prod = DB.GetProduct(id);
            return View(prod);
        }

        [Authorize(Roles = "ADMIN,  USER")]
        [HttpPost]
        [ActionName("AddToCartView")]
        public ActionResult AddToCartViewPost(int id)
        {
            var DB = DBHelper.GetInstance();
            var product = DB.GetProduct(id);
            DB.AddToCart(DB.GetIdFromLogin(User.Identity.Name), product); 
                                          
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN,  USER")]
        public ActionResult MakeOrderView()
        {
            var DB = DBHelper.GetInstance();
            var cart = DB.GetUserCart(DB.GetIdFromLogin(User.Identity.Name));

            return View(cart);
        }

        [Authorize(Roles = "ADMIN,  USER")]
        [HttpPost]
        public ActionResult MakeOrderView(int? blank)
        {
            var DB = DBHelper.GetInstance();
            var id = DB.GetIdFromLogin(User.Identity.Name);
            var cart = DB.GetUserCart(id);
            DB.AddOrder(cart);
            DB.ClearCart(id);


            return RedirectToAction("Index");
        }

    }
}