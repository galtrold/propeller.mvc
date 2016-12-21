using System;
using FluentAssertions;
using ModelTest.Constants;
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
            const string MyImageUrl = "~/media/myimage.ashx";
            var imageItemValue =
                "<image mediapath=\"\" alt=\"ford-500\" width=\"\" height=\"\" hspace=\"\" vspace=\"\" showineditor=\"\" usethumbnail=\"\" src=\"\" mediaid=\"{5EA35D30-EF21-4217-AFE8-D71F5867DCC7}\" />";

            var mediaItemId = new ID("{5EA35D30-EF21-4217-AFE8-D71F5867DCC7}");


            using (var db = new Db()
            {
                new DbTemplate("car", ConstantsCarModel.Templates.CarTemplateId)
                {
                    {new ID(ConstantsCarModel.Fields.CarPhoto), imageItemValue },
                },
                new DbItem("Ford500", ID.NewID, ConstantsCarModel.Templates.CarTemplateId),
                new DbItem("photoMediaItem", mediaItemId)
            })
            {
                
                var mediaProvider = NSubstitute.Substitute.For<Sitecore.Resources.Media.MediaProvider>();
                mediaProvider.GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(i => i.ID == mediaItemId)).Returns(MyImageUrl);

                
                // Act
                var item = db.GetItem("/sitecore/content/Ford500");
                var carViewModel = new CarViewModel(item);
                using (new Sitecore.FakeDb.Resources.Media.MediaProviderSwitcher(mediaProvider))
                {
                    carViewModel.CarPhoto = carViewModel.GetAs<Image>(p => p.CarPhoto);
                    // Assert
                    carViewModel.CarPhoto.Alt.Should().Be("ford-500");
                    carViewModel.CarPhoto.Url.Should().Be("~/media/myimage.ashx");
                }
                
                
                
            }
        }


     



    }
}