using FluentAssertions;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Model.Adapters;
using Propeller.Mvc.Model.Factory;
using Xunit;

namespace ModelTest.Tests.ViewModelFactoryTest.ComplexModels
{
    public class ResolveComplexObjectReferenceSuccesfully
    {
        [Fact]
        public void GetSingleItemReferenceAsViewModel()
        {
            using (var db = SharedDatabaseDefinition.CarDatabase())
            {
                // Arrange
                var factory = new ModelFactory();


                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = factory.Create<CarViewModel>(item);
                carViewModel.WikiLink = carViewModel.GetAs<GeneralLink>(p => p.WikiLink);

                // Assert
                carViewModel.WikiLink.Url.Should().Be(SharedDatabaseDefinition.StaticCarData.WikiLink.Url);
                carViewModel.WikiLink.Desciption.Should().Be(SharedDatabaseDefinition.StaticCarData.WikiLink.Desciption);


            }

        }
        
    }
}