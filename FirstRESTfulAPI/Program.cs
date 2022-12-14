using FirstRESTfulAPI;
using FirstRESTfulAPI.Interface;
using FirstRESTfulAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

ConfigurationManager manager = builder.Configuration;
//CORS
string[] corsURL = manager.GetSection("CORS").Get<string[]>();
Console.WriteLine(corsURL[0]);
builder.Services.AddCors(opt => {
    opt.AddPolicy("myweb", bul =>
    {
        //bul.WithHeaders(corsURL[0]);
        //bul.WithMethods(corsURL[0]);
        //bul.WithOrigins(corsURL[0]);
        bul.AllowAnyHeader();
        bul.AllowAnyMethod();
        bul.AllowAnyOrigin();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmployeesContext>((builder) => {
    builder.UseSqlServer(manager.GetConnectionString("Employee"));
});
builder.Services.AddScoped<IEmoloyee, EmployeeServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//Use Middleware
app.UseCors("myweb");

app.MapControllers();

app.Run();
