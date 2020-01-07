using System.Web.Mvc;
using Newtonsoft.Json;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Demo.ViewModels;
using Propeller.Mvc.Model.Factory;
using Propeller.Mvc.Presentation.Factory;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.Controllers
{
    public class CharacterController : Controller
    {

        public ActionResult Index()
        {
            
            var vmFactory = new ViewModelFactory();
            var characterViewModel =  vmFactory.Create<CharacterViewModel>(RenderingContext.Current.Rendering);
            ViewBag.SerializedModel = JsonConvert.SerializeObject(characterViewModel, Formatting.Indented);
            return View(characterViewModel);
        }
    }
}