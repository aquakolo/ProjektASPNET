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

        public ActionResult OrderSeen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DB = DBHelper.GetInstance();
            OrdersModel order = null;// DB.GetOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult UserSeen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var DB = DBHelper.GetInstance();
            UserModel user = null;//DB.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
    }
}