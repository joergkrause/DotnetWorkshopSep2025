
using MapsterMapper;
using Workshop.UseCases.ViewModels;

namespace Workshop.UseCases.Services;

public class CustomerApiService(MinimalApiBackend backend, IMapper mapper) : ICustomerApiService
{

  public async Task<IList<CustomerListViewModel>> GetAllCustomers()
  {
    var customers = await backend.GetCustomersAsync();
    var viewModels  = mapper.Map<IList<CustomerListViewModel>>(customers);
    return viewModels;
  }

  public async Task<CustomerEditViewModel?> GetCustomerById(int id)
  {
    var customer = await backend.GetCustomerAsync(id);
    if (customer == null)
    {
      return null;
    }
    var viewModel = mapper.Map<CustomerEditViewModel>(customer);
    return viewModel;
  }

}
