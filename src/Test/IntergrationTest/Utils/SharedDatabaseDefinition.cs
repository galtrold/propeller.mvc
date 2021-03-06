﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                    {new ID(ConstantVehicleClassModel.Fields.ClassNameField)},
                },
                // Items
                new DbItem("XWing", ConstantsVehicleModel.Instances.XWing, ConstantsVehicleModel.Templates.VehiclTemplateId),
                new DbItem("ANewHope", ConstantsMovieModel.Instances.EmpireStrikesBack, ConstantsMovieModel.Templates.MovieTemplateId)
                {
                    new DbField(new  ID(ConstantsMovieModel.Fields.TitleField)){ Value = StaticVehichleData.Appearances.First().Title},
                    new DbField(new  ID(ConstantsMovieModel.Fields.ReleaseDateField)){ Value = StaticVehichleData.Appearances.First().ReleaseDate.ToString("yyyyMMddTHHmmssZ")},
                },
                new DbItem("EmpireStrikesBack", ConstantsMovieModel.Instances.ANewHope, ConstantsMovieModel.Templates.MovieTemplateId)
                {
                    new DbField(new  ID(ConstantsMovieModel.Fields.TitleField)){ Value = StaticVehichleData.Appearances.Last().Title},
                    new DbField(new  ID(ConstantsMovieModel.Fields.ReleaseDateField)){ Value = StaticVehichleData.Appearances.Last().ReleaseDate.ToString("yyyyMMddTHHmmssZ")},
                },
                new DbItem("Fighter", ConstantVehicleClassModel.Instances.Fighter, ConstantVehicleClassModel.Templates.VehicleClassTemplateId)
                {
                    new DbField(new ID(ConstantVehicleClassModel.Fields.ClassNameField)){Value = StaticVehichleData.ClassModel.Name}
                }
                
                    
                

            };
            return db;
        }

        public static Db CpuDatabase()
        {
            var db = new Db()
            {
                // Templates
                new DbTemplate("CpuModel", ConstantsCpuModel.Templates.CpuTemplateId)
                {
                    ConstantsCpuModel.Fields.ArchitectureNameField,
                    ConstantsCpuModel.Fields.Predecessor,
                    ConstantsCpuModel.Fields.Successor
                },
                // Items
               
                new DbItem("Haswell", ConstantsCpuModel.Instances.Hasswell, ConstantsCpuModel.Templates.CpuTemplateId)
                {
                    new DbField(ConstantsCpuModel.Fields.ArchitectureNameField){ Value = "Haswell"},
                    new DbField(ConstantsCpuModel.Fields.Predecessor){ Value = ConstantsCpuModel.Instances.IvyBridge.ToString() },
                    new DbField(ConstantsCpuModel.Fields.Successor){ Value = ConstantsCpuModel.Instances.Hasswell.ToString() }
                },
                new DbItem("IvyBridge", ConstantsCpuModel.Instances.IvyBridge, ConstantsCpuModel.Templates.CpuTemplateId)
                {
                    new DbField(ConstantsCpuModel.Fields.ArchitectureNameField){ Value = "Ivy Bridge"},
                    new DbField(ConstantsCpuModel.Fields.Successor){ Value = ConstantsCpuModel.Instances.Hasswell.ToString()},
                    new DbField(ConstantsCpuModel.Fields.Predecessor){ Value = ConstantsCpuModel.Instances.IvyBridge.ToString() }
                }
            };
            return db;
        }

        public static Db CpuListDatabase()
        {
            var db = new Db()
            {
                // Templates
                new DbTemplate("CpuModel", ConstantsCpuModel.Templates.CpuTemplateId)
                {
                    ConstantsCpuModel.Fields.ArchitectureNameField,
                    ConstantsCpuModel.Fields.Predecessor,
                    ConstantsCpuModel.Fields.Successor,
                    ConstantsCpuModel.Fields.InstrunctionCount
                },
                new DbTemplate("CpuSelection", ConstantsCpuModel.Templates.CpuSelectionTemplateId)
                {
                    ConstantsCpuModel.Fields.ChosenCpus
                },
                // Items
               
                new DbItem("Haswell", ConstantsCpuModel.Instances.Hasswell, ConstantsCpuModel.Templates.CpuTemplateId)
                {
                    new DbField(ConstantsCpuModel.Fields.ArchitectureNameField){ Value = "Haswell"}
                },
                new DbItem("IvyBridge", ConstantsCpuModel.Instances.IvyBridge, ConstantsCpuModel.Templates.CpuTemplateId)
                {
                    new DbField(ConstantsCpuModel.Fields.ArchitectureNameField){ Value = "Ivy Bridge"}
                },
                new DbItem("IntelCps", ConstantsCpuModel.Instances.IntelCpuSelection, ConstantsCpuModel.Templates.CpuSelectionTemplateId)
                {
                    new DbField(ConstantsCpuModel.Fields.ChosenCpus){ Value = $"{ConstantsCpuModel.Instances.Hasswell}|{ConstantsCpuModel.Instances.IvyBridge}"}
                }
            };
            return db;
        }

    }
   
}