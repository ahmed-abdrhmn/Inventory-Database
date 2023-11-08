//using API.Service;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Database Context. This is where we specify the connection string
builder.Services.AddDbContext<InventoryDbContext>(option => {
    string conn = builder.Configuration.GetConnectionString("Local")!;
    option.UseMySql(conn,ServerVersion.AutoDetect(conn));
});

//builder.Services.AddScoped<IEntityDto,EntitiyDto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
