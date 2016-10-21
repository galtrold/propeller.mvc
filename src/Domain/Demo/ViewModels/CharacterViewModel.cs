using Propeller.Mvc.View;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class CharacterViewModel : PropellerViewModel<CharacterViewModel>
    {
        public string Species { get; set; }

        public string Genger { get; set; }

        public string Occupation { get; set; }

        public string Affiliation { get; set; }

        public string Homeworld { get; set; }

        public string Photo { get; set; }

        public string PrimaryTransporation { get; set; }

        public CharacterViewModel(Item dataItem) : base(dataItem)
        {
        }

        public CharacterViewModel(Rendering rendering) : base(rendering)
        {
        }

        
    }
}