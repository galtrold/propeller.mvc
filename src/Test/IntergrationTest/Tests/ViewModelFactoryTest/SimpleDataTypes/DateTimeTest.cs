using System;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.SimpleDataTypes
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
            var factory = new ModelFactory();

            using (var db = SharedDatabaseDefinition.StarwarsDatabase())
            {
                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = factory.Create<VehichleModel>(item);

                // Assert
                
            }
        }
    }
}