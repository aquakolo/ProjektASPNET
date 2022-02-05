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
    //[AdminAutorizationFilter]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult OrdersView()
        {
            var DB = DBHelper.GetInstance();
            var OrdersList = DB.GetOrders();
            return View(OrdersList);
        }

        public ActionResult CartsView()
        {
            var DB = DBHelper.GetInstance();
            var CartsList = new List<OrdersModel>();//DB.GetCarts();
            return View(CartsList);
        }

        public ActionResult UsersView()
        {
            var DB = DBHelper.GetInstance();
            var UsersList = DB.GetUsers();
            return View(UsersList);
        }

        public ActionResult ProductsView()
        {
            var DB = DBHelper.GetInstance();
            var ProductsList = DB.GetProducts();
            return View(ProductsList);
        }

        public ActionResult ProductSeen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DB = DBHelper.GetInstance();
            ProductModel product = null;// DB.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ProductEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DB = DBHelper.GetInstance();
            ProductModel product = null;// DB.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult ProductEdit(ProductModel product)
        {
            var DB = DBHelper.GetInstance();
            //DB.DeleteProduct(product.ID)
            //product.ID = DB.GetNewID();
            //DB.AddProduct(product);
            return View(product);
        }

        public ActionResult ProductDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DB = DBHelper.GetInstance();
            ProductModel product = null;// DB.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("ProductDelete")]
        public ActionResult ProductDeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DB = DBHelper.GetInstance();
            //DB.DeleteProduct(product.ID)
            return RedirectToAction("ProductsView");
        }

        public ActionResult ProductAdd()
        {
            var DB = DBHelper.GetInstance();
            ProductModel product = new ProductModel();
            product.ID = 0;//DB.GetNewID();
            return View(product);
        }

        [HttpPost]
        public ActionResult ProductAdd(ProductModel product)
        {
            var DB = DBHelper.GetInstance();
            //DB.AddProduct(product);
            return RedirectToAction("ProductsView");
        }
    }
}