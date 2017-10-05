using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace IntergrationTest.Models.Maps
{
    public class VehichleModelMap : ConfigurationMap<VehichleModel>
    {
        public VehichleModelMap()
        {
            SetProperty(p => p.Name).Map(new ID(ConstantsVehicleModel.Fields.NameField)).Include();
            SetProperty(p => p.Length).Map(new ID(ConstantsVehicleModel.Fields.LengthField)).Include();
            SetProperty(p => p.CargoKg).Map(new ID(ConstantsVehicleModel.Fields.CargoField)).Include();
            SetProperty(p => p.HasHyperdrive).Map(new ID(ConstantsVehicleModel.Fields.HasHyperdrive)).Include();
            SetProperty(p => p.WikiLink).Map(new ID(ConstantsVehicleModel.Fields.ExternalWikiLink)).Include();
            SetProperty(p => p.Photo).Map(new ID(ConstantsVehicleModel.Fields.PhotoField)).Include();
            SetProperty(p => p.ClassModel).Map(new ID(ConstantsVehicleModel.Fields.Class)).Include();
            SetProperty(p => p.Appearances).Map(new ID(ConstantsVehicleModel.Fields.AppearanceField)).Include();
        }
    }
}