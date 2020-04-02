using CefSharp.WinForms;
using CefSharp.WinForms.Example.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var browser = new ChromiumWebBrowser("www.google.com")
            {
                Dock = DockStyle.Fill,
            };

            browser.FocusHandler = new FocusHandler();
            Controls.Add(browser);
        }

    }
}
