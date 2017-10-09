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
                var mediaProvider = Substitute.For<Sitecore.Resources.Media.MediaProvider>();
                mediaProvider.GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(p => true )).Returns(SharedDatabaseDefinition.StaticVehichleData.Photo.Url);

                
                // Act
                var item = db.GetItem("/sitecore/content/XWing");
                var carViewModel = factory.Create<VehichleModel>(item);
                using (new Sitecore.FakeDb.Resources.Media.MediaProviderSwitcher(mediaProvider))
                {
                    carViewModel.Photo = carViewModel.GetAs<Image>(p => p.Photo);
                    // Assert
                    carViewModel.Photo.Alt.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Alt);
                    carViewModel.Photo.Url.Should().Be(SharedDatabaseDefinition.StaticVehichleData.Photo.Url);
                }
                
                
                
            }
        }


     



    }
}