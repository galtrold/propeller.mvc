using ModelTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace ModelTest.ViewModels.Maps
{
    public class CarModelMap : ConfigurationMap<CarViewModel>
    {
        public CarModelMap()
        {
            SetProperty(p => p.Manufacture).Map(ConstantsCarModel.Fields.ManuFactureField).Include();
            SetProperty(p => p.CarClass).Map(ConstantsCarModel.Fields.CarClassField).Include();
            SetProperty(p => p.CarModel).Map(ConstantsCarModel.Fields.CarModelField).Include();
            SetProperty(p => p.IsTurboCharged).Map(ConstantsCarModel.Fields.IsTurboCharged).Include();
            SetProperty(p => p.EnteredProductionDate).Map(ConstantsCarModel.Fields.EnteredProductionDateField).Include();
            SetProperty(p => p.CarPhoto).Map(ConstantsCarModel.Fields.CarPhoto).Include();
            SetProperty(p => p.WikiLink).Map(ConstantsCarModel.Fields.ExternalWikiLink).Include();
            SetProperty(p => p.ProductionCountry).Map(ConstantsCarModel.Fields.ProductionCountry).Include();
        }
    }
}