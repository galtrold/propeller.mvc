using NVelocity.Runtime.Directive;
using Propeller.Mvc.Presentation;
using Xunit;

namespace PresentationTest.ViewModels
{
    public class RenderParameterTest
    {
        [Fact]
        public void ParseBooleanSuccess()
        {
            
            // Arrange
            string value = "1";
            var factory = new RenderParameterFactory();
            
            // Act
            var result = factory.ValueTranformation<bool>(value);

            // Assert
            Assert.True(result);
        }
    }
}