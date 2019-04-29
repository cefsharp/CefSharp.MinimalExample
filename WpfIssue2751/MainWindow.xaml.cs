using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
using CefSharp.Wpf;
using CefSharp;

namespace WpfIssue2751
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            MyTitle = "Show CefSharp.Wpf Issue 2751";

            SizeChanged += OnSizeChanged;

            Loaded += OnLoad;

            browser.LoadingStateChanged += OnBrowserLoaded;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ;
        }

        async private void OnBrowserLoaded(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                await Dispatcher.Invoke(async () =>
                {
                    BodyHeight = await GetBodyScrollHeight();
                    BodyWidth = await GetBodyScrollWidth();
                    PageCount = await GetPageCount();

                    browser.Height = BodyHeight;
                    browser.Width = BodyWidth;

                    browser.InvalidateVisual();

                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string myTitle;

        public string MyTitle
        {
            get { return myTitle; }
            set
            {
                myTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyTitle)));
            }
        }

        private int pageCount;

        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageCount)));
            }
        }

        async private Task<int> GetPageCount()
        {
            int result = 1;

            var js = await browser.EvaluateScriptAsync("GetPageCount()");
            if (js.Result != null)
            {
                result = string.IsNullOrEmpty(Convert.ToString(js.Result)) ? 1 : Convert.ToInt32(js.Result);
            }

            return result;
        }

        async private Task<double> GetBodyScrollHeight()
        {
            double result = 1;

            var js = await browser.EvaluateScriptAsync("GetBodyScrollHeight()");
            if (js.Result != null)
            {
                result = string.IsNullOrEmpty(Convert.ToString(js.Result)) ? 1 : Convert.ToDouble(js.Result);
            }

            return result;
        }
        async private Task<double> GetBodyScrollWidth()
        {
            double result = 1;

            var js = await browser.EvaluateScriptAsync("GetBodyScrollWidth()");
            if (js.Result != null)
            {
                result = string.IsNullOrEmpty(Convert.ToString(js.Result)) ? 1 : Convert.ToDouble(js.Result);
            }

            return result;
        }
        private double bodyHeight;

        public double BodyHeight
        {
            get { return bodyHeight; }
            set
            {
                bodyHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BodyHeight)));
            }
        }

        private double bodyWidth;

        public double BodyWidth
        {
            get { return bodyWidth; }
            set
            {
                bodyWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BodyWidth)));
            }
        }

        private void LoadHtmlFile(string filename)
        {
            string html = File.ReadAllText(filename);
            browser.LoadHtml(html);
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            string filename = "normal.html";
            LoadHtmlFile(filename);
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            string filename = "big.html";
            LoadHtmlFile(filename);
        }
    }
}
