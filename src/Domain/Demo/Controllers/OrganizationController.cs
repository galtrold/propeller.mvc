using System.Web.Mvc;
using Propeller.Mvc.Demo.ViewModels;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class OrganizationController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new OrganizationViewModel(RenderingContext.Current.Rendering);

            return View(viewModel);
        }
    }
}