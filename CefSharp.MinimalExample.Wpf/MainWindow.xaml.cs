using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf
{
    public partial class MainWindow : Window
    {
        private ChromeDriver driver;
        private ChromeDriverService service;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += WindowLoaded;

            Closed += WindowClosed;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //Use the ThreadPool to connect ChromeDriver

            Task.Run(() =>
            {
                try
                {
                    service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;
                    var chromeOptions = new ChromeOptions
                    {
                        DebuggerAddress = "localhost:9222"
                    };

                    driver = new ChromeDriver(service, chromeOptions);

                    driver.Navigate().GoToUrl("http://github.com");
                }
                catch (Exception)
                {
                    service?.Dispose();
                }
            });
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            driver?.Close();
            driver?.Dispose();
            service?.Dispose();
        }
    }
}
