using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace IntergrationTest.Models.Maps
{
    public class MovieModelMap : ConfigurationMap<MovieModel>
    {
        public MovieModelMap()
        {
            SetProperty(p => p.Title).Map(new ID(ConstantsMovieModel.Fields.TitleField)).Include();
            SetProperty(p => p.ReleaseDate).Map(new ID(ConstantsMovieModel.Fields.ReleaseDateField)).Include();
        }
    }
}