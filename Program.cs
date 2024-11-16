using dotenv.net;
using Microsoft.EntityFrameworkCore;
using umbraco_lingoquest.Data;
using umbraco_lingoquest.Helpers;
using umbraco_lingoquest.ServiceModil;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
DotEnv.Load();

string dbFileName = "lingoquest_db.sqlite.db"; 
string relativePath = Path.Combine("umbraco", "data", dbFileName); 
string absoluteDbPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);


builder.Configuration["ConnectionStrings:umbracoDbDSN"] = $"Data Source={absoluteDbPath};Cache=Shared;Foreign Keys=True;Pooling=True";

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("umbracoDbDSN")));

builder.Services.AddScoped<JwtService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhostClient",
        builder =>
        {
            builder.WithOrigins()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
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