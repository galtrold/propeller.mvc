using System;
using FluentAssertions;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace ModelTest.Tests.ViewModelFactoryTest.ComplexModels
{
    public class Get_referenced_viewmodels
    {
        [Fact]
        public void Get_a_single_reference_viewmodel_with_its_properties_populated_Success()
        {
            using (var db = SharedDatabaseDefinition.CarDatabase())
            {
                // Arrange
                EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var mappingProcessor = new MappingProcessor();
                mappingProcessor.Process(null);
                var factory = new ModelFactory();
                
                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = factory.Create<CarViewModel>(item);

                // Assert
                carViewModel.ProductionCountry.Name.Should().Be(SharedDatabaseDefinition.StaticCarData.ProductionCountry.Name);
                carViewModel.ProductionCountry.Currency.Should().Be(SharedDatabaseDefinition.StaticCarData.ProductionCountry.Currency);
                carViewModel.ProductionCountry.DieselTax.Should().Be(SharedDatabaseDefinition.StaticCarData.ProductionCountry.DieselTax);
                carViewModel.ProductionCountry.PetrolTax.Should().Be(SharedDatabaseDefinition.StaticCarData.ProductionCountry.PetrolTax);

            }

        }
        
    }
}