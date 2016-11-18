using System.Web.Mvc;
using Propeller.Mvc.Demo.ViewModels;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class CharacterController : Controller
    {

        public ActionResult Index()
        {
            var characterViewModel = new CharacterViewModel(RenderingContext.Current.Rendering);

            return View(characterViewModel);
        }
    }
}