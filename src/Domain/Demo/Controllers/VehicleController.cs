using System.Web.Mvc;
using Newtonsoft.Json;
using Propeller.Mvc.Demo.ViewModels;
using Propeller.Mvc.Presentation.Factory;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class VehicleController : Controller
    {
        public ActionResult Index()
        {
            var vmFactory = new ViewModelFactory();
            var viewModel = vmFactory.Create<VehicleViewModel>(RenderingContext.Current.Rendering);
            ViewBag.SerializedModel = JsonConvert.SerializeObject(viewModel, Formatting.Indented);
            return View(viewModel);
        }
    }
}