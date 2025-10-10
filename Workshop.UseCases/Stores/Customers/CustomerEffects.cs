using Fluxor;
using Workshop.UseCases.Services;
using static Workshop.UseCases.Stores.Customers.CustomerActions;

namespace Workshop.UseCases.Stores.Customers;

public class CustomerEffects(ICustomerApiService customerApiService)
{

  [EffectMethod]
  public async Task HandleLoadCustomersAction(LoadCustomersAction action, IDispatcher dispatcher)
  {
    try
    {
      var customers = await customerApiService.GetAllCustomers();
      dispatcher.Dispatch(LoadCustomersSuccess(customers.AsReadOnly()));
    }
    catch(Exception ex)
    {
      dispatcher.Dispatch(LoadCustomersFailure(ex.Message));
    }
    
  }

  [EffectMethod]
  public async Task HandleEditCustomerAction(EditCustomerAction action, IDispatcher dispatcher)
  {
    ArgumentOutOfRangeException.ThrowIfZero(action.CustomerId, nameof(action.CustomerId));

    try
    {
      var editCustomer = await customerApiService.GetCustomerById(action.CustomerId);
      if (editCustomer is null)
      {
        dispatcher.Dispatch(EditCustomerFailure($"Customer with id {action.CustomerId} not found"));
        return;
      }
      dispatcher.Dispatch(EditCustomerSuccess(editCustomer));
    }
    catch(Exception ex)
    {
      dispatcher.Dispatch(EditCustomerFailure(ex.Message));
    }

  }

}
