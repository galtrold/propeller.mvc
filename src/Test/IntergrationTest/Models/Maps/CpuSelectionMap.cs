using Propeller.Mvc.Core.Mapping;

namespace IntergrationTest.Models.Maps
{
    public class CpuSelectionMap : ConfigurationMap<CpuSelectionModel>
    {
        public CpuSelectionMap()
        {
            SetProperty(p => p.SelectedCps).Map(Constants.ConstantsCpuModel.Fields.ChosenCpus).Include();
        }
    }
}