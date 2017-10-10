using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace IntergrationTest.Models.Maps
{
    public class VehicleClassModelMap : ConfigurationMap<VehicleClassModel>
    {
        public VehicleClassModelMap()
        {

            SetProperty(p => p.Name).Map(new ID(ConstantVehicleClassModel.Fields.ClassNameField)).Include();
        }
    }
}