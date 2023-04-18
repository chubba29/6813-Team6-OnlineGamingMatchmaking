using Microsoft.EntityFrameworkCore;
using ServiceDb.Data;
using System.Text.Json.Serialization;
using WebServiceGame6813Team6.Authorization;
using WebServiceGame6813Team6.Services;


var builder = WebApplication.CreateBuilder(args);


// add services to Dependency Injection container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var projectBaseDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
//Console.WriteLine("Project Base Dir: " + projectBaseDirectory);
//var serviceDBPath = $"Data Source = {projectBaseDirectory}/WebServiceGame6813Team6/ServiceDb.db";
//Console.WriteLine("DB Path: " + serviceDBPath);

builder.Services.AddDbContext<ServiceDbContext>
    (options => options.UseSqlite("Name=DefaultConnection"));

//builder.Services.AddDbContext<ServiceDbContext>
//    (options => options.UseSqlite(serviceDBPath));

builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; options.JsonSerializerOptions.WriteIndented = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
    .AllowCredentials()); // allow credentials

app.UseHttpsRedirection();

// custom basic auth middleware
app.UseMiddleware<BasicAuthMiddleware>();
//app.UseAuthorization();

app.MapControllers();

app.Run();
