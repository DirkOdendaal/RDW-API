using Microsoft.AspNetCore.Mvc;
using Moq;
using RDW_API.Controllers;
using RDW_API.Interfaces;
using RDW_API.Models;

namespace RDW_API_Test.ControllerTests
{
    public class VehicleInfoControllerTest
    {
        [Fact]
        public async Task GetVehiclesSuccessTest()
        {
            // Arrange
            var mockService = new Mock<IVehicleInfoService>();
            mockService.Setup(service => service.GetVehicles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<VehicleInfo> { new VehicleInfo { } });

            var controller = new VehicleInfoController(mockService.Object);

            // Act
            var result = await controller.GetVehicles();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<VehicleInfo>>(okResult.Value);
            Assert.Single(model);
        }

        [Fact]
        public async Task GetVehiclesNotFoundTest()
        {
            // Arrange
            var mockService = new Mock<IVehicleInfoService>();
            mockService.Setup(service => service.GetVehicles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<VehicleInfo>());

            var controller = new VehicleInfoController(mockService.Object);

            // Act
            var result = await controller.GetVehicles("make","registration","type");

            // Assert

            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
