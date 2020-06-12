using CarsIsland.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CarsIsland.WebApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CarsIslandDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CarsIslandDbContext>>()))
            {
                if (context.Locations.Any())
                {
                    return;
                }

                context.Locations.AddRange(
                    new Location() { Id = Guid.NewGuid(), Address = "Street 1/2", City = "Paris", Country = "France", ZipCode = "75008" },
                    new Location() { Id = Guid.NewGuid(), Address = "Street 2/4", City = "Berlin", Country = "Germany", ZipCode = "12432" },
                    new Location() { Id = Guid.NewGuid(), Address = "Street 10/4", City = "Warsaw", Country = "Poland", ZipCode = "64543" },
                    new Location() { Id = Guid.NewGuid(), Address = "Street 87/41", City = "Barcelona", Country = "Spain", ZipCode = "64543" }
                );
                context.SaveChanges();

                var addedLocations = context.Locations.ToList();

                context.Contacts.AddRange(
                    new Contact()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Adam",
                        LastName = "Smith",
                        Location = addedLocations[0],
                        Email = "cars@island.fr",
                        PhoneNumber = "144144144"
                    },
                    new Contact()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "John",
                        LastName = "Long",
                        Location = addedLocations[1],
                        Email = "cars@island.de",
                        PhoneNumber = "166166166"
                    },
                    new Contact()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Sara",
                        LastName = "Conor",
                        Location = addedLocations[2],
                        Email = "cars@island.pl",
                        PhoneNumber = "122122122"
                    },
                    new Contact()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Miranda",
                        LastName = "Grande",
                        Location = addedLocations[3],
                        Email = "cars@island.es",
                        PhoneNumber = "315654987"
                    }
                );
                context.SaveChanges();

                var addedContacts = context.Contacts.ToList();

                context.Cars.AddRange(
                    new Car()
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Audi",
                        Model = "A6",
                        Cost = "200€/day",
                        ImageUrl = "https://carsislandstorageaccount.blob.core.windows.net/cars-images/audi-car-image.jpg",
                        ContactPerson = addedContacts[0],
                        Location = addedLocations[0]
                    },
                    new Car()
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Mercedes",
                        Model = "SLS cabrio",
                        Cost = "300€/day",
                        ImageUrl = "https://carsislandstorageaccount.blob.core.windows.net/cars-images/mercedes-car-image.jpg",
                        ContactPerson = addedContacts[1],
                        Location = addedLocations[1],
                    },
                    new Car()
                    {
                        Id = Guid.NewGuid(),
                        Brand = "BMW",
                        Model = "330",
                        Cost = "250€/day",
                        ImageUrl = "https://carsislandstorageaccount.blob.core.windows.net/cars-images/bmw-car-image.jpg",
                        ContactPerson = addedContacts[2],
                        Location = addedLocations[2]
                    },
                    new Car()
                    {
                        Id = Guid.NewGuid(),
                        Brand = "Smart",
                        Model = "ForTwo",
                        Cost = "150€/day",
                        ImageUrl = "https://carsislandstorageaccount.blob.core.windows.net/cars-images/smart-car-image.jpg",
                        ContactPerson = addedContacts[3],
                        Location = addedLocations[3]
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
