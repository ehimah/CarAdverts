using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using CarAdverts.Models;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CarAdverts.IntegrationTest
{
    public class CarAdvertIntegrationTest
    {
        [Fact]
        public async Task Test_Get_AllAsync()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("api/caradverts");
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task Test_Post()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("api/caradverts",
                    new StringContent(
                        JsonConvert.SerializeObject(new CarAdvertRequestModel()
                        {
                            Title = "Toyota Camry 2012",
                            Price = 200000,
                            Fuel = FuelType.Gasoline,
                            New = true
                        }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }

        [Fact]
        public async Task Test_Put_WithExistingID_ShouldReturnUpdateItem()
        {
            using (var client = new TestClientProvider().Client)
            {
                //Arrange
                var randomGuid = Guid.NewGuid();
                var response = await client.GetAsync("api/caradverts");
                var responseText = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<CarAdvertResponseModel>>(responseText);
                var item = items.First();
                var newTitile = "Toyota Camry 2012";
                var newPrice = 200000;
                var newFuelTYype = FuelType.Gasoline;
                var newStatus = true;

                //Act
                var putResponse = await client.PutAsync($"api/caradverts/{item.Id}",
                    new StringContent(
                        JsonConvert.SerializeObject(new CarAdvertRequestModel()
                        {
                            Title = newTitile,
                            Price = newPrice,
                            Fuel = newFuelTYype,
                            New = newStatus
                        }), Encoding.UTF8, "application/json"));
                putResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

                var getByIdResponse = await client.GetAsync($"api/caradverts/{item.Id}");
                var getItem = JsonConvert.DeserializeObject<CarAdvertResponseModel>(
                    await getByIdResponse.Content.ReadAsStringAsync()
                    );

                getByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                //Assert
                getItem.Title.Should().Be(newTitile);
                getItem.Price.Should().Be(newPrice);
                getItem.Fuel.Should().Be(newFuelTYype);
                getItem.New.Should().Be(newStatus);
            }
        }

        [Fact]
        public async Task Test_Put_WithRandomID_ShouldReturnNotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var randomGuid = Guid.NewGuid();
                var response = await client.PutAsync($"api/caradverts/{randomGuid}",
                    new StringContent(
                        JsonConvert.SerializeObject(new CarAdvertRequestModel()
                        {
                            Title = "Toyota Camry 2012",
                            Price = 200000,
                            Fuel = FuelType.Gasoline,
                            New = true
                        }), Encoding.UTF8, "application/json"));


                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task Test_Delete_WithRandomID_ShouldReturnNotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var randomGuid = Guid.NewGuid();
                var response = await client.DeleteAsync($"api/caradverts/{randomGuid}");

                response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

    }
}
