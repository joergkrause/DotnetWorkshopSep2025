
namespace Workshop.UseCases.Services;

public class CustomerApiService(MinimalApiBackend backend) : ICustomerApiService
{

  public async Task<IList<CustomerListDto>> GetAllCustomers()
  {
    var customers = await backend.GetCustomersAsync();
    return [.. customers];
  }

}
