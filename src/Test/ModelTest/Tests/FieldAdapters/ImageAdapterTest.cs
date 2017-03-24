using System;
using FluentAssertions;
using ModelTest.Constants;
using ModelTest.Utils;
using ModelTest.ViewModels;
using NSubstitute;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.FakeDb;
using Xunit;

namespace ModelTest.Tests.FieldAdapters
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

            using (var db = SharedDatabaseDefinition.CarDatabase())
            {
                var mediaProvider = Substitute.For<Sitecore.Resources.Media.MediaProvider>();
                mediaProvider.GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(i => i.ID.ToString() == ConstantsCarModel.Fields.MediaImageItem)).Returns(ConstantsCarModel.Fields.MediaImageItem);

                
                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = new CarViewModel(item);
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