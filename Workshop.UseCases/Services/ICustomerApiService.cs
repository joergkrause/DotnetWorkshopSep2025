
namespace Workshop.UseCases.Services;

public interface ICustomerApiService
{
  Task<IList<CustomerListDto>> GetAllCustomers();
}
