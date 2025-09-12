using Grpc.Core;
using Workshop.BackendApi;
using Workshop.Persistence.Respositories;

namespace Workshop.BackendApi.Services
{
  public class CustomerService : Customer.CustomerBase
  {
    private readonly ILogger<CustomerService> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
    {
      _logger = logger;
      _customerRepository = customerRepository;
    }

    public override async Task<CustomersReply> GetCustomers(CustomersRequest request, ServerCallContext context)
    {
      var models = await _customerRepository.GetAllAsync();
      var reply = new CustomersReply();
      reply.Customers.AddRange(models.Select(m => new CustomersListReply
      {
        Id = m.Id,
        Name = m.Name
      }));
      return reply;
    }

    public override async Task<CustomerReply> GetCustomer(CustomerRequest request, ServerCallContext context)
    {
      var model = await _customerRepository.GetByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Customer with ID={request.Id} is not found."));
      var reply = new CustomerReply
      {
        Id = model.Id,
        Name = model.Name,
        Email = model.Email,
        Phone = model.Phone
      };
      return reply;
    }

    public override async Task<AddCustomerReply> AddCustomer(AddCustomerRequest request, ServerCallContext context)
    {
      var model = new DomainModels.Customer
      {
        Name = request.Name,
        Email = request.Email,
        Phone = request.Phone
      };
      await _customerRepository.AddAsync(model);
      var reply = new AddCustomerReply
      {
        Id = model.Id
      };
      return reply;
    }

    public override async Task<UpdateCustomerReply> UpdateCustomer(UpdateCustomerRequest request, ServerCallContext context)
    {
      var model = await _customerRepository.GetByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Customer with ID={request.Id} is not found."));
      model.Name = request.Name;
      model.Email = request.Email;
      model.Phone = request.Phone;
      await _customerRepository.UpdateAsync(model);
      return new UpdateCustomerReply();
    }

    public override async Task<DeleteCustomerReply> DeleteCustomer(DeleteCustomerRequest request, ServerCallContext context)
    {
      var model = await _customerRepository.GetByIdAsync(request.Id) ?? throw new RpcException(new Status(StatusCode.NotFound, $"Customer with ID={request.Id} is not found."));
      await _customerRepository.DeleteAsync(model.Id);
      return new DeleteCustomerReply();
    }
   
  }
}
