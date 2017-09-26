using NSubstitute;
using Ploeh.AutoFixture;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace ModelTest
{
    public class DatabaseFactory
    {   
        public Database Create()
        {
            var db = Substitute.For<Database>();
            var language = Substitute.For<Language>();
            var definition = new ItemDefinition(ID.NewID, "itemname", ID.NewID, ID.NewID);
            var data = new ItemData(definition, language, Sitecore.Data.Version.First, new FieldList());
            var rootItem = Substitute.For<Item>(ID.NewID, data, db);

            // Field collections
            var carFieldCollection = Substitute.For<FieldCollection>(rootItem);
            carFieldCollection
                .AddField(Constants.ConstantsCarModel.Fields.ProductionCountry,
                    $"<link text=\"Slave One\" anchor=\"\" linktype=\"internal\" class=\"\" title=\"\" target=\"\" querystring=\"\" id=\"{ItemInstances.CountryItem}\" />", rootItem)
                .AddField(Constants.ConstantsCarModel.Fields.CarClassField, "Medium", rootItem)
                .AddField(Constants.ConstantsCarModel.Fields.CarModelField, "Astra", rootItem)
                .AddField(Constants.ConstantsCarModel.Fields.ManuFactureField, "Opel", rootItem)
                .AddField(Constants.ConstantsCarModel.Fields.IsTurboCharged, "1", rootItem)
                .AddField(Constants.ConstantsCarModel.Fields.ExternalWikiLink, "http://site.com", rootItem)
                .AddField(Constants.ConstantsCarModel.Fields.EnteredProductionDateField, "20161217T212000Z", rootItem);

            var countryFieldCollection = Substitute.For<FieldCollection>(rootItem)
                .AddField(Constants.ConstantsCountryModel.Fields.CurrencyField, "euro", rootItem)
                .AddField(Constants.ConstantsCountryModel.Fields.DieselTaxField, "0.3", rootItem)
                .AddField(Constants.ConstantsCountryModel.Fields.PetrolTaxField, "0.1", rootItem)
                .AddField(Constants.ConstantsCountryModel.Fields.NameField, "Germany", rootItem);
            
            // Items
            var carItem = Substitute.For<Item>(ItemInstances.CarItem, data,  db);
            carItem.Fields.Returns(carFieldCollection);
            var countryItem = Substitute.For<Item>(ItemInstances.CountryItem, data, db);
            countryItem.Fields.Returns(countryFieldCollection);

            // Database
            
            db.GetItem(ItemInstances.CarItem).Returns(carItem);
            db.GetItem(ItemInstances.CountryItem).Returns(countryItem);

            return db;
        }

        public struct ItemInstances
        {
            public static readonly ID CarItem = new ID("{5291D199-BBD6-4DA5-9FFD-09BAEB972008}");
            public static readonly ID CountryItem = new ID("{62689EC0-27CE-4612-80A4-C0B898B0CE50}");
        }
        }
}