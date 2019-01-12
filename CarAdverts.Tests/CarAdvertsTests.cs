using CarAdverts.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace CarAdverts.Tests
{
    public class CarAdvertsControllerTests
    {
        CarAdvertsController controller;

        public CarAdvertsControllerTests()
        {
           controller = new CarAdvertsController();
        }
        [Fact]
        public void Get_WhenCalled_ReturensOkResult()
        {
            //Act
            var okResult = controller.Get();

            //Assert
            Assert.IsType<OkResult>(okResult);
            
        }
    }
}
