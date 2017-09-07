using System;
using FluentAssertions;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace ModelTest.ViewModelFactoryTest.FieldAdapters
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
            var factory = new ModelFactory();

            using (var db = SharedDatabaseDefinition.CarDatabase())
            {

                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = factory.Create<CarViewModel>(item);
                carViewModel.WikiLink = carViewModel.GetAs<GeneralLink>(p => p.WikiLink);

                // Assert
                carViewModel.WikiLink.Url.Should().Be(SharedDatabaseDefinition.StaticCarData.WikiLink.Url);
                carViewModel.WikiLink.Desciption.Should().Be(SharedDatabaseDefinition.StaticCarData.WikiLink.Desciption);


            }
        }

    }
}