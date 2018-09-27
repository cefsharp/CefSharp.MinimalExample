using System;
using System.Collections.Generic;
using System.IO;

namespace CefSharp.MinimalExample.WinForms
{
    public class LocalResourceSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public const string SchemeName = "custom";

        private static readonly Dictionary<string, string> ResourceDictionary = new Dictionary<string, string>()
        {
            { "/index.html", "This content was served from a custom ISchemeHandlerFactory." }
        };

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var uri = new Uri(request.Url);
            var fileName = uri.AbsolutePath;

            if (ResourceDictionary.TryGetValue(fileName, out var resource))
            {
                var fileExtension = Path.GetExtension(fileName);
                return ResourceHandler.FromString(resource, fileExtension);
            }

            //ResourceHandler has many static methods for dealing with strings, stream, files

            return ResourceHandler.FromString("File Not Found", ".html"); ;
        }
    }
}


