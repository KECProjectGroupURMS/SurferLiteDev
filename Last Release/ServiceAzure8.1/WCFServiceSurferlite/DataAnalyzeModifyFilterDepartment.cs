using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HtmlAgilityPack;
using System.Text;

using System.Xml;
using System.Xml.XPath;

namespace WCFServiceSurferlite
{
    public class DataAnalyzeModifyFilterDepartment
    {
        private HtmlDocument doc;
        private byte[] changedByte;
        internal byte[] ModifiedByte
        {
            get
            {
                return changedByte;
            }
            set
            {
                changedByte = value;
            }
        }

        public CompressorDepartment CompressorDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void RemoveImage() { }
        public void RemoveHrefs(byte[] receivedBytes) {

            string html = System.Text.Encoding.Default.GetString(receivedBytes);

            //string html = "<h1>Test</h1><script>dos omet hin ghere.</script><!-- This is a test -->";

            doc = new HtmlDocument();
            doc.LoadHtml(html);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                if (link.Attributes["onclick"] == null) {
                    link.Attributes.Add("onclick", "window.external.notify('" + att.Value + "')");
                }
                att.Value = "#";
            }

            //var urls = doc.DocumentNode.Descendants("img")
            //                    .Select(e => e.SetAttributeValue("src", "#"));

            //foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//img[@src]"))
            //{
            //    HtmlAttribute att = link.Attributes["src"];
            //    att.Value = "#";
            //}

            string newHtml = doc.DocumentNode.OuterHtml;
            ModifiedByte = Encoding.ASCII.GetBytes(newHtml);
        }
        public void ChangeHtml() { }
        public void RemoveScriptsStyleComment(byte[] receivedBytes)
        {
            string html = System.Text.Encoding.Default.GetString(receivedBytes);

            
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            doc.DocumentNode.Descendants()
                            .Where(n => n.Name == "script" || /*n.Name == "style" ||*/ n.Name == "#comment" || n.Name=="img")
                            .ToList()
                            .ForEach(n => n.Remove());
            string newHtml=doc.DocumentNode.OuterHtml;
            ModifiedByte = Encoding.ASCII.GetBytes(newHtml);
        }
        
    }
}