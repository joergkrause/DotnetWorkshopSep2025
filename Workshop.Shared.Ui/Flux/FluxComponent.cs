using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Workshop.Shared.Ui.Flux;

public class FluxComponent<TState> : FluxorComponent
  where TState : BaseState 
{

  [Inject]
  public IDispatcher  Dispatcher { get; set; } = default!;

  [Inject]
  public IState<TState> State { get; set; } = default!;


}
