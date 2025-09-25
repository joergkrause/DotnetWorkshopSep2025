using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workshop.DataTransferModels;

namespace Workshop.Persistence.Respositories;

public class CustomerRepository(IServiceProvider serviceProvider) : ICustomerRepository
{
  private WorkshopContext Context => (WorkshopContext)serviceProvider.GetService(typeof(WorkshopContext))!;
  private readonly IMapper _mapper = serviceProvider.GetRequiredService<IMapper>();

  public async Task<List<CustomerListDto>> GetAllAsync()
  {
    var models = await Context.Customers.Select(e => new { e.Name, e.Id }).ToListAsync();
    // Sonderfall: Anonyme Typen können nicht direkt gemappt werden

    return [.. models.Select(m => new CustomerListDto { Id = m.Id, Name = m.Name })];
  }

  public async Task<List<CustomerDto>> GetAllAsync(string searchName)
  {
    var models = await Context.Customers.Where(e => e.Name.Contains(searchName)).ToListAsync();
    return _mapper.Map<List<CustomerDto>>(models);
  }

  public async Task<List<CustomerDto>> QueryAsync(QueryDto query)
  {
    if (query.Field != "name" || query.Operation != Operation.Contains)
    {
      throw new NotImplementedException("Only 'name' field with 'contains' operation is implemented.");
    }
    var models = await Context.Customers
      .Where(e => e.Name.Contains(query.Value))
      .ToListAsync();
    return _mapper.Map<List<CustomerDto>>(models);
  }

  public async Task<CustomerDto?> GetByIdAsync(int id)
  {
    var model = await Context.Customers.FindAsync(id);
    if (model == null) return null;
    return _mapper.Map<CustomerDto?>(model);
  }

  public async Task<int> AddAsync(CustomerAddDto customer)
  {
    var model = _mapper.Map<DomainModels.Customer>(customer);
    Context.Customers.Add(model);
    await Context.SaveChangesAsync();
    return model.Id;
  }

  public async Task UpdateAsync(CustomerDto customer)
  {
    ArgumentOutOfRangeException.ThrowIfZero(customer.Id);

    var model = _mapper.Map<DomainModels.Customer>(customer);
    Context.Customers.Update(model);
    await Context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    ArgumentOutOfRangeException.ThrowIfZero(id);

    var customer = await Context.Customers.FindAsync(id);
    if (customer != null)
    {
      Context.Customers.Remove(customer);
      await Context.SaveChangesAsync();
    }
  }

}
