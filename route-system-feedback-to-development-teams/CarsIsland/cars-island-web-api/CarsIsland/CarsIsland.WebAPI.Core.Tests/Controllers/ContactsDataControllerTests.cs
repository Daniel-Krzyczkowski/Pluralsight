using CarsIsland.WebAPI.Controllers;
using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Tests.Controllers
{
    [TestClass]
    public class ContactsDataControllerTests
    {
        private Mock<IContactsDataService> _contactsDataService;
        private ContactsController _contactsController;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            _contactsDataService = new Mock<IContactsDataService>();
            _contactsDataService.Setup(service => service.GetContacts())
                .ReturnsAsync(GetTestContacts());

            _contactsController = new ContactsController(_contactsDataService.Object);
        }

        [TestMethod]
        public async Task ShouldReturnContactsData()
        {
            //Act
            var contactsResponse = await _contactsController.Get();

            //Assert
            Assert.IsInstanceOfType(contactsResponse, typeof(List<ContactDto>));
        }

        private List<ContactDto> GetTestContacts()
        {
            var contactsList = new List<ContactDto>()
                {
                    new ContactDto() { FirstName = "Adam", LastName = "Smith",
                                         Location = new LocationDto()
                                          {
                                          Address = "Street 1/2",
                                          City = "Paris",
                                          Country = "France",
                                          ZipCode = "75008"}},

                    new ContactDto() { FirstName = "John", LastName = "Long",
                                       Location = new LocationDto()
                                       {
                                           Address = "Street 2/4",
                                           City = "Berlin",
                                           Country = "Germany",
                                           ZipCode = "12432"}},
                    new ContactDto() { FirstName = "Sara", LastName = "Conor",
                                       Location = new LocationDto()
                                       {
                                           Address = "Street 10/4",
                                           City = "Warsaw",
                                           Country = "Poland",
                                           ZipCode = "64543"}}
                };


            return contactsList;
        }
    }
}
