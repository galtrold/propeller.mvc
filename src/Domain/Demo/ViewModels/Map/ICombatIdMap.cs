using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Propeller.Mvc.Demo.ViewModels.Map
{
    public class ICombatIdMap : ConfigurationMap<ICombatId>
    {
        public ICombatIdMap()
        {
            SetProperty(p => p.Category).Map(new ID("{B69BED8B-4C31-4455-BF30-3B1C21B9FE4F}")).Include();
        }
    }
}