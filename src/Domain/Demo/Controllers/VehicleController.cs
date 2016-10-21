using System.Web.Mvc;
using Propeller.Mvc.Demo.ViewModels;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class VehicleController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new VehicleViewModel(RenderingContext.Current.Rendering);

            return View(viewModel);
        }
    }
}