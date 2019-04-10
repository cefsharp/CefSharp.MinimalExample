// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.MinimalExample.WinForms.JavascriptBinding;
using CefSharp.SchemeHandler;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
        private readonly ChromiumWebBrowser browser;
        private readonly JavascriptEventHandler eventHandler;

        static BrowserForm()
        {
            var globalRequestContext = Cef.GetGlobalRequestContext();

            //Register a scheme handler factory for our Html directory for the binding.cefsharp.test domain name
            //so we can access our local web pages. There are lots of options for doing this, this is a very simple method.
            //This example contains two basic examples, we'll register them under different domains for the purpose
            //of testing cross-domain navigation.
            globalRequestContext.RegisterSchemeHandlerFactory("http", "binding.cefsharp.test", new FolderSchemeHandlerFactory(Path.GetFullPath("javascriptbinding\\html")));
            globalRequestContext.RegisterSchemeHandlerFactory("http", "cefsharp.binding.test", new FolderSchemeHandlerFactory(Path.GetFullPath("javascriptbinding\\html"), defaultPage: "simple.html"));
        }

        public BrowserForm()
        {
            InitializeComponent();

            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            browser = new ChromiumWebBrowser("http://binding.cefsharp.test")
            {
                Dock = DockStyle.Fill,
            };
            toolStripContainer.ContentPanel.Controls.Add(browser);

            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;

            //Our bound object, our javascript code will call the RaiseEvent method
            eventHandler = new JavascriptEventHandler();
            eventHandler.EventArrived += JavascriptEventHandlerEventArrived;

            //We'll use the default Binding which is capable of turning Javascript objects into C# objects, supports use of dynamic keyboard also
            //For Index.html we'll bound our object in advance.
            browser.JavascriptObjectRepository.Register("boundEventHandler", eventHandler, true, BindingOptions.DefaultBinder);

            //For Simple.html we'll register our object on demand
            //Usually you would only bind an object once, we're reusing the object for demo purposes only.
            browser.JavascriptObjectRepository.ResolveObject += (sender, args) =>
            {
                if (args.ObjectName == "simpleBoundEventHandler")
                {
                    args.ObjectRepository.Register("simpleBoundEventHandler", eventHandler, true, BindingOptions.DefaultBinder);
                }
            };

            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}, Environment: {3}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion, bitness);
            DisplayOutput(version);
        }

        private void JavascriptEventHandlerEventArrived(string eventName, dynamic eventArgs)
        {
            if (eventName == "click")
            {
                //Thanks to the model binder we can use the dynamic keyword to access our params here.
                var id = eventArgs.id;
                var tagName = eventArgs.tagName;
                var link = eventArgs.link;

                browser.Load(link);
            }
        }

        private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
            {
                var b = ((ChromiumWebBrowser)sender);

                this.InvokeOnUiThreadIfRequired(() => b.Focus());
            }
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            SetCanGoBack(args.CanGoBack);
            SetCanGoForward(args.CanGoForward);

            this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => urlTextBox.Text = args.Address);
        }

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
            goButton.Text = isLoading ?
                "Stop" :
                "Go";
            goButton.Image = isLoading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        public void DisplayOutput(string output)
        {
            this.InvokeOnUiThreadIfRequired(() => outputLabel.Text = output);
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void ExitMenuItemClick(object sender, EventArgs e)
        {
            browser.Dispose();
            Cef.Shutdown();
            Close();
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(urlTextBox.Text);
        }

        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }

        private void ShowDevToolsMenuItemClick(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }
    }
}
