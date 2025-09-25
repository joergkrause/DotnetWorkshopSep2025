using Workshop.DataTransferModels;

namespace Workshop.Persistence.Respositories;

public interface ICustomerRepository
{
  Task AddAsync(CustomerDto customer);
  Task DeleteAsync(int id);
  Task<List<CustomerListDto>> GetAllAsync();
  Task<List<CustomerDto>> GetAllAsync(string searchName);
  Task<CustomerDto?> GetByIdAsync(int id);
  Task<List<CustomerDto>> QueryAsync(QueryDto query);
  Task UpdateAsync(CustomerDto customer);
}