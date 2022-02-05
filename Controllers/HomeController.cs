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

        // zalogowany
        [HttpPost]
        public ActionResult Index(HomeModel model)
        {
            return View(model);
        }
    }
}