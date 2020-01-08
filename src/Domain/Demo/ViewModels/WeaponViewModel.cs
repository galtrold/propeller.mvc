using Propeller.Mvc.Model;

namespace Propeller.Mvc.Demo.ViewModels
{
    public class WeaponViewModel : PropellerModel<WeaponViewModel>, ICombatId
    {

        public string WeaponType { get; set; }

        public int DamageValue { get; set; }

        public string Category { get; set; }
    }
}