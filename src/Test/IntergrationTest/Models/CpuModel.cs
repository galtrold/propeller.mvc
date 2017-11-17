using Propeller.Mvc.Model;

namespace IntergrationTest.Models
{
    public class CpuModel : PropellerModel<CpuModel>
    {
        public string ArchitectureName { get; set; }

        public CpuModel Predecessor { get; set; }

        public CpuModel Successor { get; set; }
    }
}