using System;
using FluentAssertions;
using ModelTest.Constants;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.FakeDb;
using Xunit;

namespace ModelTest.Tests.FieldAdapters
{
    public class GeneralLinkAdapterTest
    {
        [Fact]
        public void GeneralLinkAdapter_ExternalLink_Success()
        {
            // Arrange 
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);

            var generalLinkValue =
                "<link text=\"Star wars wiki\" linktype=\"external\" url=\"http://starwars.wikia.com/wiki/Boba_Fett\" anchor=\"\" target=\"_blank\" />";

            using (var db = new Db()
            {
                new DbTemplate("car", ConstantsCarModel.Templates.CarTemplateId)
                {
                    {new ID(ConstantsCarModel.Fields.ExternalWikiLink), generalLinkValue}
                },
                new DbItem("Ford500", ID.NewID, ConstantsCarModel.Templates.CarTemplateId),

            })
            {

                // Act
                var item = db.GetItem("/sitecore/content/Ford500");
                var carViewModel = new CarViewModel(item);
                carViewModel.WikiLink = carViewModel.GetAs<GeneralLink>(p => p.WikiLink);

                // Assert
                carViewModel.WikiLink.Url.Should().Be("http://starwars.wikia.com/wiki/Boba_Fett");
                carViewModel.WikiLink.Desciption.Should().Be("Star wars wiki");


            }
        }

    }
}