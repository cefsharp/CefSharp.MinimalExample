using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharp.MinimalExample.Common
{
    public abstract class GenericControlToJsObject<T> where T : EventArgs
    {
        private EventHandler<T> callBackEventHandler;
        public event EventHandler<T> WebBrowserCallBack
        {
            add { callBackEventHandler += value; }
            remove { callBackEventHandler -= value; }
        }

        protected virtual void OnWebBrowserCallBack(T e)
        {
            callBackEventHandler?.Invoke(this, e);
        }
    }
    public enum GanttCallBackNames
    {
        SelectedItemChanged
    }
    public class GanttBoundObjectEventArgs : EventArgs
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.EventArgs" /> class.</summary>
        public GanttBoundObjectEventArgs(GanttCallBackNames ganttCallBackName, string data)
        {
            GanttCallBackName = ganttCallBackName;
            Data = data;
        }

        public GanttCallBackNames GanttCallBackName { get; set; }
        public string Data { get; set; }
    }
    public class GanttBoundObject : GenericControlToJsObject<GanttBoundObjectEventArgs>
    {
        public void OnSelectedItemChanged(string taskId)
        {
            base.OnWebBrowserCallBack(new GanttBoundObjectEventArgs(GanttCallBackNames.SelectedItemChanged, taskId));
        }
    }
    public class DataProvider
    {
        static DataProvider()
        {
            CallBackObject = new GanttBoundObject();
        }
        public static string GanttUrl
        {
            get { return BuildLocalUrl("/LocalHtml/gantt.html"); }
        }

        public static GanttBoundObject CallBackObject { get; private set; }

        private static string BuildLocalUrl(string localhtml)
        {
            return UrlHelper.GetPhysicalPath(localhtml);
        }

        public static string ProvideData()
        {
            return string.Empty;
        }
    }

    internal class UrlHelper
    {
        public static string GetPhysicalPath(string relativePath)
        {
            //有效性验证
            if (string.IsNullOrEmpty(relativePath))
            {
                return string.Empty;
            }
            //~,~/,/,\
            relativePath = relativePath.Replace("/", @"\").Replace("~", string.Empty).Replace("~/", string.Empty);
            //网络共享目录中的文件不应移除根路径
            if (!relativePath.StartsWith("\\\\"))
                relativePath = relativePath.StartsWith("\\") ? relativePath.Substring(1, relativePath.Length - 1) : relativePath;
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var fullPath = System.IO.Path.Combine(path, relativePath);
            return fullPath;
        }
    }
}
