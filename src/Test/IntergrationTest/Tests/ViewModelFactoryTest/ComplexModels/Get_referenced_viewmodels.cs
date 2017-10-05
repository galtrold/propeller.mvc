using System;
using FluentAssertions;
using IntergrationTest.Constants;
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
                using (ShareMediaProvider.MediaProvider())
                {

                    // Arrange
                    EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    var mappingProcessor = new MappingProcessor();
                    mappingProcessor.Process(null);
                    var factory = new ModelFactory();

                    // Act
                    var item = db.GetItem("/sitecore/content/XWing");
                    var xwingViewModel = factory.Create<VehichleModel>(item);

                    // Assert
                    xwingViewModel.Name.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Name);
                    xwingViewModel.Length.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Length);
                    xwingViewModel.CargoKg.Should().Be(SharedDatabaseDefinition.StaticVehichleData.CargoKg);
                    xwingViewModel.HasHyperdrive.Should().Be(SharedDatabaseDefinition.StaticVehichleData.HasHyperdrive);

                    xwingViewModel.WikiLink.Desciption.Should().Be(SharedDatabaseDefinition.StaticVehichleData.WikiLink.Desciption);
                    xwingViewModel.WikiLink.Url.Should().Be(SharedDatabaseDefinition.StaticVehichleData.WikiLink.Url);
                    xwingViewModel.WikiLink.LinkType.Should().Be(SharedDatabaseDefinition.StaticVehichleData.WikiLink.LinkType);

                    xwingViewModel.Photo.Url.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Url);
                    xwingViewModel.Photo.Url.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Url);
                    xwingViewModel.Photo.Alt.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Alt);



                }
            }

        }

    }
}