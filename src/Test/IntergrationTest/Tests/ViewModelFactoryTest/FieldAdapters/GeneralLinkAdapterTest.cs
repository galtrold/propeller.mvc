using System;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.FieldAdapters
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

            using (var db = SharedDatabaseDefinition.StarwarsDatabase())
            {

                // Act
                var item = db.GetItem("/sitecore/content/XWing");
                var carViewModel = factory.Create<VehichleModel>(item);

                // Assert
                carViewModel.WikiLink.Url.Should().Be(SharedDatabaseDefinition.StaticVehichleData.WikiLink.Url);
                carViewModel.WikiLink.Desciption.Should().Be(SharedDatabaseDefinition.StaticVehichleData.WikiLink.Desciption);


            }
        }

    }
}