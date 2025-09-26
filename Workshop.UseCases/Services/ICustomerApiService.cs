
using Workshop.UseCases.ViewModels;

namespace Workshop.UseCases.Services;

public interface ICustomerApiService
{
  Task<IList<CustomerListViewModel>> GetAllCustomers();
  Task<CustomerEditViewModel?> GetCustomerById(int id);
}
