// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System.Windows.Controls;
using CefSharp.MinimalExample.Wpf.ViewModels;

namespace CefSharp.MinimalExample.Wpf.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            browser.RegisterJsObject("jsBind", new BoundExampleObject());
        }

        public void DisposeBrowserAndShutdown()
        {
            var parent = (Grid) browser.Parent;
            parent.Children.RemoveAt(0);
            browser.Dispose();
            Cef.Shutdown();
        }
    }

    public class BoundExampleObject
    {

    }
}
