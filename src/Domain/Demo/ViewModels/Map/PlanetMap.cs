using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace Propeller.Mvc.Demo.ViewModels.Map
{
    public class PlanetMap : ConfigurationMap<PlanetViewModel>
    {
        public PlanetMap()
        {
            SetProperty(p => p.Region).Map(new ID("{E9F01DD2-1D56-424E-84D1-8730AA38E5F2}")).Editable();
            SetProperty(p => p.Moons).Map(new ID("{FCE0331E-D362-4FA7-80CC-4B892EF0EA72}")).Editable();
            SetProperty(p => p.SurfaceWater).Map(new ID("{A3FFEB37-56C1-4DE0-8F12-FDC14C3C8023}")).Editable();
            SetProperty(p => p.Population).Map(new ID("{3BDF4B87-443E-44C5-A282-3118E96D64AC}")).Editable();
            SetProperty(p => p.Affiliation).Map(new ID("{D0AAF369-02C5-468B-8DE7-5CEC8344F33F}")).Editable();
            SetProperty(p => p.Photo).Map(new ID("{3B427930-C49A-4736-AC49-FFA095EC0D49}")).Include().Editable();

        }
    }
}