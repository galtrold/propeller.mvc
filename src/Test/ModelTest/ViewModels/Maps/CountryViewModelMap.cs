using ModelTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace ModelTest.ViewModels.Maps
{
    public class CountryModelMap : ConfigurationMap<CountryViewModel>
    {
        public CountryModelMap()
        {
            SetProperty(p => p.Name).Map(new ID(ConstantsCountryModel.Fields.NameField)).Include();
            SetProperty(p => p.Currency).Map(new ID(ConstantsCountryModel.Fields.CurrencyField)).Include();
            SetProperty(p => p.DieselTax).Map(new ID(ConstantsCountryModel.Fields.DieselTaxField)).Include();
            SetProperty(p => p.PetrolTax).Map(new ID(ConstantsCountryModel.Fields.PetrolTaxField)).Include();
        }
    }
}