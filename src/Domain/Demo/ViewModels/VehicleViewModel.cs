using Newtonsoft.Json;
using Propeller.Mvc.View;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class VehicleViewModel : PropellerViewModel<VehicleViewModel>, IPropellerTemplate<VehicleViewModel>
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

        public VehicleViewModel()
        {
        }

        public VehicleViewModel(Item dataItem) : base(dataItem)
        {
        }

        public VehicleViewModel(Rendering rendering) : base(rendering)
        {
        }

        
        public VehicleViewModel TemplateArg() { return this; } 
        
    }
}