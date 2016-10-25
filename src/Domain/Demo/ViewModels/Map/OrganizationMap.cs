using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Propeller.Mvc.Demo.ViewModels.Map
{
    public class OrganizationMap : ConfigurationMap<OrganizationViewModel>
    {
        public OrganizationMap()
        {
            SetProperty(p => p.OrganizationType).Map(new ID("{73AACA1A-CC66-40C7-BA6F-F30603C7A42E}")).Include().Editable();
            SetProperty(p => p.Headquarters).Map(new ID("{435B2A53-F83C-4B8B-95BA-B41395409CDE}")).Include().Editable();
            SetProperty(p => p.Leaders).Map(new ID("{44B3D3FF-ED52-4124-B998-6EFA27A9897E}")).Include().Editable();
            SetProperty(p => p.FormedFrom).Map(new ID("{D4A744E0-81BF-43EC-B05D-750A439E571A}")).Include().Editable();
            SetProperty(p => p.DateEstablished).Map(new ID("{146B1291-22A9-428E-BB3B-7AE3BE6B0936}")).Include().Editable();
            SetProperty(p => p.DateReorganized).Map(new ID("{81BCBEF4-3289-4814-A108-9E1469AFD357}")).Include().Editable();
            SetProperty(p => p.DateDissolved).Map(new ID("{DEAD5BFE-8271-4F28-A5EC-AB962A41CC9E}")).Include().Editable();
            SetProperty(p => p.Intro).Map(new ID("{7F9C35CA-4C3D-4D72-9595-37C5627CFC63}")).Include().Editable();
            SetProperty(p => p.History).Map(new ID("{ECD1F451-9C4A-467A-8689-244B12A6F812}")).Include().Editable();
        }
    }
}