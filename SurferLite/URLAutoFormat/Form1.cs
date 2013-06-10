using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            // We need to put main code here of applying formatting input URL

            //check if the url is ok
            if (Uri.IsWellFormedUriString(textBoxInput.Text,UriKind.RelativeOrAbsolute)){
                textBoxOutput.Text = textBoxInput.Text;
            }
        }
    }
}
