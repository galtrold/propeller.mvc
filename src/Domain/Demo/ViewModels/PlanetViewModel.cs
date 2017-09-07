using Propeller.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class PlanetViewModel : PropellerViewModel<PlanetViewModel>, IPropellerTemplate<PlanetViewModel>
    {
        public string Region { get; set; }

        public string Moons { get; set; }

        public string SurfaceWater { get; set; }

        public string Population { get; set; }

        public string Affiliation { get; set; }

        public string Photo { get; set; }

     

        public PlanetViewModel TemplateArg() {  return this; } 
    }
}