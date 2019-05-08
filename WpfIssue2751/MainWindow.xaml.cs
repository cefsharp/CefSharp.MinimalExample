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
using System.Threading.Tasks;


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

            MyTitle = "CefZoom";

            SizeChanged += OnSizeChanged;

            Loaded += OnLoad;

            //myScrollViewer.

            browser.LoadingStateChanged += OnBrowserLoaded;
            browser.SizeChanged += OnBrowserSizeChanged;

            //CurrentPageIndex = 0;
            //TotalPageCount = 0;
        }

        public double ScrollViewHeight { get; } = 474;
        public double ScrollViewWidth { get; } = 636;

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            AddLog($"OnLoad() IsBrowserLoaded={IsBrowserLoaded}");
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            AddLog($"OnSizeChanged() IsBrowserLoaded={IsBrowserLoaded}");

            this.browser.Height = ScrollViewHeight;
            this.browser.Width = ScrollViewWidth;
        }

        private void OnScrollViewerLayoutUpdated(object sender, EventArgs e)
        {
            AddLog("OnScrollViewerLayoutUpdated");

        }

        async private void OnBrowserSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsBrowserLoaded)
            {
                AddLog($"OnBrowserSizeChanged() IsResizing={IsResizing}");

                //first time
                if (!HasResized)
                {
                    HasResized = true;
                    CurrentPageIndex = 1;
                    PendingZoomIndex = 1;
                }
                else
                {
                    ;
                }

                AddLog($"myScrollViewer.UpdateLayout()-before");
                myScrollViewer.UpdateLayout();
                for(int i = 1; i <= 10; i++)
                {
                    await Task.Delay(100);
                    AddLog($"myScrollViewer.UpdateLayout()-For Loop{i*100}");
                }

                //await Task.Delay(1000);

                ActualZoomIndex = PendingZoomIndex;
                CurrentZoomIndex = ActualZoomIndex;

                GotoPage(CurrentPageIndex);

                IsResizing = false;
            }
        }

        public bool IsBrowserLoaded { get; set; }
        public bool HasResized { get; set; }
        public bool IsResizing { get; set; }
        async private void OnBrowserLoaded(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                AddLog("OnBrowserLoaded");

                await Dispatcher.Invoke(async () =>
                {
                    IsBrowserLoaded = true;
                    HasResized = false;

                    BodyHeight = await GetBodyHeight();
                    BodyWidth = await GetBodyWidth();
                    HtmlPageHeight = await GetPageHeight();
                    HtmlPageWidth = BodyWidth;

                    TotalPageCount = await GetPageCount();
                    //CurrentPageIndex = 1;

                    IsResizing = false;

                    SetZoom(1);//100
                });
            }
        }



        private void AddLog(string info)
        {
            //string msg = $"{DateTime.Now.ToString("HH:mm:ss.fff")} - {info} -ActualWidth={browser.ActualWidth},ActHeight={browser.ActualHeight} ,ScrollHeight={myScrollViewer.ExtentHeight}";
            Dispatcher.Invoke(() =>
            {
                string msg = $"{DateTime.Now.ToString("HH:mm:ss.fff")} - {info} -browserActHeight={browser.ActualHeight} ,ScrollExtentHeight={myScrollViewer.ExtentHeight}";
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

        private int totalPageCount;

        public int TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                totalPageCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPageCount)));
            }
        }

        private int currentPageIndex;

        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                currentPageIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPageIndex)));
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

        private void LoadHtmlFile(string filename)
        {
            string html = File.ReadAllText(filename);
            Dispatcher.Invoke(() =>
            {
                IsBrowserLoaded = false;
                browser.LoadHtml(html, "http://ReportingControl");

            });
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            string filename = "debug-big.html";
            LoadHtmlFile(filename);
        }

        private void DownBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex--;
                GotoPage(CurrentPageIndex);
            }
        }

        private void UpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPageIndex < (TotalPageCount))
            {
                CurrentPageIndex++;
                GotoPage(CurrentPageIndex);
            }
        }

        private void PART_VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //AddLog("PART_VerticalScrollBar_ValueChanged");                       
            CurrentPageIndex = GetCurrtentPage();
            //update
        }

        public double CurrentZoomPercent { get; set; }
        public double CurrentZoomLevel { get; set; }

        private double GetBrowserZoomPercent(int zoomIndex)
        {
            switch (zoomIndex)
            {
                case 0:
                    //50%
                    return (ScrollViewHeight+10) / HtmlPageHeight;
                case 2:
                    //125%
                    return ScrollViewWidth*1.25 / HtmlPageWidth;
                case 1:
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

        private void SetZoom(int zoomIndex)
        {
            string message = IsResizing ? "Deny" : "Resizing";
            AddLog($"SetZoom({zoomIndex}) {message}");

            ////browser.Height = BodyHeight;
            //browser.Height = ScreenHeight*9.103;
            //browser.Width = BodyWidth;

            //browser.InvalidateVisual();
            //int currentPageIndex = GetCurrtentPage();

            //CurrentPage = GetCurrtentPage();
            if (!IsResizing)
            {
                IsResizing = true;

                if (HasResized)
                {
                    if (ActualZoomIndex == zoomIndex)
                    {
                        AddLog($"SetZoom({zoomIndex}), same , not resizing...");

                        IsResizing = false;
                        CurrentZoomIndex = ActualZoomIndex;
                        return;
                    }
                }

                PendingZoomIndex = zoomIndex;

                CurrentZoomPercent = GetBrowserZoomPercent(zoomIndex);
                CurrentZoomLevel = GetBrowserZoomLevel(CurrentZoomPercent);

                ZoomedPageHeight = HtmlPageHeight * CurrentZoomPercent;
                ZoomedPageWidth = HtmlPageWidth * CurrentZoomPercent;


                Dispatcher.Invoke(() =>
                {
                    //AddLog($"browser.SetZoomLevel() - before");

                    browser.SetZoomLevel(CurrentZoomLevel);
                    //add a little more
                    //browser.Height = TotalPageCount * ZoomedPageHeight + 0.1;
                    browser.Height = ZoomedPageHeight * TotalPageCount;

                    //browser.Width = ZoomedPageWidth > ScrollViewWidth ? ZoomedPageWidth : ScrollViewWidth;
                    browser.Width = ZoomedPageWidth;

                   // AddLog($"browser.InvalidateVisual() - before");

                    //update browser layout and render it
                    //browser.InvalidateMeasure();//This method calls InvalidateArrange internally.
                    browser.InvalidateVisual();//This method calls InvalidateArrange internally.
                    //AddLog($"browser.InvalidateVisual() - after");
                });
            }

            //GotoPage(currentPageIndex);
        }

        public void GotoPage(int currentPageIndex)
        {
            int index = currentPageIndex;
            if (index <= 1) { index = 1; }
            if (index >= TotalPageCount) { index = TotalPageCount; }

            //add a little more
            double verticalOffset = (index - 1) * ZoomedPageHeight + 0.1 * CurrentZoomPercent;
            AddLog($"GotoPage({index}), {verticalOffset}, ZoomedHeight={ZoomedPageHeight}");

            myScrollViewer.ScrollToVerticalOffset(verticalOffset);
        }

        public int GetCurrtentPage()
        {
            //if scroll more than 90% of page height, add page index
            int index = 1 + (int)((myScrollViewer.VerticalOffset + 0.1 * ZoomedPageHeight) / ZoomedPageHeight);

            //AddLog($"GetCurrentPage(): index={index}, VerOffset={myScrollViewer.VerticalOffset}, ZoomedPageHeight={ZoomedPageHeight}");
            AddLog($"GetCurrentPage(): index={index}, VerOffset={myScrollViewer.VerticalOffset}, ZoomedPageHeight={ZoomedPageHeight}");

            return index;
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            this.LogCollection.Clear();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentZoomIndex > 0)
            {
                CurrentZoomIndex--;
                SetZoom(CurrentZoomIndex);
            }
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentZoomIndex < 2)
            {
                CurrentZoomIndex++;
                SetZoom(CurrentZoomIndex);
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            string filename = "debug-Small.html";
            LoadHtmlFile(filename);
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

        public int PendingZoomIndex { get; set; }
        public int ActualZoomIndex { get; set; }


        private int currentZoomIndex;

        public int CurrentZoomIndex
        {
            get { return currentZoomIndex; }
            set
            {
                currentZoomIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentZoomIndex)));
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
    }
}
