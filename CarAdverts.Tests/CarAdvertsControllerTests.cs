using CarAdverts.Controllers;
using CarAdverts.Domain.Data;
using CarAdverts.Domain.Entity;
using CarAdverts.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarAdverts.Tests
{
    public class CarAdvertsControllerTests
    {
        CarAdvertsController controller;

        public CarAdvertsControllerTests()
        {
            
            var mockService = new Mock<ICarAdvertService>();
            mockService.Setup(service => service.GetAllItems())
                .Returns(GetTestCarAdverts());
           controller = new CarAdvertsController(mockService.Object);
        }

        

        [Fact]
        public  void Get_WhenCalled_ReturensOkResult()
        {
            //Act
            var okResult = controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
            
        }

        [Fact]
        public void Get_WhenCalled_ReturensAllItems()
        {
            //Act
            var okResult = controller.Get() as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<CarAdvert>>(okResult.Value);
            Assert.Equal(5, items.Count);
        }

        //[Fact]
        //public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        //{
        //    Act
        //   var notFoundResult = controller.Get(Guid.NewGuid());

        //    Assert
        //    Assert.IsType<NotFoundResult>(notFoundResult.Result);
        //}

        //[Fact]
        //public void GetById_ExistingGuidPassed_ReturnsOkResult()
        //{
        //    Arrange
        //   var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    Act
        //   var okResult = controller.Get(testGuid);

        //    Assert
        //    Assert.IsType<OkObjectResult>(okResult.Result);
        //}

        //[Fact]
        //public void GetById_ExistingGuidPassed_ReturnsCorrectItem()
        //{
        //    Arrange
        //   var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    Act
        //   var okResult = controller.Get(testGuid).Result as OkObjectResult;

        //    Assert
        //    Assert.IsType<CarAdvert>(okResult.Value);
        //    Assert.Equal(testGuid, (okResult.Value as CarAdvert).Id);
        //}

        private  IEnumerable<CarAdvert> GetTestCarAdverts()
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
