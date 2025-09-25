using Microsoft.EntityFrameworkCore;
using Workshop.BackendRestMinimal.Mappings;
using Workshop.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Workshop.Persistence.WorkshopContext>(options =>
{
  var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
  if (environment == "Development")
  {
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(WorkshopContext)));
  }
  else
  {
    throw new NotImplementedException();
  }
});

builder.Services.AddPersistenceDependencies();
builder.Services.AddMapsterMappings();

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhost", builder =>
  {
    builder.WithOrigins("https://localhost:7017")
           .AllowAnyMethod()
           .AllowAnyHeader();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.MapCustomerEndpoint();


app.Run();

