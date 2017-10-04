using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace IntergrationTest.Models.Maps
{
    public class VehichleModelMap : ConfigurationMap<VehichleModel>
    {
        public VehichleModelMap()
        {
            SetProperty(p => p.Name).Map(new ID(ConstantsVehicleModel.Fields.NameField));
            SetProperty(p => p.Length).Map(new ID(ConstantsVehicleModel.Fields.LengthField));
            SetProperty(p => p.CargoKg).Map(new ID(ConstantsVehicleModel.Fields.CargoField));
            SetProperty(p => p.HasHyperdrive).Map(new ID(ConstantsVehicleModel.Fields.HasHyperdrive));
            SetProperty(p => p.WikiLink).Map(new ID(ConstantsVehicleModel.Fields.ExternalWikiLink));
            SetProperty(p => p.Photo).Map(new ID(ConstantsVehicleModel.Fields.PhotoField));
            SetProperty(p => p.ClassModel).Map(new ID(ConstantsVehicleModel.Fields.Class));
            SetProperty(p => p.Appearances).Map(new ID(ConstantsVehicleModel.Fields.AppearanceField));
        }
    }
}