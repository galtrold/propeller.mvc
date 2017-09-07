using System;
using FluentAssertions;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace ModelTest.ViewModelFactoryTest.SimpleDataTypes
{
    public class StringIncludeTest
    {


        [Fact]
        public void String_type_Success()
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

                // Assert
                carViewModel.Manufacture.Should().Be(SharedDatabaseDefinition.StaticCarData.Manufacture);
            }
        }

    }
}