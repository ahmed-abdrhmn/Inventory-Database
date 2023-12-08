//using API.Service;
using Presentation.Controllers;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;
using Infrastructure;
using Services;
using Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adding Controllers from another project
var controllerAssembly = typeof(InventoryController).Assembly;
builder.Services.AddControllers().AddApplicationPart(controllerAssembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Database Context. This is where we specify the connection string
builder.Services.AddDbContext<InventoryDbContext>(option => {
    string conn = builder.Configuration.GetConnectionString("Local")!;
    option.UseMySql(conn,ServerVersion.AutoDetect(conn));
    option.EnableSensitiveDataLogging();
});

// Adding Scopes to configure dependency injection
// Repository
builder.Services.AddScoped<IRepository<InventoryInHeader>,DocumentRepository>();

// Services
builder.Services.AddScoped<IDocumentService, DocumentService>();

//Enable CORS
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

var app = builder.Build();

//Handling the Exception in Contract.Exceptions
app.UseExceptionHandler(options => {
    options.Run(async context => {
        context.Response.StatusCode = 400; //malformed Request
        context.Response.ContentType = "text/plain";
        var ex = context.Features.Get<IExceptionHandlerFeature>();
        if (ex!.Error is IDNotFoundException){
            await context.Response.WriteAsync(ex.Error.Message).ConfigureAwait(false);
        }
    });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
