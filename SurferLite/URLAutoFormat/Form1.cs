using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URLAutoFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Globalization.CompareInfo cmpUrl = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
            if (cmpUrl.IsPrefix(textBoxInput.Text, "http://") == false)
            {
                textBoxOutput.Text = "http://" + textBoxInput.Text;
            }
            Regex RgxUrl = new Regex("(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?");
            if (RgxUrl.IsMatch(textBoxInput.Text))
            {
                MessageBox.Show("URL is valid.");
            }
            else
            {
                MessageBox.Show("URL is invalid!");
            }

            //check if the url is ok
            if (Uri.IsWellFormedUriString(textBoxInput.Text,UriKind.RelativeOrAbsolute)){
                textBoxOutput.Text = textBoxInput.Text;
            }
        }
    }
}
