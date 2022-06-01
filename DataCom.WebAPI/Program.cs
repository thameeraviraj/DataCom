using DataCom.WebAPI.Data;
using DataCom.WebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var shouldUseInMemory = builder.Configuration.GetValue<bool>("UseInMemory");

AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "../../../Data"));

builder.Services.AddAutoMapper(typeof(Program)); 

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (shouldUseInMemory)
    {
        options.UseInMemoryDatabase("DataCom");
    }
    else
    {
        options.UseSqlServer(connectionString);
    }
    
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<IProductOptionRepository, ProductOptionRepository>();
builder.Services.AddTransient<IProductOptionService, ProductOptionService>();

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
        opt.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    using (var scope = app.Services.CreateScope())
    {
        var initializer = ActivatorUtilities.CreateInstance<ApplicationDbContextInitializer>(scope.ServiceProvider);
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
    }
    
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}