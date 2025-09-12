using Microsoft.EntityFrameworkCore;
using Workshop.BackendApi.Services;
using Workshop.Persistence;
using Workshop.Persistence.Respositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CustomerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
