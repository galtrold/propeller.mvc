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
    public class StringIncludeTest
    {


        [Fact]
        public void String_type_Success()
        {
            // Arrange 
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);



            using (var db = BasicDbScaffolding.SetupDatebaseWithSimpleFieldTypeAndValue(ConstantsCarModel.Fields.ManuFactureField, "Ford"))
            {
                // Act
                var item = db.GetItem("/sitecore/content/Ford500");
                var carViewModel = new CarViewModel(item);

                // Assert
                carViewModel.Manufacture.Should().Be("Ford");
            }
        }

    }
}