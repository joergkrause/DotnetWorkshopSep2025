using Workshop.DomainModels;
using Workshop.Persistence;

namespace Workshop.Persistence.Validation;

public class CustomValidatorService(IServiceProvider serviceProvider) : ICustomValidatorService
{
  private WorkshopContext Context => (WorkshopContext)serviceProvider.GetService(typeof(WorkshopContext))!;
  public int MaxNameLength => Context.Set<Setting>().Single(e => e.Name == "NameMaxLength").Value;
}