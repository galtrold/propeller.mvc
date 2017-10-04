using System;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.ComplexModels
{
    public class Get_referenced_viewmodels
    {
        [Fact]
        public void Get_a_single_reference_viewmodel_with_its_properties_populated_Success()
        {
            using (var db = SharedDatabaseDefinition.StarwarsDatabase())
            {
                // Arrange
                EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var mappingProcessor = new MappingProcessor();
                mappingProcessor.Process(null);
                var factory = new ModelFactory();
                
                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = factory.Create<VehichleModel>(item);

                // Assert
                carViewModel.ClassModel.Name.Should().Be(SharedDatabaseDefinition.StaticVehichleData.ClassModel.Name);
               

            }

        }
        
    }
}