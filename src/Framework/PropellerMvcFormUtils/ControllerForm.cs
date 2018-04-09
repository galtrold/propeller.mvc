using System.Web;
using System.Web.Mvc;

namespace Propeller.Mvc.FormUtils
{
    public static class ControllerForm
    {
        public static string ControllerUrl(this HtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.Request.RawUrl;
        }


        public static HtmlString ControllerHandler(this HtmlHelper helper, string controllerName = null, string action = "Index")
        {
            controllerName = controllerName ?? helper.ViewContext.HttpContext.Request.RawUrl.Trim('/');

            var htmlStr =
                $"<input type=\"hidden\" name=\"fhController\" value=\"{controllerName}Controller\" />" +
                $"<input type=\"hidden\" name=\"fhAction\" value=\"{action}\" />";
            return new HtmlString(htmlStr);
        }
    }
}