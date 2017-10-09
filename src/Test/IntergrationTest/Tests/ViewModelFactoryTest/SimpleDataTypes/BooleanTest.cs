﻿using System;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.SimpleDataTypes
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
            var factory = new ModelFactory();


            using (var db = SharedDatabaseDefinition.StarwarsDatabase())
            {
                // Act
                var item = db.GetItem("/sitecore/content/XWing");
                var carViewModel = factory.Create<VehichleModel>(item);

                // Assert
                carViewModel.HasHyperdrive.Should().Be(true);
            }
        }

    }
}