using System.ComponentModel.DataAnnotations;

namespace Workshop.UseCases.ViewModels;

public class CustomerNewViewModel
{
  [Required(ErrorMessage = "Bitte geben Sie Ihren Namen ein."), StringLength(100)]
  public string Name { get; set; } = default!;

  [Required(ErrorMessage = "Bitte geben Sie Ihre E-Mail-Adresse ein."), EmailAddress(ErrorMessage = "Bitte geben Sie eine gültige E-Mail-Adresse ein.")]
  public string Email { get; set; } = default!;

  [Required(ErrorMessage = "Bitte geben Sie Ihre Telefonnummer ein."), Phone(ErrorMessage = "Bitte geben Sie eine gültige Telefonnummer ein.")]
  public string Phone { get; set; } = default!;
}
