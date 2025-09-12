using Workshop.Persistence.Respositories;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class AddDependenciesExtension
{
  public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
  {
    services.AddScoped<ICustomerRepository, CustomerRepository>();
    return services;
  }
}
