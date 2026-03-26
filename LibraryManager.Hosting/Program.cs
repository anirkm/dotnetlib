using System.Text.Json.Serialization;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);
string dbPath = Path.Combine(AppContext.BaseDirectory, "library.db");

builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LibraryManager API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ICatalogManager, CatalogManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
