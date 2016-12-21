using System.IO;
using System.Web;

namespace Propeller.Mvc.Core.Utility
{
    public static class EnvironmentSetttings
    {
        private static string _applicationPath;
        public static string ApplicationPath
        {
            get
            {
                if(_applicationPath == null)
                    _applicationPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "bin");
                return _applicationPath;
            }
            set { _applicationPath = value; }
        }
    }
}