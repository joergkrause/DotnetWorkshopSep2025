using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Workshop.DataTransferModels;
using Workshop.DomainModels;
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

  public static IServiceCollection AddMapsterMappings(this IServiceCollection services)
  {
    services.AddMapster();
    var mapperConfig = TypeAdapterConfig.GlobalSettings;
    mapperConfig.NewConfig<Customer, CustomerDto>();
    mapperConfig.NewConfig<Customer, CustomerListDto>();
    
    mapperConfig.NewConfig<CustomerDto, Customer>();
    mapperConfig.NewConfig<CustomerAddDto, Customer>();

    services.AddSingleton(mapperConfig);
    services.AddScoped<IMapper, Mapper>();

    return services;
  }

}
