using Propeller.Mvc.Model;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class WeaponViewModel : PropellerModel<WeaponViewModel>
    {

        public string WeaponType { get; set; }

        public int DamageValue { get; set; }

    }
}