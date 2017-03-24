using Propeller.Mvc.Presentation;

namespace ModelTest.ViewModels
{
    public class CountryViewModel : PropellerViewModel<CountryViewModel>
    {

        public string Name { get; set; }

        public string Currency { get; set; }

        public string PetrolTax { get; set; }

        public string DieselTax { get; set; }



    }
}