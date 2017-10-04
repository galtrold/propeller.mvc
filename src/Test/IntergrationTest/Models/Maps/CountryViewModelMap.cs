using IntergrationTest.Constants;
using Propeller.Mvc.Core.Mapping;
using Sitecore.Data;

namespace IntergrationTest.Models.Maps
{
    public class MovieModelMap : ConfigurationMap<MovieModel>
    {
        public MovieModelMap()
        {
            //SetProperty(p => p.).Map(new ID(ConstantsMovieModel.Fields.TitleField)).Include();
            //SetProperty(p => p.Currency).Map(new ID(ConstantsMovieModel.Fields.ReleaseDateField)).Include();
            //SetProperty(p => p.DieselTax).Map(new ID(ConstantsMovieModel.Fields.DieselTaxField)).Include();
            //SetProperty(p => p.PetrolTax).Map(new ID(ConstantsMovieModel.Fields.PetrolTaxField)).Include();
        }
    }
}