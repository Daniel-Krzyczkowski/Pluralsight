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
    public class CarsDataControllerTests
    {
        private Mock<ICarsDataService> _carsDataService;
        private CarsController _carsController;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            _carsDataService = new Mock<ICarsDataService>();
            _carsDataService.Setup(service => service.GetAvailableCars())
                .ReturnsAsync(GetTestCars());

            _carsController = new CarsController(_carsDataService.Object);
        }

        [TestMethod]
        public async Task ShouldReturnAvailableCarsData()
        {
            //Act
            var carsResponse = await _carsController.Get();

            //Assert
            Assert.IsInstanceOfType(carsResponse, typeof(List<CarDto>));
        }

        private List<CarDto> GetTestCars()
        {
            var carsList = new List<CarDto>()
            {
                new CarDto() { Brand = "BMW", Model = "430", Cost = "250€/day", Location = new LocationDto()
                                                {
                                                    Address = "Street 1/2",
                                                    City = "Paris",
                                                    Country = "France",
                                                    ZipCode = "75008"} },
                new CarDto() { Brand = "Mercedes", Model = "C300", Cost = "300€/day", Location = new LocationDto() { Address = "Street 2/4", City = "Berlin", Country = "Germany", ZipCode = "12432"} },
                new CarDto() { Brand = "Audi", Model = "A1", Cost = "200€/day", Location = new LocationDto() { Address = "Street 10/4", City = "Warsaw", Country = "Poland", ZipCode = "64543"} }
            };

            return carsList;
        }
    }
}
