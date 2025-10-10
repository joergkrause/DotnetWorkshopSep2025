using CustomerFrontendApp.Security;
using Fluxor;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Workshop.UseCases.Mappings;
using Workshop.UseCases.Stores.Customers;
using Workshop.UseCases.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5214");
//var client = new Workshop.BackendApi.Customer.CustomerClient(channel);
//builder.Services.AddSingleton(client);

// TODO: MemoryCache
builder.Services.AddMapsterMappings();
builder.Services.AddServiceDependencies();

// Add Identity

builder.Services.AddScoped<IUserStore<UserViewModel>, CustomUserStore>();
builder.Services.AddScoped<IRoleStore<RoleViewModel>, CustomRoleStore>();

builder.Services.AddIdentityCore<UserViewModel>(options =>
{
  options.Password.RequiredLength = 10;
})
  .AddRoles<RoleViewModel>()
  .AddRoleStore<CustomRoleStore>()  
  .AddUserStore<CustomUserStore>()
  .AddSignInManager()
  .AddDefaultTokenProviders()
  ;

builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = "/Account/Login";
  options.LogoutPath = "/Account/Logout";
  options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddFluxor(options =>
{
  options.ScanAssemblies(typeof(CustomerActions).Assembly);  
});

builder.Services.AddRazorPages();
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
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<CustomerFrontendApp.Components.App>()    
    .AddInteractiveServerRenderMode();

app.Run();
