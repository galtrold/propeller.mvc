using System;
using System.Collections.Generic;
using IntergrationTest.Constants;
using IntergrationTest.Models;
using NSubstitute;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;

namespace IntergrationTest.Utils
{
    public class SharedDatabaseDefinition
    {
        public static VehichleModel StaticVehichleData { get; set; }

        static SharedDatabaseDefinition()
        {
            StaticVehichleData = new VehichleModel()
            {
                Name = "X-wing",
                Length = 12.5,
                CargoKg = 110,
                HasHyperdrive= true,
                WikiLink = new GeneralLink() { Desciption = "Star wars wiki", LinkType = "external", Url = "http://starwars.wikia.com/wiki/T-65_X-wing_starfighter" },
                Photo = new Image() { Alt = "X-Wing", Url = "~/media/xwing.ashx" },
                ClassModel = new VehicleClassModel() { Name = "Fighter"},
                Appearances = new List<MovieModel>()
                {
                    new MovieModel() { ReleaseDate = new DateTime(1977, 12, 30, 6, 6, 6), Title = "A new hope"},
                    new MovieModel() { ReleaseDate = new DateTime(1980, 12, 30, 6, 6, 6), Title = "Empire strikes back"}
                }
                
            };


        }

        public static Db StarwarsDatabase()
        {
          


            var db = new Db()
            {
                // Templates
                new DbTemplate("vehicle", ConstantsVehicleModel.Templates.VehiclTemplateId)
                {
                    {new ID(ConstantsVehicleModel.Fields.NameField), StaticVehichleData.Name },
                    {new ID(ConstantsVehicleModel.Fields.LengthField), StaticVehichleData.Length.ToString()},
                    {new ID(ConstantsVehicleModel.Fields.CargoField), StaticVehichleData.CargoKg.ToString() },
                    {new ID(ConstantsVehicleModel.Fields.HasHyperdrive), StaticVehichleData.HasHyperdrive ? "1": "0"},
                    {new ID(ConstantsVehicleModel.Fields.ExternalWikiLink), $"<link text=\"{StaticVehichleData.WikiLink.Desciption}\" linktype=\"{StaticVehichleData.WikiLink.LinkType}\" url=\"{StaticVehichleData.WikiLink.Url}\" anchor=\"\" target=\"_blank\" />"},
                    {new ID(ConstantsVehicleModel.Fields.PhotoField), $"<image mediapath=\"\" alt=\"{StaticVehichleData.Photo.Alt}\" width=\"\" height=\"\" hspace=\"\" vspace=\"\" showineditor=\"\" usethumbnail=\"\" src=\"\" mediaid=\"{ConstantsVehicleModel.Fields.MediaImageItem}\" />"},
                    {new ID(ConstantsVehicleModel.Fields.Class), ConstantVehicleClassModel.Instances.Fighter.ToString()},
                    {new ID(ConstantsVehicleModel.Fields.AppearanceField), $"{ConstantsMovieModel.Instances.ANewHope}|{ConstantsMovieModel.Instances.EmpireStrikesBack}"}

                },
                new DbTemplate("movie", ConstantsMovieModel.Templates.MovieTemplateId)
                {
                    {new ID(ConstantsMovieModel.Fields.TitleField)},
                    {new ID(ConstantsMovieModel.Fields.ReleaseDateField ) }
                },
                new DbTemplate("VehicleClass", Constants.ConstantVehicleClassModel.Templates.VehicleClassTemplateId)
                {
                    {new ID(ConstantVehicleClassModel.Fields.NameField)},
                },
                // Items
                new DbItem("XWing", ConstantsVehicleModel.Instances.XWing, ConstantsVehicleModel.Templates.VehiclTemplateId),
                //new DbItem("ANewHope", ConstantsMovieModel.Instances.ANewHope, ConstantsMovieModel.Templates.MovieTemplateId),
                //new DbItem("EmpireStrikesBack", ConstantsMovieModel.Instances.EmpireStrikesBack, ConstantsMovieModel.Templates.MovieTemplateId),
                //new DbItem("Fighter", ConstantVehicleClassModel.Instances.Fighter, ConstantVehicleClassModel.Templates.VehicleClassTemplateId)

            };
            return db;
        }

    }
   
}