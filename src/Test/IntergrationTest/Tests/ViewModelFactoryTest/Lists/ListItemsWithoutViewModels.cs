using System;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.Lists
{
    public class ListItemsWithoutViewModels
    {


        [Fact]
        public void GetIsFromAMultilist()
        {
            using (var db = SharedDatabaseDefinition.CpuListDatabase())
            {
                // Arrange
                EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var mappingProcessor = new MappingProcessor();
                mappingProcessor.Process(null);
                var factory = new ModelFactory();

                // Act
                var item = db.GetItem("/sitecore/content/IntelCps");
                var intelCpus = factory.Create<CpuSelectionModel>(item);

                // Assert
                intelCpus.SelectedCps.Split('|').Should().HaveCount(2);
            }
        }
    }
}