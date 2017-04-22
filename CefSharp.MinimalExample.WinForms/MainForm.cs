using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class MainForm : Form
    {
        private BrowserForm _browserForm;

        public MainForm() {
            InitializeComponent();
        }


        // These url are meaningless, but these can reproduces my problem
        private void btnOpenBrowser_Click(object sender, EventArgs e) {
            if (_browserForm != null && !_browserForm.IsDisposed) {
                _browserForm.Show();
                return;
            }

            // load https://mai.taobao.com/seller_admin.htm, it will receive 302. redirect to login.taobao.com
            _browserForm = _browserForm = new BrowserForm( "https://mai.taobao.com/seller_admin.htm" );
            _browserForm.AddressChanged += Browser_AddressChanged;
            _browserForm.ShowDialog();
        }


        private void Browser_AddressChanged(object sender, CefSharp.AddressChangedEventArgs e) {
            var url = new Uri( e.Address );
            if ("mai.taobao.com".Equals( url.Host, StringComparison.OrdinalIgnoreCase )) {
                return;
            }

            var browserForm = _browserForm;

            if (browserForm.InvokeRequired) {
                browserForm.Invoke( new MethodInvoker( delegate { browserForm.Hide(); } ) );
            }
            else { browserForm.Hide(); }

            if ("login.taobao.com".Equals( url.Host, StringComparison.OrdinalIgnoreCase )) {
                browserForm.LoadUrl( "https://www.github.com/" );
                return;
            }
            if (!"github.com".Equals( url.Host, StringComparison.OrdinalIgnoreCase )) {
                return;
            }

            browserForm.AddressChanged -= Browser_AddressChanged;

            MessageBox.Show( "Done" );
        }


    }
}
