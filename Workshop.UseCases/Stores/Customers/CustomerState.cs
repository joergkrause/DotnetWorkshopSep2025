using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Shared.Ui.Flux;
using Workshop.UseCases.ViewModels;

namespace Workshop.UseCases.Stores.Customers;

[FeatureState(CreateInitialStateMethodName = nameof(CreateInitialState))]
public record CustomerState(
    IReadOnlyList<CustomerListViewModel> Customers,
    CustomerEditViewModel? CurrentEdit,
    int CurrentEditId
  ) : BaseState("")
{

  private static CustomerState CreateInitialState() => new([], null, 0);
}
