using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using RDW_API.Configurations;
using RDW_API.Controllers;

namespace RDW_API_Test.ControllerTests
{
    public class TokenControllerTest
    {
        [Fact]
        public void GenerateTokenSuccessTest()
        {
            // Arrange
            var mockOptions = new Mock<IOptions<JwtOptions>>();
            var mockConfiguration = new JwtOptions { Issuer = "testIssuer", Audience = "testAudience", Key = "ThisIsAValidKeyBeingUsedToSignTheJWT", ExpirationInMinutes = 30 };
            mockOptions.Setup(options => options.Value).Returns(mockConfiguration);

            var controller = new TokenController(mockOptions.Object);

            // Act
            var result = controller.GenerateToken("ThisIsAValidKeyBeingUsedToSignTheJWT");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GenerateTokenBadRequestTest(string incommingKey)
        {
            // Arrange
            var mockOptions = new Mock<IOptions<JwtOptions>>();
            var mockConfiguration = new JwtOptions { Issuer = "testIssuer", Audience = "testAudience", Key = incommingKey, ExpirationInMinutes = 30 };
            mockOptions.Setup(options => options.Value).Returns(mockConfiguration);
            var controller = new TokenController(mockOptions.Object);

            // Act
            var result = controller.GenerateToken(null);

            // Assert
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void GenerateTokenForbiddenTest()
        {
            // Arrange
            var mockOptions = new Mock<IOptions<JwtOptions>>();
            var mockConfiguration = new JwtOptions { Issuer = "testIssuer", Audience = "testAudience", Key = "ThisIsAValidKeyBeingUsedToSignTheJWT", ExpirationInMinutes = 30 };
            mockOptions.Setup(options => options.Value).Returns(mockConfiguration);

            var controller = new TokenController(mockOptions.Object);

            // Act
            var result = controller.GenerateToken("ThisIsAnInvalidKeyBeingUsedToSignTheJWT");

            // Assert
            var forbiddenResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(403, forbiddenResult.StatusCode);
        }
    }
}
