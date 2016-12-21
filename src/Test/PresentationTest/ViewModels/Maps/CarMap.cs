using PresentationTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace PresentationTest.ViewModels.Maps
{
    public class CarMap : ConfigurationMap<CarViewModel>
    {
        public CarMap()
        {
            SetProperty(p => p.Manufacture).Map(new ID(ConstantsCarModel.Fields.ManuFactureField)).Include();
            SetProperty(p => p.CarClass).Map(new ID(ConstantsCarModel.Fields.CarClassField)).Include();
            SetProperty(p => p.CarModel).Map(new ID(ConstantsCarModel.Fields.CarModelField)).Include();
        }
    }
}