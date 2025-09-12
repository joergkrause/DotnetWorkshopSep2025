using System.ComponentModel.DataAnnotations;

namespace Workshop.DomainModels;

public class Customer : EntityBase
{
  public string Name { get; set; } = default!;

  [EmailAddress]
  public string Email { get; set; } = default!;

  [Phone]
  public string Phone { get; set; } = default!;
}
