using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace winform.test
{
    public class AppContext : ApplicationContext
    {
        private Form1 form1;
        private Form1 form2;

        public AppContext()
        {
            form1 = new Form1();
            form1.WindowState = FormWindowState.Normal;
            form2 = new Form1();
            form2.WindowState = FormWindowState.Normal;

            form1.FormClosed += OnFormClosed;
            form2.FormClosed += OnFormClosed;

            form1.Show();
            form2.Show();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                ExitThread();
            }
        }
    }
}
