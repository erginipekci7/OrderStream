using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http.Json; 

public class OrderApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrderApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrders_ReturnsSuccessStatusCode()
    {
        var response = await _client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderById_ReturnsSuccessStatusCode()
    {
        var orderId = "6739fe16301b2db8ae92985f";
        var response = await _client.GetAsync($"/api/orders/{orderId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_ReturnsSuccessStatusCode()
    {
        var order = new OrderRequest
        {
            CustomerId = 1,
            OrderNumber = "123",
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductName = "Product 1", Quantity = 1, UnitPrice = 100 }
            }
        };
        var response = await _client.PostAsJsonAsync<OrderRequest>("/api/orders", order);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync<OrderRequest>("/api/orders", null);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_ReturnsSuccessStatusCode()
    {
        var orderId = "6739fe16301b2db8ae92985f";
        var order = new OrderRequest
        {
            CustomerId = 1,
            OrderNumber = "123",
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductName = "UPDATED Product 1", Quantity = 1, UnitPrice = 100 }
            }
        };
        var response = await _client.PutAsJsonAsync<OrderRequest>($"/api/orders/{orderId}", order);
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_ReturnsBadRequest()
    {
        var response = await _client.PutAsJsonAsync<OrderRequest>("/api/orders/6739fe16301b2db8ae92985f", null);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteOrder_ReturnsSuccessStatusCode()
    {
        var orderId = "674c732401326a3f749bc695";
        var response = await _client.DeleteAsync($"/api/orders/{orderId}");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

