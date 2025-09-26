
namespace CustomerFrontendApp.Services
{
  public interface ICustomerApiService
  {
    Task<IList<CustomerListDto>> GetAllCustomers();
  }
}