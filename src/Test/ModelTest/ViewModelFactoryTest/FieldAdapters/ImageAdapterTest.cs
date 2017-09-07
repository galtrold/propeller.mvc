using System;
using FluentAssertions;
using ModelTest.Utils;
using ModelTest.ViewModels;
using NSubstitute;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace ModelTest.ViewModelFactoryTest.FieldAdapters
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

            using (var db = SharedDatabaseDefinition.CarDatabase())
            {   
                var mediaProvider = Substitute.For<Sitecore.Resources.Media.MediaProvider>();
                mediaProvider.GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(p => true )).Returns(SharedDatabaseDefinition.StaticCarData.CarPhoto.Url);

                
                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = factory.Create<CarViewModel>(item);
                using (new Sitecore.FakeDb.Resources.Media.MediaProviderSwitcher(mediaProvider))
                {
                    carViewModel.CarPhoto = carViewModel.GetAs<Image>(p => p.CarPhoto);
                    // Assert
                    carViewModel.CarPhoto.Alt.Should().Be(SharedDatabaseDefinition.StaticCarData.CarPhoto.Alt);
                    carViewModel.CarPhoto.Url.Should().Be(SharedDatabaseDefinition.StaticCarData.CarPhoto.Url);
                }
                
                
                
            }
        }


     



    }
}