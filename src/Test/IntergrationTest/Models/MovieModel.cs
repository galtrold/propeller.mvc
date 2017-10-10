using System;
using Propeller.Mvc.Model;

namespace IntergrationTest.Models
{
    public class MovieModel : PropellerModel<MovieModel>
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}