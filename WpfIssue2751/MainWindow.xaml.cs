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
using System.Drawing;
using System.Collections.ObjectModel;


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
            browser.SizeChanged += OnBrowserSizeChanged;
            browser.Paint += OnBrowserPaint;
        }

        public double ScrollViewHeight { get; } = 474;
        public double ScrollViewWidth { get; } = 636;

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            AddLog("OnLoad");
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //AddLog("OnSizeChanged");

            this.browser.Height = ScrollViewHeight;
            this.browser.Width = ScrollViewWidth;
        }

        private void OnBrowserPaint(object sender, PaintEventArgs e)
        {
            //AddLog("OnBrowserPaint");
        }
        private void OnBrowserSizeChanged(object sender, SizeChangedEventArgs e)
        {
            AddLog("OnBrowserSizeChanged");
        }

        async private void OnBrowserLoaded(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                AddLog("OnBrowserLoaded");

                await Dispatcher.Invoke(async () =>
                {
                    BodyHeight = await GetBodyHeight();
                    BodyWidth = await GetBodyWidth();
                    HtmlPageHeight = await GetPageHeight();
                    HtmlPageWidth = BodyWidth;

                    TotalPageCount = await GetPageCount();

                    SetZoom("100");
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

        private void AddLog(string info)
        {
            string msg = $"{DateTime.Now.ToString("HH:mm:ss.fff")} - {info} -ActualWidth={browser.ActualWidth},ActHeight={browser.ActualHeight} ";

            Dispatcher.Invoke(() =>
            {
                this.LogCollection.Insert(0, msg);
            });
        }

        private ObservableCollection<string> logCollection = new ObservableCollection<string>();

        public ObservableCollection<string> LogCollection
        {
            get { return logCollection; }
            set
            {
                logCollection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LogCollection)));
            }
        }

        private int pageCount;

        public int TotalPageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPageCount)));
            }
        }

        private int currentPage;

        public int CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
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

        async private Task<double> GetBodyHeight()
        {
            double result = 1;

            var js = await browser.EvaluateScriptAsync("GetBodyHeight()");
            if (js.Result != null)
            {
                result = string.IsNullOrEmpty(Convert.ToString(js.Result)) ? 1 : Convert.ToDouble(js.Result);
            }

            return result;
        }

        async private Task<double> GetBodyWidth()
        {
            double result = 1;

            var js = await browser.EvaluateScriptAsync("GetBodyWidth()");
            if (js.Result != null)
            {
                result = string.IsNullOrEmpty(Convert.ToString(js.Result)) ? 1 : Convert.ToDouble(js.Result);
            }

            return result;
        }

        async private Task<double> GetPageHeight()
        {
            double result = 1;

            var js = await browser.EvaluateScriptAsync("GetPageHeight()");
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

        private double pageHeight;

        public double HtmlPageHeight
        {
            get { return pageHeight; }
            set
            {
                pageHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HtmlPageHeight)));
            }
        }

        private double pageWidth;

        public double HtmlPageWidth
        {
            get { return pageWidth; }
            set
            {
                pageWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HtmlPageWidth)));
            }
        }

        private double zoomedPageHeight;

        public double ZoomedPageHeight
        {
            get { return zoomedPageHeight; }
            set
            {
                zoomedPageHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZoomedPageHeight)));
            }
        }

        private double zoomedPageWidth;

        public double ZoomedPageWidth
        {
            get { return zoomedPageWidth; }
            set
            {
                zoomedPageWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZoomedPageWidth)));
            }
        }

        private string info;

        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));
            }
        }

        private void LoadHtmlFile(string filename)
        {
            string html = File.ReadAllText(filename);
            Dispatcher.Invoke(() =>
            {
                browser.LoadHtml(html, "http://ReportingControl");

            });
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            string filename = "debug.html";
            LoadHtmlFile(filename);
        }

        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
                GotoPage(CurrentPage);
            }
        }

        private void UpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage < (TotalPageCount-1))
            {
                CurrentPage++;
                GotoPage(CurrentPage);
            }

        }

        private void PART_VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ;
        }

        private void PART_VerticalScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            
            ;
        }

        private void Zoom50_Click(object sender, RoutedEventArgs e)
        {
            SetZoom("50");
        }

        private void Zoom100_Click(object sender, RoutedEventArgs e)
        {
            SetZoom("100");

        }

        private void Zoom125_Click(object sender, RoutedEventArgs e)
        {
            SetZoom("125");

        }

        public double CurrentZoomPercent { get; set; }
        public double CurrentZoomLevel { get; set; }

        private double GetBrowserZoomPercent(string userPercent)
        {
            switch (userPercent)
            {
                case "50":
                    return (ScrollViewHeight+10) / HtmlPageHeight;
                case "125":
                    return ScrollViewWidth*1.25 / HtmlPageWidth;
                case "100":
                default:
                    return ScrollViewWidth / HtmlPageWidth;
            }
        }

        private double GetBrowserZoomLevel(double zoomPercent)
        {
            double result = Math.Log(zoomPercent) / Math.Log(1.2);
            //a little bigger
            return Math.Ceiling(result * 1000) / 1000;
        }

        private void SetZoom(string userPercent)
        {
            AddLog($"SetZoom({userPercent})");

            ////browser.Height = BodyHeight;
            //browser.Height = ScreenHeight*9.103;
            //browser.Width = BodyWidth;

            //browser.InvalidateVisual();
            int currentPageIndex = GetCurrtentPage();

            CurrentZoomPercent = GetBrowserZoomPercent(userPercent);
            CurrentZoomLevel = GetBrowserZoomLevel(CurrentZoomPercent);

            ZoomedPageHeight = HtmlPageHeight * CurrentZoomPercent;
            ZoomedPageWidth = HtmlPageWidth * CurrentZoomPercent;

            Dispatcher.Invoke(() =>
            {
                AddLog($"browser.SetZoomLevel() - before");

                browser.SetZoomLevel(CurrentZoomLevel);
                //add a little more
                //browser.Height = TotalPageCount * ZoomedPageHeight + 0.1;
                browser.Height = ZoomedPageHeight * TotalPageCount;

                //browser.Width = ZoomedPageWidth > ScrollViewWidth ? ZoomedPageWidth : ScrollViewWidth;
                browser.Width = ZoomedPageWidth;

                AddLog($"browser.InvalidateVisual() - before");

                //update browser layout and render it
                //browser.InvalidateMeasure();//This method calls InvalidateArrange internally.
                browser.InvalidateVisual();//This method calls InvalidateArrange internally.
                AddLog($"browser.InvalidateVisual() - after");
            });

            GotoPage(currentPageIndex);
        }

        public void GotoPage(int currentPageIndex)
        {
            AddLog($"GotoPage({currentPageIndex})");


            int index = currentPageIndex;
            if (index <= 1) { index = 1; }
            if (index >= TotalPageCount) { index = TotalPageCount; }

            //add a little more
            double verticalOffset = (index - 1) * ZoomedPageHeight + 0.1 * CurrentZoomPercent;
        }

        public int GetCurrtentPage()
        {
            //if scroll more than 90% of page height, add page index
            int index = 1 + (int)((myScrollViewer.VerticalOffset + 1 * ZoomedPageHeight) / ZoomedPageHeight);
            return index;
        }

        private void MyScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ;
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            this.LogCollection.Clear();
        }
    }
}
