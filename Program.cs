using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.ServiceModil;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Lägg till DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("umbracoDbDSN")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostClient",
        builder =>
        {
            builder.WithOrigins() 
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

app.UseCors("AllowLocalhostClient");
// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    SeedDatas.Initialize(context);

}

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

app.MapControllers();

await app.RunAsync();
