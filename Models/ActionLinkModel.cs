using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class ActionLinkModel
    {
        public ActionLinkModel(string v1, string v2)
        {
            Text = v1;
            View = v2;
        }

        public string Text { get; set; }
        public string View { get; set; }
    }
}