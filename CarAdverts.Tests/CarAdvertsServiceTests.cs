using CarAdverts.Domain.Data;
using CarAdverts.Domain.Entity;
using CarAdverts.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using CarAdverts.Models;

namespace CarAdverts.Tests
{
    public class CarAdvertsServiceTests
    {
        [Fact]
        public void GetAllItems_WhenStoreIsEmpty_ReturnsEmptyList()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "EmptyStore")
               .Options;

            //act

            //assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var allCarAdverts = service.GetAllItems();
                Assert.Equal(new List<CarAdvert>(), allCarAdverts);
            }
        }


        [Fact]
        public void GetAllItems_WhenStoreIsPopulated_ReturnsAllItems()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "PopulatedStore")
               .Options;

            //act
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var items = GetTestCarAdverts().ToList();

                items.ForEach(carAdvert => service.Add(carAdvert));

                context.SaveChanges();
            }

            //assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var allCarAdverts = service.GetAllItems().ToList();
                Assert.Equal(5, allCarAdverts.Count);
            }
        }

        [Fact]
        public void WhenStoreIsPopulated_ReturnsSingleItemById()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "ReturnSingleItem")
               .Options;

            //act
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var items = GetTestCarAdverts().ToList();

                items.ForEach( carAdvert => service.Add(carAdvert));

                context.SaveChanges();
            }

            //assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var testGuid = new Guid("981a7ab2-7887-4afd-b123-7d440c181627");
                var carAdvert = service.GetById(testGuid);
                Assert.IsType<CarAdvert>(carAdvert);
                Assert.Equal(testGuid, carAdvert.Id);
            }
        }

        [Fact]
        public void Remove_WhenStoreIsPopulated_RemovessSingleItemById()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "RemoveSingleItem")
               .Options;

            //act
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var items = GetTestCarAdverts().ToList();

                items.ForEach(carAdvert => service.Add(carAdvert));

                context.SaveChanges();
            }

            //assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var testGuid = new Guid("981a7ab2-7887-4afd-b123-7d440c181627");
                service.Remove(testGuid);

                var deletedCarAdvert = service.GetById(testGuid);
                var allCarAdverts = service.GetAllItems().ToList();

                Assert.Null(deletedCarAdvert);
                Assert.Equal(4, allCarAdverts.Count);
            }
        }

        [Fact]
        public void Update_WhenFieldsAreModified_ReturnsUpdatedFields()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "UpdateSingleItem")
               .Options;
            var testGuid = new Guid("981a7ab2-7887-4afd-b123-7d440c181627");
            var newTitle = "Used Merceded Benz G50 for Sale";
            var newFuel = FuelType.Gasoline;
            var newPrice = 200000;
            var newStatus = false;
            var newMileage = 105123;
            var newFirstRegistrationDate = new DateTime(2018, 3, 2);
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var items = GetTestCarAdverts().ToList();

                items.ForEach(carAdvert => service.Add(carAdvert));

                context.SaveChanges();
            }

             //act
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var carAdvertToUpdate = service.GetById(testGuid);

                carAdvertToUpdate.Title = newTitle;
                carAdvertToUpdate.Fuel = newFuel;
                carAdvertToUpdate.Price = newPrice;
                carAdvertToUpdate.New = newStatus;
                carAdvertToUpdate.Mileage = newMileage;
                carAdvertToUpdate.FirstRegistration = newFirstRegistrationDate;

                service.Update(carAdvertToUpdate);
            }

            //assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                
                var updatedCarAdvert = service.GetById(testGuid);
                
                Assert.Equal(updatedCarAdvert.Title, newTitle);
                Assert.Equal(updatedCarAdvert.Fuel, newFuel);
                Assert.Equal(updatedCarAdvert.Price, newPrice);
                Assert.Equal(updatedCarAdvert.New, newStatus);
                Assert.Equal(updatedCarAdvert.Mileage, newMileage);
                Assert.Equal(updatedCarAdvert.FirstRegistration, newFirstRegistrationDate);               
            }
        }

        [Fact]
        public void GetByQuery_WhenQueryIsEmpty_ReturnsAllItems()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetByQuery")
               .Options;
            
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var items = GetTestCarAdverts().ToList();

                items.ForEach(carAdvert => service.Add(carAdvert));

                context.SaveChanges();
            }

            //act & Assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var allCarAdverts = service.GetByQuery(new CarAdvertQueryModel()).ToList();
                Assert.Equal(5, allCarAdverts.Count);
            }
        }

        [Fact]
        public void GetByQuery_WhenTitleIsSupplied_FiltersByTitle()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetByQueryTitleSupplied")
               .Options;
            
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var items = GetTestCarAdverts().ToList();

                items.ForEach(carAdvert => service.Add(carAdvert));

                context.SaveChanges();
            }
            var query = new CarAdvertQueryModel
            {
                Title = "Used Toyota Matrix"
            };
            //act & Assert
            using (var context = new ApplicationContext(options))
            {
                var service = new CarAdvertService(context);
                var allCarAdverts = service.GetByQuery(query).ToList();
                Assert.Single(allCarAdverts);
            }
        }

        private List<CarAdvert> GetTestCarAdverts()
        {
            return new List<CarAdvert>()
            {
                new CarAdvert
                {
                    Id = new Guid("981a7ab2-7887-4afd-b123-7d440c181627"),
                    Title = "Toyota Corolla 2012 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 50000,
                    New = true,

                },
                new CarAdvert
                {
                    Id=new Guid("be3af952-44ee-4d04-b53b-c9f446a75d3d"),
                    Title = "Used Toyota Matrix 2014 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 45000,
                    New = false,
                    Mileage = 60000,
                    FirstRegistration= DateTime.Now.Subtract(TimeSpan.FromDays(200))

                },
                new CarAdvert
                {
                    Id=new Guid("7aa129fd-27a9-464a-a1e0-415a09d728ab"),
                    Title = "Honda Accord 2016 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 50000,
                    New = true,

                },
                new CarAdvert
                {
                    Id=new Guid("3e13bbdf-f757-4825-8b7c-3cbe1df1b9e3"),
                    Title = "Toyota Highlander 2012 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 120000,
                    New = false,
                    FirstRegistration = DateTime.Now.Subtract(TimeSpan.FromDays(720)),
                    Mileage = 29000

                },
                new CarAdvert
                {
                    Id=new Guid("5584046f-6973-4264-94eb-ce329a03ad7e"),
                    Title = "Toyota Corolla 2012 for sale",
                    Fuel = FuelType.Diesel,
                    Price = 50000,
                    New = true,
                },

            };
        }
    }
}
