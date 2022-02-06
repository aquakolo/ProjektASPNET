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
        private string ConvertPriceToString(long price)
        {
            var str = price.ToString();
            return str.Substring(0, str.Length - 2) + "," + str.Substring(str.Length - 2, 2) + "zł";
        }
        private string CreateProductsTable(bool isAuthorized)
        {
            var elemList = DBHelper.GetInstance().GetProducts();

            var tableBuilder = new HtmlTableBuilder();
            if (isAuthorized)
                tableBuilder.setHeader(new List<string>{"Nazwa", "Opis", "Cena", "Zobacz", "Dodaj do koszyka"});
            else
                tableBuilder.setHeader(new List<string> { "Nazwa", "Opis", "Cena", "Zobacz" });

            foreach (var elem in elemList)
            {
                var rowBulder = new HTMLTableRowBuilder();
                rowBulder.addText(elem.Name);
                rowBulder.addText(elem.Description);
                rowBulder.addText(ConvertPriceToString(elem.Price));
                rowBulder.addButton("Zobacz", "zobaczButton", elem.ID.ToString());
                if (isAuthorized)
                    rowBulder.addButton("Dodaj do koszyka", "dodajDoKoszykaButton", elem.ID.ToString());

                tableBuilder.addRow(rowBulder);
            }

            var str = tableBuilder.create();
            return str;
        }
        // niezalogowany
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.UserRole = "NotLogged";

            string table = CreateProductsTable(false);

            model.HtmlTable = table;
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