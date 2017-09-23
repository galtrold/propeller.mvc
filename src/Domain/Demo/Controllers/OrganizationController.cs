using System.Web.Mvc;
using Propeller.Mvc.Demo.ViewModels;
using Propeller.Mvc.Presentation.Factory;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class OrganizationController : Controller
    {
        public ActionResult Index()
        {
            var vmFactory = new ViewModelFactory();
            var viewModel = vmFactory.Create<OrganizationViewModel>(RenderingContext.Current.Rendering);

            return View(viewModel);
        }
    }
}