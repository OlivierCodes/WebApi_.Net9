using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApi.src.Domain.Validations;
using WebApi.src.Infrastrure.Context;
using WebApi.src.Infrastrure.Exception;
using WebApi.src.Infrastrure.Mapping;
using WebApi.src.Service;
using static WebApi.src.Domain.Contract.ProductsContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddSwaggerGen();


// Register validators
builder.Services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateProductRequest>, UpdateProductRequestValidator>();

// Register AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Register our Services 
builder.Services.AddScoped<IProductService, ProductServie>();

// Register our DbContext

builder.Services.AddDbContext<ProductDbContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseExceptionHandler();

app.MapControllers();

app.Run();
