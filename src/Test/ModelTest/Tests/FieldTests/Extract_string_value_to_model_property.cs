using System;
using FluentAssertions;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Sitecore.AutoFixture.NSubstitute;
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
        public void Extract_field_value_as_string_and_write_it_to_model_property_Success(DatabaseFactory dbFactory, ModelFactory factory)
        {
            // Arrange
            var db = dbFactory.Create();
            var item = db.GetItem(DatabaseFactory.ItemInstances.CarItem);
            // Act

            var carViewModel = factory.Create<CarViewModel>(item);

            // Assert
            carViewModel.CarClass.ShouldBeEquivalentTo("Medium");
            carViewModel.ProductionCountry.Name.ShouldBeEquivalentTo("Germany");
                



        }

        [Theory]
        public void MatchIdentifiers_from_collection()
        {
            Assert.True(true);
        }
    }
}