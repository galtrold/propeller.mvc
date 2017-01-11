﻿using System;
using FluentAssertions;
using ModelTest.Constants;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Xunit;

namespace ModelTest.Tests.SimpleDataTypes
{
    public class BooleanIncludeTest
    {


        [Fact]
        public void boolean_is_true_Success()
        {
            // Arrange 
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);



            using (var db = BasicDbScaffolding.SetupDatebaseWithSimpleFieldTypeAndValue(ConstantsCarModel.Fields.IsActive, "1"))
            {
                // Act
                var item = db.GetItem("/sitecore/content/Ford500");
                var carViewModel = new CarViewModel(item);

                // Assert
                carViewModel.IsActive.Should().Be(true);
            }
        }

    }
}