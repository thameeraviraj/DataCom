using DataCom.WebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataCom.WebAPI.Data;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }


    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        var products = new List<Product>()
        {
            new()
            {
                Id = Guid.Parse("8F2E9176-35EE-4F0A-AE55-83023D2DB1A3"),
                Name = "Samsung Galaxy S7",
                Description = "Newest mobile product from Samsung.",
                Price = (decimal) 1024.99,
                DeliveryPrice = (decimal) 16.99,
                Options = new List<ProductOption>()
                {
                    new()
                    {
                        Id = Guid.Parse("0643CCF0-AB00-4862-B3C5-40E2731ABCC9"),
                        Name = "White",
                        Description = "White Samsung Galaxy S7"
                    },
                    new()
                    {
                        Id = Guid.Parse("A21D5777-A655-4020-B431-624BB331E9A2"),
                        Name = "Black",
                        Description = "Black Samsung Galaxy S7"
                    }
                }
            },
            new()
            {
                Id = Guid.Parse("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"),
                Name = "Apple iPhone 6S",
                Description = "Newest mobile product from Apple.",
                Price = (decimal) 1299.99,
                DeliveryPrice = (decimal) 15.99,
                Options = new List<ProductOption>()
                {
                    new()
                    {
                        Id = Guid.Parse("5C2996AB-54AD-4999-92D2-89245682D534"),
                        Name = "Rose Gold",
                        Description = "White Samsung Galaxy S7"
                    },
                    new()
                    {
                        Id = Guid.Parse("9AE6F477-A010-4EC9-B6A8-92A85D6C5F03"),
                        Name = "Black",
                        Description = "White Apple iPhone 6S"
                    },
                    new()
                    {
                        Id = Guid.Parse("4E2BC5F2-699A-4C42-802E-CE4B4D2AC0EF"),
                        Name = "Black",
                        Description = "Black Apple iPhone 6S"
                    }
                }
            }
        };

        // Ensure Delete All
        _context.Products.RemoveRange(_context.Products);
        await _context.Products.AddRangeAsync(products);

        await _context.SaveChangesAsync();

    }
}