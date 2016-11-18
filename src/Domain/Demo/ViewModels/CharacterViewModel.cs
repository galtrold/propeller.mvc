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

        public string Homeworld { get; set; }

        public string Photo { get; set; }

        public string PrimaryTransporation { get; set; }

        public string ExternalLink { get; set; }

        public CharacterViewModel(Item dataItem) : base(dataItem)
        {
        }

        public CharacterViewModel(Rendering rendering) : base(rendering)
        {
        }
        
        public CharacterViewModel TemplateArg()
        {
            return this;
        }
    }
}