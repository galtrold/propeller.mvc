using System.Web.Mvc;
using Propeller.Mvc.Demo.ViewModels;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class PlanetController : Controller
    {
        public ActionResult Index()
        {
            var planetViewModel = new PlanetViewModel(RenderingContext.Current.Rendering);

            return View(planetViewModel);
        }
    }
}