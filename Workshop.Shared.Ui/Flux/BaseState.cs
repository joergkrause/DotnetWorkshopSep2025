namespace Workshop.Shared.Ui.Flux;

public abstract record BaseState(string? ErrorMessage)
{
  private int LoadingCount { get; set; }

  public bool IsLoading
  {
    get => LoadingCount > 0;
    set => LoadingCount = value ? LoadingCount + 1 : Math.Max(0, LoadingCount -1);
  }

  public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

}
