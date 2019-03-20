// Copyright © 2010-2019 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.RenderProcess;

namespace CefSharp.MinimalExample.WinForms.RenderProcess
{
    public class RenderProcessHandler : IRenderProcessHandler
    {
        void IRenderProcessHandler.OnContextCreated(IBrowser browser, IFrame frame, IV8Context context)
        {
            //TODO: browser and frame are currently null as there is no C++ wrapper in the render process currently.
            V8Exception ex;
            if (!context.Execute("Object.freeze(console)", "about:blank", 1, out ex))
            {
                //TODO: Do something with the exception
            }
        }

        void IRenderProcessHandler.OnContextReleased(IBrowser browser, IFrame frame, IV8Context context)
        {
            
        }
    }
}
