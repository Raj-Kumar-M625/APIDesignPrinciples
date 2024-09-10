using ApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRefitClient<IBlogApi>()
                .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));
var app = builder.Build();

app.MapGet("/posts/{id}", async (int id, IBlogApi api) =>
    await api.GetPostAsync(id));

app.MapPost("/posts", async ([FromBody] Post post, IBlogApi api) =>
    await api.CreatePostAsync(post));

app.MapPut("/posts/{id}", async (int id, [FromBody] Post post, IBlogApi api) =>
    await api.UpdatePostAsync(id, post));

app.MapDelete("/posts/{id}", async (int id, IBlogApi api) =>
    await api.DeletePostAsync(id));

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
