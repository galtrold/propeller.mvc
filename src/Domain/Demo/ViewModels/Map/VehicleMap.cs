using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Propeller.Mvc.Demo.ViewModels.Map
{
    public class VehicleMap : ConfigurationMap<VehicleViewModel>
    {
        public VehicleMap()
        {
            SetProperty(p => p.Manufacturer).Map(new ID("{E02F29EB-A3F0-4388-A38A-D506604CF515}")).Include().Editable();
            SetProperty(p => p.Model).Map(new ID("{C59E678F-1891-4DDC-ADDD-E3DF93CA9991}")).Include().Editable();
            SetProperty(p => p.Class).Map(new ID("{697A3D97-0EE0-495B-AF9F-80125286E3F1}")).Include().Editable();
            SetProperty(p => p.Photo).Map(new ID("{9631C96D-ABFF-421F-B687-CD679E8A8E6E}")).Include().Editable();
            SetProperty(p => p.Engine).Map(new ID("{8383A0C6-4524-4C1E-B26D-444F998C807A}")).Include().Editable();
            SetProperty(p => p.Hyperdrive).Map(new ID("{38570933-4CF5-4D7A-B3A0-14C2E5533967}")).Include().Editable();
            SetProperty(p => p.Length).Map(new ID("{85F354F4-DF6F-4854-AE2A-809B5342FF7B}")).Include().Editable();
            SetProperty(p => p.MaximumAtmosphericSpeed).Map(new ID("{8BED42AC-2707-4CD5-B1FD-BF6BD138AF4B}")).Include().Editable();
            SetProperty(p => p.Shielding).Map(new ID("{9304E02D-4F20-4361-AEC4-E8BB10C7BC89}")).Include().Editable();
            SetProperty(p => p.EnteredService).Map(new ID("{CA2B640A-A591-46A3-A437-9A07ADC7F684}")).Include();
        }
    }
}