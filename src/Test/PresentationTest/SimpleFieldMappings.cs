using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using PresentationTest.Constants;
using PresentationTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Sitecore.Data;
using Sitecore.FakeDb;
using Xunit;

namespace PresentationTest
{


    public class SimpleFieldMappings

    {

        [Fact]
        public void Map_simple_string_fields_to_the_viewmodel_using_the_Include_configuration_success()
        {
            // Arrange 
            EnvironmentSetttings.ApplicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var mappingProcessor = new MappingProcessor();
            mappingProcessor.Process(null);
            

            using (var db = new Db()
            {
                new DbTemplate("car", ConstantsCarModel.Templates.CarTemplateId)
                {
                        {new ID(ConstantsCarModel.Fields.ManuFactureField), "Ford" },
                        {new ID(ConstantsCarModel.Fields.CarModelField), "500" },
                        {new ID(ConstantsCarModel.Fields.CarClassField), "City Cruiser"}
                },
                new DbItem("Ford500", ID.NewID, ConstantsCarModel.Templates.CarTemplateId)
            })
            {
                // Act
                var item = db.GetItem("/sitecore/content/Ford500");
                var carViewModel = new CarViewModel(item);

                // Assert
                carViewModel.Manufacture.Should().Be("Ford");
                carViewModel.CarModel.Should().Be("500");
                carViewModel.CarClass.Should().Be("City Cruiser");
            }
        }

    }
}
