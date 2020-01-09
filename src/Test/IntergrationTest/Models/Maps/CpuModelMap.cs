using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;

namespace IntergrationTest.Models.Maps
{
    public class CpuModelMap : ConfigurationMap<CpuModel>
    {
        public CpuModelMap()
        {
            SetProperty(p => p.ArchitectureName).Map(ConstantsCpuModel.Fields.ArchitectureNameField).Include();
            SetProperty(p => p.Predecessor).Map(ConstantsCpuModel.Fields.Predecessor).Include();
            SetProperty(p => p.Successor).Map(ConstantsCpuModel.Fields.Successor).Include();
            ImportConfiguration<IMips>();
        }
    }
}