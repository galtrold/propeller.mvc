using System;
using System.Linq;
using FluentAssertions;
using IntergrationTest.Models;
using IntergrationTest.Utils;
using Newtonsoft.Json;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace IntergrationTest.Tests.ViewModelFactoryTest.Serilization
{
    public class Serialize_PropellerModel
    {
        [Fact]
        public void Serialize_Propeller_Model_Success()
        {
            using (var db = SharedDatabaseDefinition.StarwarsDatabase())
            {
                using (ShareMediaProvider.MediaProvider())
                {

                    // Arrange
                    var model = SharedDatabaseDefinition.StaticVehichleData;

                    // Act
                    var jsonStr = JsonConvert.SerializeObject(model);

                    // Assert
                    jsonStr.Length.Should().BeGreaterThan(10);

                }
            }
        }
    }
}