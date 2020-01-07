using Propeller.Mvc.Model;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class ArmorViewModel : PropellerModel<ArmorViewModel>
    {

        public string BodyPart { get; set; }

        public int ProtectionValue { get; set; }
        
    }
}