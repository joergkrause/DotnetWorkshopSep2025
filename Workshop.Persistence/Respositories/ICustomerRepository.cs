using Workshop.DomainModels;

namespace Workshop.Persistence.Respositories
{
  public interface ICustomerRepository
  {
    Task AddAsync(Customer customer);
    Task DeleteAsync(int id);
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task UpdateAsync(Customer customer);
  }
}