using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.MapControllers();

app.MapGet("/", () => "Hello, World!");

app.MapGet("/user", () => "Rodrigo Gallert");

app.MapPost("/", () => new { Name = "Rodrigo", Age = 26 });

app.MapGet("/AddHeader", (HttpResponse response) => { response.Headers.Add("Teste", "Rodrigo"); return "aeae"; });

app.MapPost("/saveproduct", (Product product) =>
{
    return $"{product.Id} - {product.Name}";
});

app.MapGet("/getproduct", ([FromQuery] string id, string name) =>
{
    return $"Produto {id} nome {name}";
});


app.MapGet("/getproduct/{id}", ([FromRoute] string id) =>
{
    return $"Produto {id}";
});

app.MapGet("/getproductbyheader", (HttpRequest request) =>
{
    return request.Headers["product"].ToString();
});

app.Run();

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}