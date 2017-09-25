using ModelTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace ModelTest.ViewModels.Maps
{
    public class CarModelMap : ConfigurationMap<CarViewModel>
    {
        public CarModelMap()
        {
            SetProperty(p => p.Manufacture).Map(new ID(ConstantsCarModel.Fields.ManuFactureField)).Include();
            SetProperty(p => p.CarClass).Map(new ID(ConstantsCarModel.Fields.CarClassField)).Include();
            SetProperty(p => p.CarModel).Map(new ID(ConstantsCarModel.Fields.CarModelField)).Include();
            SetProperty(p => p.IsActive).Map(new ID(ConstantsCarModel.Fields.IsActive)).Include();
            SetProperty(p => p.EnteredProductionDate).Map(new ID(ConstantsCarModel.Fields.EnteredProductionDateField)).Include();
            SetProperty(p => p.CarPhoto).Map(new ID(ConstantsCarModel.Fields.CarPhoto)).Include();
            SetProperty(p => p.WikiLink).Map(new ID(ConstantsCarModel.Fields.ExternalWikiLink)).Include();
            SetProperty(p => p.ProductionCountry).Map(new ID(ConstantsCarModel.Fields.ProductionCountry)).Include();
        }
    }
}