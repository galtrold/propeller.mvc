using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Shell.Applications.ContentEditor;
using DateTime = System.DateTime;
using Image = Propeller.Mvc.Model.Adapters.Image;

namespace ModelTest.ViewModels
{
    public class CarViewModel : PropellerViewModel<CarViewModel>
    {

        public string Manufacture { get; set; }
        public string CarModel { get; set; }
        public string CarClass { get; set; }

        public bool IsActive { get; set; }

        public GeneralLink WikiLink { get; set; }

        public DateTime EnteredProductionDate { get; set; }

        public Image CarPhoto { get; set; }

        public CountryViewModel ProductionCountry { get; set; }


     
    }
}
