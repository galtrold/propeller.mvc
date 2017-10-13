using System.Collections.Generic;
using Propeller.Mvc.Model;
using Propeller.Mvc.Model.Adapters;
using Image = Propeller.Mvc.Model.Adapters.Image;

namespace IntergrationTest.Models
{
    public class VehichleModel : PropellerModel<VehichleModel>
    {

        public string Name { get; set; }
        
        public double Length { get; set; }

        public int CargoKg { get; set; }

        public bool HasHyperdrive { get; set; }
        
        public GeneralLink WikiLink { get; set; }

        public Image Photo { get; set; }
        
        public VehicleClassModel ClassModel { get; set; }

        public IEnumerable<MovieModel> Appearances { get; set; }


    }
}
