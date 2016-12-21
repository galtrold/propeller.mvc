using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Propeller.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace PresentationTest.ViewModels
{
    public class CarViewModel : PropellerViewModel<CarViewModel>
    {

        public string Manufacture { get; set; }
        public string CarModel { get; set; }
        public string CarClass { get; set; }

        public CarViewModel()
        {
        }

        public CarViewModel(Item dataItem) : base(dataItem)
        {
        }

        public CarViewModel(Rendering rendering) : base(rendering)
        {
        }
    }
}
