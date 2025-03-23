using System.Reflection;
using OrderFlow.Api.Data;
using OrderFlow.Api.EndPoints;
using OrderFlow.Api.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("OrderflowPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7120") // URL do seu Blazor WASM
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Se você estiver enviando cookies/autenticação
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("OrderflowPolicy");

app.MapCustomerEndpoints()
    .MapProductEndpoints()
    .MapOrderEndpoints();


app.UseHttpsRedirection();

app.Run();