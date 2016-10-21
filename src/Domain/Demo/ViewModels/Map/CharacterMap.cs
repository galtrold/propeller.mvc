using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Propeller.Mvc.Demo.ViewModels.Map
{
    public class CharacterMap : ConfigurationMap<CharacterViewModel>
    {
        public CharacterMap()
        {
            SetProperty(p => p.Species).Map(new ID("{FF80B838-B0E4-4266-9E6A-2918585C4EB7}")).Include().Editable();
            SetProperty(p => p.Genger).Map(new ID("{7079E001-680A-460C-BB86-91E31C3EA2A5}")).Include().Editable();
            SetProperty(p => p.Occupation).Map(new ID("{73FF41AA-2ACF-45F8-9FA3-8C4F6374F217}")).Include().Editable();
            SetProperty(p => p.Affiliation).Map(new ID("{EE444D4A-7FDF-4D94-8ABA-6054A898E721}")).Include().Editable();
            SetProperty(p => p.Homeworld).Map(new ID("{E8D236D1-E473-4816-89BD-EE4CCD613972}")).Include().Editable();
            SetProperty(p => p.Photo).Map(new ID("{ADB6E999-106A-4611-B3B0-51E442FD4A58}")).Include().Editable();
            SetProperty(p => p.PrimaryTransporation).Map(new ID("{4D888B1E-8B88-4594-8B1B-6082BCDCFEFF}")).Include().Editable();
            SetProperty(p => p.ExternalLink).Map(new ID("{D64C6352-DA90-4DDE-9A9A-31E154E9905A}")).Include().Editable();
        }
    }
}