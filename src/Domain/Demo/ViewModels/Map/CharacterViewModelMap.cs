using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Demo.ViewModels.Map
{
    public class CharacterViewModelMap : ConfigurationMap<CharacterViewModel>
    {
        public CharacterViewModelMap()
        {

            SetProperty(p => p.Affiliation).Map(new ID("")).Include().Editable();

        }
    }
}