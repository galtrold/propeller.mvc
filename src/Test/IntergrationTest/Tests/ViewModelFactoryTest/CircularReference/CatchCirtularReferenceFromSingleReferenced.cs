using System;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.CircularReference
{
    public class CatchCirtularReferenceFromSingleReferenced
    {
        [Fact]
        public void CircularReferenceBetweenTwoViewModels()
        {
            using (var db = SharedDatabaseDefinition.CpuDatabase())
            {
                // Arrange
                EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                var mappingProcessor = new MappingProcessor();
                mappingProcessor.Process(null);
                var factory = new ModelFactory();

                // Act
                var item = db.GetItem("/sitecore/content/Haswell");
                var haswellCpu = factory.Create<CpuModel>(item);

                // Assert
                haswellCpu.ArchitectureName.ShouldBeEquivalentTo("Haswell");
                haswellCpu.Predecessor.ArchitectureName.Should().Be("Ivy Bridge");
                haswellCpu.Predecessor.Successor.ArchitectureName.Should().Be("Haswell");

            }
        }
    }
}