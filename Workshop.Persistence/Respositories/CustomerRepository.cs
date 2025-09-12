using Microsoft.EntityFrameworkCore;

namespace Workshop.Persistence.Respositories;

public class CustomerRepository(IServiceProvider serviceProvider) : ICustomerRepository
{
  private WorkshopContext Context => (WorkshopContext)serviceProvider.GetService(typeof(WorkshopContext))!;

  public async Task<List<DomainModels.Customer>> GetAllAsync()
  {
    return await Context.Customers.ToListAsync();
  }

  public async Task<DomainModels.Customer?> GetByIdAsync(int id)
  {
    return await Context.Customers.FindAsync(id);
  }

  public async Task AddAsync(DomainModels.Customer customer)
  {
    Context.Customers.Add(customer);
    await Context.SaveChangesAsync();
  }

  public async Task UpdateAsync(DomainModels.Customer customer)
  {
    Context.Customers.Update(customer);
    await Context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var customer = await Context.Customers.FindAsync(id);
    if (customer != null)
    {
      Context.Customers.Remove(customer);
      await Context.SaveChangesAsync();
    }
  }

}
