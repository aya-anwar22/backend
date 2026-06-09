var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// IMPORTANT: UseCors قبل أي endpoint
app.UseCors("AllowReact");

// لازم MapGet يكون بعد UseCors
var products = new List<string>
{
    "Laptop",
    "Phone",
    "Keyboard"
};

app.MapGet("/products", () => Results.Ok(products));

app.MapGet("/products/{id}", (int id) =>
{
    if (id < 0 || id >= products.Count)
        return Results.NotFound();

    return Results.Ok(products[id]);
});

app.Run();