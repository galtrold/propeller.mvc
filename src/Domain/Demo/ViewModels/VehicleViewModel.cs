using System;
using Propeller.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class VehicleViewModel : PropellerViewModel<VehicleViewModel>, IPropellerTemplate<VehicleViewModel>, ICombatId
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }
        public string Class { get; set; }

        public string Photo { get; set; }

        public string Engine { get; set; }
        public string Hyperdrive { get; set; }

        public string Length { get; set; }

        public string MaximumAtmosphericSpeed { get; set; }

        public string Shielding { get; set; }

        public DateTime EnteredService { get; set; }

      

        
        public VehicleViewModel TemplateArg() { return this; }

        public string Category { get; set; }
    }
}