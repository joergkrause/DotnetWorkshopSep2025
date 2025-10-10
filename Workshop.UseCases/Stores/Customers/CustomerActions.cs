using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Shared.Ui.Flux;
using Workshop.UseCases.ViewModels;

namespace Workshop.UseCases.Stores.Customers;

public static class CustomerActions
{
  public record LoadCustomersAction : IAction;
  public record LoadCustomersSuccessAction(IReadOnlyList<CustomerListViewModel> Customers) : IAction;
  public record LoadCustomersFailureAction(string ErrorMessage) : IAction;

  public record EditCustomerAction(int CustomerId) : IAction;
  public record EditCustomerSuccessAction(CustomerEditViewModel Customer) : IAction;
  public record EditCustomerFailureAction(string ErrorMessage) : IAction;

  public record AddCustomerAction(CustomerNewViewModel NewCustomer) : IAction;
  public record UpdateCustomerAction(CustomerEditViewModel UpdatedCustomer) : IAction;
  public record DeleteCustomerAction(Guid CustomerId) : IAction;

  
  public static LoadCustomersAction LoadCustomers() => new();
  public static LoadCustomersSuccessAction LoadCustomersSuccess(IReadOnlyList<CustomerListViewModel> customers) => new(customers);
  public static LoadCustomersFailureAction LoadCustomersFailure(string errorMessage) => new(errorMessage);

  public static EditCustomerAction EditCustomer(int customerId) => new(customerId);
  public static EditCustomerSuccessAction EditCustomerSuccess(CustomerEditViewModel customer) => new(customer);
  public static EditCustomerFailureAction EditCustomerFailure(string errorMessage) => new(errorMessage);

  public static AddCustomerAction AddCustomer(CustomerNewViewModel newCustomer) => new(newCustomer);
  public static UpdateCustomerAction UpdateCustomer(CustomerEditViewModel updatedCustomer) => new(updatedCustomer);
  public static DeleteCustomerAction DeleteCustomer(Guid customerId) => new(customerId);

}
