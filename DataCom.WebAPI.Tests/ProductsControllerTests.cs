using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DataCom.WebAPI.Data;
using DataCom.WebAPI.Models;
using DataCom.WebAPI.Requests;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DataCom.WebAPI.Tests;

public class ProductsControllerTests
{
#pragma warning disable CS8618
    private DataComApiApplication Application { get; set; }
#pragma warning restore CS8618

    [SetUp]
    public void Setup()
    {
        Application = new DataComApiApplication();
    }

    [Test]
    public async Task TaskGetAll_ReturnsProducts()
    {
        var client = Application.CreateClient();
        var response = await client.GetAsync("/products");
        Assert.True(response.IsSuccessStatusCode);

        var products = await response.Content.ReadFromJsonAsync<Products>();
        Assert.NotNull(products);
        Assert.AreEqual(2, products!.Items.Count());
    }

    [Test]
    public async Task Create_PostValidProduct_Returns201CreatedWithLocation_AndProductCountIncreaseByOne()
    {
        using var scope = Application.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var productCount = dbContext.Products.Count();

        var client = Application.CreateClient();
        var product = new ProductRequest()
        {
            Name = "Apple MacBook Pro",
            Description = "M1 Pro chip, Late 2021",
            Price = (decimal) 2999.99,
            DeliveryPrice = (decimal) 24.49
        };

        var response = await client.PostAsJsonAsync("/products", product);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        Assert.NotNull(response.Headers.Location);

        Assert.AreEqual(productCount + 1, dbContext.Products.Count());
    }


    [TearDown]
    public void Dispose()
    {
        Application.Dispose();
    }
}