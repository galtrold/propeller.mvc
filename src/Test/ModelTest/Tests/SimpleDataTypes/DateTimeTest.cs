using System;
using FluentAssertions;
using ModelTest.Constants;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Xunit;

namespace ModelTest.Tests.SimpleDataTypes
{
    public class DateTimeTest
    {
        [Fact]
        public void DateTime_type_success()
        {
            // Arrange 
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);


            using (var db = BasicDbScaffolding.SetupDatebaseWithSimpleFieldTypeAndValue(ConstantsCarModel.Fields.EnteredProductionDateField, "20161217T212000Z"))
            {
                // Act
                var item = db.GetItem("/sitecore/content/Ford500");
                var carViewModel = new CarViewModel(item);

                // Assert
                carViewModel.EnteredProductionDate.Date.Should().Be(17.December(2016));
            }
        }
    }
}