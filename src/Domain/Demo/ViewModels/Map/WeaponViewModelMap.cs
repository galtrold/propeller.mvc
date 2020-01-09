using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Propeller.Mvc.Demo.ViewModels.Map
{
    public class WeaponViewModelMap : ConfigurationMap<WeaponViewModel>
    {
        public WeaponViewModelMap()
        {
            SetProperty(p => p.WeaponType).Map(new ID("{E0D6AE69-032C-48DC-977C-69282475B825}")).Include();
            SetProperty(p => p.DamageValue).Map(new ID("{BBC0AB3A-5B9C-4098-A9AF-32BE463EBE0B}")).Include();
            ImportConfiguration<ICombatId>();
        }
    }
}