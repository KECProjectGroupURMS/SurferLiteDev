using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using HtmlAgilityPack;

namespace AgilityPackImplementation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = FormatText(textBox1.Text);
        }

        public string FormatText(string text)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(text);

            var links = document.DocumentNode.SelectNodes("//a[@href]");
            foreach (HtmlNode link in links)
            {
                listBox1.Items.Add(link.InnerText);
                if (link.Attributes["target"] != null)
                {
                    link.Attributes["target"].Value = "_blank";
                }
                else
                {
                    link.Attributes.Add("target", "_blank");
                }
            }
            int startIndex = text.IndexOf("<a");
            int endIndex = text.LastIndexOf('a') + 2;
            return text.Substring(startIndex, (text.Length - startIndex) - (text.Length - endIndex));
        }
    }
}
