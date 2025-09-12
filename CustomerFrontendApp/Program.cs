using CustomerFrontendApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5214");
var client = new Workshop.BackendApi.Customer.CustomerClient(channel);

builder.Services.AddSingleton(client);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
