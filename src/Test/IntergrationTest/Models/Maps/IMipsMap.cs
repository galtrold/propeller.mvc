using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;

namespace IntergrationTest.Models.Maps
{
    public class IMipsMap : ConfigurationMap<IMips>
    {
        public IMipsMap()
        {
            SetProperty(p => p.InstructionCount).Map(ConstantsCpuModel.Fields.InstrunctionCount).Include();
        }
    }
}