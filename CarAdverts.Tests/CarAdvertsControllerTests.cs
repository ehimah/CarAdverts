using AutoMapper;
using CarAdverts.Configuration;
using CarAdverts.Controllers;
using CarAdverts.Domain.Data;
using CarAdverts.Domain.Entity;
using CarAdverts.Domain.Service;
using CarAdverts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.Tests
{
    public class CarAdvertsControllerTests
    {

        public CarAdvertsControllerTests()
        {
            
        }
        #region GET Action Tests
        [Fact]
        public  void Get_WhenCalled_ReturensOkResult()
        {
            //arrange
            var controller = GetSUT("ReturensOkResult");

            //Act
            var okResult = controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            
        }

        [Fact]
        public void Get_WhenCalled_ReturensAllItems()
        {
            //arrange
            var controller = GetSUT("ReturensAllItems");

            //Act
            var okResult = controller.Get() as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<CarAdvertResponseModel>>(okResult.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            //arrange
            var controller = GetSUT("ReturnsNotFoundResult");

            //Act
            var notFoundResult = controller.Get(Guid.NewGuid());

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            //Arrange
            var controller = GetSUT("ExistingGuidPassed");
            var result = controller.Get() as OkObjectResult;
            var carAdverts = result.Value as List<CarAdvertResponseModel>;

            var testGuid = carAdverts.First().Id;

           //Act
           var okResult = controller.Get(testGuid);

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsCorrectItem()
        {
            //Arrange
            var controller = GetSUT("ExistingGuidPassed_ReturnsCorrectItem");
            var result = controller.Get() as OkObjectResult;
            var carAdverts = result.Value as List<CarAdvertResponseModel>;

            var testGuid = carAdverts.First().Id;

            //Act
            var okResult = controller.Get(testGuid) as OkObjectResult;

            //Assert
            Assert.IsType<CarAdvertResponseModel>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as CarAdvertResponseModel).Id);
        }
        #endregion

        #region Add ActionTests
        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var validCarAdvert = new CarAdvertRequestModel()
            {
                Title = "ValidObjectPassed",
                Fuel = FuelType.Diesel,
                Price = 50000,
                New = true,
            };
            var controller = GetSUT("Add_ValidObjectPassed_ReturnedResponseHasCreatedItem");

            // Act
            var createdResponse = controller.Post(validCarAdvert) as CreatedAtActionResult;
            var createdItem = createdResponse.Value as CarAdvertRequestModel;
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
            Assert.Equal("ValidObjectPassed", createdItem.Title);
        }
        #endregion

        #region POST Action test

        #endregion

        #region DELETE Action test

        #endregion

        #region PATCH Action test

        #endregion


        #region Helpers
        private CarAdvertsController GetSUT(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: databaseName)
               .Options;
            var context = new ApplicationContext(options);
            var service = new CarAdvertService(context);
            var items = GetTestCarAdverts().ToList();

            items.ForEach(carAdvert => service.Add(carAdvert));

            context.SaveChanges();
            
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarAdvertMappingProfile()));
            var mapper = new Mapper(configuration);

            var controller = new CarAdvertsController(service,mapper);
            return controller;
        }

        private IEnumerable<CarAdvert> GetTestCarAdverts()
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

        #endregion
    }
}
