using ModelTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace ModelTest.ViewModels.Maps
{
    public class CountryModelMap : ConfigurationMap<CountryViewModel>
    {
        public CountryModelMap()
        {
            SetProperty(p => p.Name).Map(ConstantsCountryModel.Fields.NameField).Include();
            SetProperty(p => p.Currency).Map(ConstantsCountryModel.Fields.CurrencyField).Include();
            SetProperty(p => p.DieselTax).Map(ConstantsCountryModel.Fields.DieselTaxField).Include();
            SetProperty(p => p.PetrolTax).Map(ConstantsCountryModel.Fields.PetrolTaxField).Include();
        }
    }
}