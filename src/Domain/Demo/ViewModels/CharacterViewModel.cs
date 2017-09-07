using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class CharacterViewModel : PropellerViewModel<CharacterViewModel>, IPropellerTemplate<CharacterViewModel>
    {
        public string Species { get; set; }

        public string Genger { get; set; }

        public string Occupation { get; set; }

        public string Affiliation { get; set; }

        public GeneralLink Homeworld { get; set; }

        public string Photo { get; set; }

        public GeneralLink PrimaryTransportation { get; set; }

        public string ExternalLink { get; set; }

        
        public CharacterViewModel TemplateArg()
        {
            return this;
        }
    }
}