using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Helpers
{
    public class HTMLTableRowBuilder
    {
        public void addText(string text, int maxLength=50)
        {
            if (text.Length >= maxLength - 3)
            {
                text = text.Substring(0, maxLength - 3) + "...";
            }
            _elements.Add(text);
        }

        public void addButton(string text, string name, string value, int maxLength=25)
        {
            if (text.Length >= maxLength - 3)
            {
                text = text.Substring(0, maxLength - 3) + "...";
            }
            _elements.Add(String.Format("<button name='{0}' value='value'>{2}</button>", name, value, text));
        }

        public string create()
        {
            string toReturn = "";
            if (_elements.Count == 0)
                return toReturn;

            toReturn += String.Format("<tr> <th> {0} </th>", _elements[0]);
            _elements.RemoveAt(0);
            foreach (var element in _elements)
            {
                toReturn += String.Format("<td> {0} </td>", element);
            }

            toReturn += "</tr>";
            return toReturn;
        }

        private List<string> _elements = new List<string>();
    };
    public class HtmlTableBuilder
    {
        public void setHeader(List<string> headerElements)
        {
            _headerElements = headerElements;
        }

        public void addRow(HTMLTableRowBuilder rowBuilder)
        {
            _rows.Add(rowBuilder);
        }
        public string create()
        {
            string toReturn = "<table><thead> ";
            toReturn += String.Format("<tr> <th> {0} </th>", _headerElements[0]);
            _headerElements.RemoveAt(0);
            foreach (var element in _headerElements)
            {
                toReturn += String.Format("<th> {0} </th>", element);
            }

            toReturn += "<tr/></thead><tbody>";
            toReturn += _rows[0].create();
            _rows.RemoveAt(0);
            foreach (var element in _rows)
            {
                toReturn += element.create();
            }
            toReturn += "</tbody></table>";
            return toReturn;
        }

        private List<string> _headerElements = new List<string>();
        private List<HTMLTableRowBuilder> _rows = new List<HTMLTableRowBuilder>();
    }
}