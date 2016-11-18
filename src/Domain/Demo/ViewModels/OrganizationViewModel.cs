using Propeller.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class OrganizationViewModel : PropellerViewModel<OrganizationViewModel>
    {
        public string OrganizationType { get; set; }
        public string Headquarters { get; set; }
        public string Leaders { get; set; }

        public string FormedFrom { get; set; }

        public string DateEstablished { get; set; }
        public string DateReorganized { get; set; }

        public string DateDissolved { get; set; }

        public string Intro { get; set; }

        public string History { get; set; }


        public OrganizationViewModel()
        {
        }

        public OrganizationViewModel(Item dataItem) : base(dataItem)
        {
        }

        public OrganizationViewModel(Rendering rendering) : base(rendering)
        {
        }
    }
}