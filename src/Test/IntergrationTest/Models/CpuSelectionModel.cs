using Propeller.Mvc.Model;

namespace IntergrationTest.Models
{
    public class CpuSelectionModel : PropellerModel<CpuSelectionModel>
    {
        public string SelectedCps { get; set; }
    }
}