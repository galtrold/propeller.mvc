using System;
using FluentAssertions;
using ModelTest.Constants;
using ModelTest.Utils;
using ModelTest.ViewModels;
using Propeller.Mvc.Core.Processing;
using Propeller.Mvc.Core.Utility;
using Propeller.Mvc.Model.Adapters;
using Sitecore.Data;
using Sitecore.FakeDb;
using Xunit;

namespace ModelTest.Tests.ComplexModels
{
    public class ResolveComplexObjectReferenceSuccesfully
    {
        [Fact]
        public void GetSingleItemReferenceAsViewModel()
        {
            using (var db = SharedDatabaseDefinition.CarDatabase())
            {

                // Act
                var item = db.GetItem("/sitecore/content/Astra");
                var carViewModel = new CarViewModel(item);
                carViewModel.WikiLink = carViewModel.GetAs<GeneralLink>(p => p.WikiLink);

                // Assert
                carViewModel.WikiLink.Url.Should().Be(SharedDatabaseDefinition.StaticCarData.WikiLink.Url);
                carViewModel.WikiLink.Desciption.Should().Be(SharedDatabaseDefinition.StaticCarData.WikiLink.Desciption);


            }

        }
        
    }
}