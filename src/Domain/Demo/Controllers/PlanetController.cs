using System.Web.Mvc;
using Propeller.Mvc.Demo.ViewModels;
using Propeller.Mvc.Presentation.Factory;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class PlanetController : Controller
    {
        public ActionResult Index()
        {
            var vmFactory = new ViewModelFactory();
            var planetViewModel = vmFactory.Create<PlanetViewModel>(RenderingContext.Current.Rendering);
            
            return View(planetViewModel);
        }
    }
}