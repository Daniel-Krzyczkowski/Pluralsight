using CarsIsland.WebAPI.Controllers;
using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Tests.Controllers
{
    [TestClass]
    public class LocationsDataControllerTests
    {
        private Mock<ILocationsDataService> _locationsDataService;
        private LocationsController _locationsController;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            _locationsDataService = new Mock<ILocationsDataService>();
            _locationsDataService.Setup(service => service.GetLocations())
                .ReturnsAsync(GetTestLocations());

            _locationsController = new LocationsController(_locationsDataService.Object);
        }

        [TestMethod]
        public async Task ShouldReturnLocationsData()
        {
            //Act
            var locationsResponse = await _locationsController.Get();

            //Assert
            Assert.IsInstanceOfType(locationsResponse, typeof(List<LocationDto>));
        }

        private List<LocationDto> GetTestLocations()
        {
            var locationsList = new List<LocationDto>()
            {
                new LocationDto() { Address = "Street 1/2", City = "Paris", Country = "France", ZipCode = "75008"},
                new LocationDto() { Address = "Street 2/4", City = "Berlin", Country = "Germany", ZipCode = "12432"},
                new LocationDto() { Address = "Street 10/4", City = "Warsaw", Country = "Poland", ZipCode = "64543"}
            };

            return locationsList;
        }
    }
}
