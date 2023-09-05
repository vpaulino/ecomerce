using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BasketApi.Tests
{
    public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient httpClient;
        string relativePath = "orders";
        public OrdersControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            httpClient = _factory.CreateClient();
        }

        [Theory]
        [InlineData("0d1bdf9e-253d-487c-b919-1e342ec501a5")]
        public async Task GetOrder_WhenOrderDoesNotExit_ShouldReturnNotFound(string guid)
        {
       
            var getOrderResponse = await httpClient.GetAsync($"{relativePath}/{Guid.Parse(guid)}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getOrderResponse.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_WhenProductLineIsEmpty_ShouldReturnBadRequest()
        {
           
        var getOrderResponse = await httpClient.PostAsJsonAsync<CreateOrderRequest>("orders", new CreateOrderRequest("user@email.com", 200, new List<OrderLine>()));

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, getOrderResponse.StatusCode);
             

        }

        [Fact]
        public async Task CreateOrder_WhenProductLineIsNotEmpty_ShouldReturnCreated()
        {
           
            var createOrderRequest = new CreateOrderRequest("user@email.com", 200, new List<OrderLine>() { new OrderLine(1, "ProductName", 1, null, 1, 1) });

            var getOrderResponse = await httpClient.PostAsJsonAsync<CreateOrderRequest>($"{relativePath}", createOrderRequest);
           var response = getOrderResponse.Content.ReadFromJsonAsync<OrderResponse>();

            Assert.Equal(System.Net.HttpStatusCode.Created, getOrderResponse.StatusCode);
            Assert.NotNull(response);
            
        }

        [Fact]
        public async Task UpdateOrder_AddOrderLine_ShouldReturnOk()
        {

            var orderLine = new OrderLine(1, "ProductName", 1, null, 1, 1);

            var getOrderResponse = await httpClient.PatchAsJsonAsync<OrderLine>($"{relativePath}", orderLine);
           
            Assert.Equal(System.Net.HttpStatusCode.OK, getOrderResponse.StatusCode);
            

        }

        [Fact]
        public async Task UpdateOrder_DeleteOrderLine_ShouldReturnOk()
        {

            var orderLine =  new OrderLine(1, "ProductName", 1, null, 1, 1);
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, $"{relativePath}/1/orderLine");
            message.Content = JsonContent.Create<OrderLine>(orderLine);
            var getOrderResponse = await httpClient.SendAsync(message);
           
            Assert.Equal(System.Net.HttpStatusCode.OK, getOrderResponse.StatusCode);
            

        }
    }
}