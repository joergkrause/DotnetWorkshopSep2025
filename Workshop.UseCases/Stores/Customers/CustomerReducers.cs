using Fluxor;
using Workshop.Shared.Ui.Flux;
using static Workshop.UseCases.Stores.Customers.CustomerActions;

namespace Workshop.UseCases.Stores.Customers;

public static class CustomerReducers
{
  [ReducerMethod]
  public static CustomerState ReduceCustomerActions(CustomerState state, IAction action)
  {
    return action switch
    {
      LoadCustomersAction _ => state with { IsLoading = true, ErrorMessage = "" },
      LoadCustomersSuccessAction a => state with { IsLoading = false, Customers = a.Customers, ErrorMessage = "" },
      LoadCustomersFailureAction a => state with { IsLoading = false, ErrorMessage = a.ErrorMessage },

      EditCustomerAction a => state with { IsLoading = true, CurrentEditId = a.CustomerId },
      EditCustomerSuccessAction a => state with { IsLoading = false, CurrentEdit = a.Customer, ErrorMessage = "" },
      EditCustomerFailureAction a => state with { IsLoading = false, CurrentEdit = null, CurrentEditId = 0, ErrorMessage = a.ErrorMessage },
      
      _ => state
    };
  }
}
