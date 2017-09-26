using System;
using FluentAssertions;
using ModelTest.Constants;
using ModelTest.Utils;
using ModelTest.ViewModels;
using NSubstitute;
using NSubstitute.Extensions;
using Ploeh.AutoFixture;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Sitecore.AutoFixture.NSubstitute;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Xunit;

namespace ModelTest.Tests.FieldTests
{
    public class Extract_string_value_to_model_property
    {
        public Extract_string_value_to_model_property()
        {
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);
        }

        [Theory, AutoSitecoreData]
        public void Extract_field_value_as_string_and_write_it_to_model_property_Success(Item item, Item prodItem,Field field, Field production, FieldCollection collection, ModelFactory factory, string dummyClass, string country)
        {
            // Arrange


            production.Value.Returns(
                "<link text=\"Germany\" anchor=\"\" linktype=\"internal\" class=\"\" title=\"Opel HQ is placed in Germany\" target=\"\" querystring=\"\" id=\"{ConstantsCountryModel.LinkItems.CountryItem}\" />");
            field.Value.Returns(dummyClass);
           
            collection[new ID(ConstantsCarModel.Fields.CarClassField)].Returns(field);
            collection[new ID(ConstantsCarModel.Fields.ProductionCountry)].Returns(production);
            item.Database.GetItem(ConstantsCountryModel.LinkItems.CountryItem).Returns(prodItem);
            item.Fields.Returns(collection);

            // Act
            var carViewModel = factory.Create<CarViewModel>(item);

            // Assert
            carViewModel.CarClass.ShouldBeEquivalentTo(dummyClass);
            carViewModel.ProductionCountry.Name.ShouldBeEquivalentTo(country);
                



        }
    }
}