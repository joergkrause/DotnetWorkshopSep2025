using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.UseCases.Services;
using Workshop.UseCases.ViewModels;

namespace Workshop.UseCases.Mappings;

public static class AddDependenciesExtension
{
  public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
  {
    services.AddSingleton<ICustomerApiService, CustomerApiService>();
    services.AddSingleton<MinimalApiBackend>(op =>
    {
      var httpClient = new HttpClient
      {
        BaseAddress = new Uri("https://localhost:7017")
      };
      return new MinimalApiBackend(httpClient);
    });

    return services;
  }

  public static IServiceCollection AddMapsterMappings(this IServiceCollection services)
  {
    services.AddMapster();
    var mapperConfig = TypeAdapterConfig.GlobalSettings;
    mapperConfig.NewConfig<CustomerDto, CustomerEditViewModel>();
    mapperConfig.NewConfig<CustomerDto, CustomerNewViewModel>();
    mapperConfig.NewConfig<CustomerListDto, CustomerListViewModel>();

    mapperConfig.NewConfig<CustomerEditViewModel, CustomerDto>();
    mapperConfig.NewConfig<CustomerNewViewModel, CustomerAddDto>();

    services.AddSingleton(mapperConfig);
    services.AddSingleton<IMapper, Mapper>();

    return services;
  }

}
