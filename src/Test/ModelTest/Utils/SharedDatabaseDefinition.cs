using System;
using ModelTest.Constants;
using ModelTest.ViewModels;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.FakeDb;

namespace ModelTest.Utils
{
    public class SharedDatabaseDefinition
    {
        public static CarViewModel StaticCarData { get; set; }

        static SharedDatabaseDefinition()
        {
            StaticCarData = new CarViewModel()
            {
                CarClass = "C",
                CarModel = "Astra",
                CarPhoto = new Image() {Alt = "Sports Tourer", Url = "~/media/astrasportstourer.ashx"},
                DataItem = null,
                EnteredProductionDate = new DateTime(2016, 12, 30, 6, 6, 6),
                Manufacture = "Opel",
                WikiLink = new GeneralLink() { Desciption = "Car of the year", Url = "opel.dk"},
                ProductionCountry = new CountryViewModel() { Currency = "euro", Name = "Germany", PetrolTax = "6.75", DieselTax = "15.44"},
                IsActive = true
            };


        }

        public static Db CarDatabase()
        {
            var db = new Db()
            {
                new DbTemplate("car", ConstantsCarModel.Templates.CarTemplateId)
                {
                    {new ID(ConstantsCarModel.Fields.CarClassField), StaticCarData.CarClass },
                    {new ID(ConstantsCarModel.Fields.IsActive), "1" },
                    {new ID(ConstantsCarModel.Fields.CarModelField), StaticCarData.CarModel },
                    {new ID(ConstantsCarModel.Fields.CarPhoto), $"<image mediapath=\"\" alt=\"{StaticCarData.CarPhoto.Alt}\" width=\"\" height=\"\" hspace=\"\" vspace=\"\" showineditor=\"\" usethumbnail=\"\" src=\"\" mediaid=\"{ConstantsCarModel.Fields.MediaImageItem}\" />"},
                    {new ID(ConstantsCarModel.Fields.EnteredProductionDateField), Sitecore.DateUtil.ToIsoDate(StaticCarData.EnteredProductionDate)},
                    {new ID(ConstantsCarModel.Fields.ManuFactureField), StaticCarData.Manufacture },
                    {new ID(ConstantsCarModel.Fields.ExternalWikiLink), $"<link text=\"{StaticCarData.WikiLink.Desciption}\" linktype=\"external\" url=\"{StaticCarData.WikiLink.Url}\" anchor=\"\" target=\"_blank\" />" },
                    {new DbLinkField(ConstantsCarModel.Fields.ProductionCountry)
                        {
                           LinkType = "internal",
                           Text = "Germany",
                           Title = "Opel HQ is placed in Germany",
                           TargetID = Constants.ConstantsCountryModel.LinkItems.CountryItem
                        }
                    }
                },

                new DbTemplate("country", ConstantsCountryModel.Templates.CountryTemplateId)
                {
                    
                },

                new DbItem("Astra", ID.NewID, ConstantsCarModel.Templates.CarTemplateId),
                new DbItem("Germany", ConstantsCountryModel.LinkItems.CountryItem, ConstantsCountryModel.Templates.CountryTemplateId)
            };
            return db;
        }

    }
   
}