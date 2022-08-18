using FirstRESTfulAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
ConfigurationManager manager = builder.Configuration;
builder.Services.AddDbContext<EmployeesContext>((builder) => {
    builder.UseSqlServer(manager.GetConnectionString("Employee"));
});

//CORS
string[] corsURL = manager.GetSection("CORS").Get<string[]>();
Console.WriteLine(corsURL[0]);
builder.Services.AddCors(opt => {
    opt.AddPolicy("myweb", bul =>
    {
        bul.WithHeaders(corsURL[0]);
        bul.WithMethods(corsURL[0]);
        bul.WithOrigins(corsURL[0]);
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("myweb");

app.MapControllers();

app.Run();
