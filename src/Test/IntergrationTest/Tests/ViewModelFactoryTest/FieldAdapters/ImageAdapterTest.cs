using System;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using NSubstitute;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.FieldAdapters
{
    public class ImageAdapterTest
    {

        [Fact]
        public void ImageAdapter_Success()
        {
            // Arrange 
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);
            var factory = new ModelFactory();

            using (var db = SharedDatabaseDefinition.StarwarsDatabase())
            {
                using (ShareMediaProvider.MediaProvider())
                {
                    // Act
                    var item = db.GetItem("/sitecore/content/XWing");
                    var carViewModel = factory.Create<VehichleModel>(item);

                    // Assert
                    carViewModel.Photo.Alt.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Alt);
                    carViewModel.Photo.Url.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Url);
                }


            }
        }






    }
}