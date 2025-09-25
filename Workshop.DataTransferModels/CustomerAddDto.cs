using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Workshop.DataTransferModels;

public class NameFormattingAttribute : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    if (value is string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        return new ValidationResult("Name cannot be empty or whitespace.");
      }
      if (name.Length > 100)
      {
        return new ValidationResult("Name cannot exceed 100 characters.");
      }
      if (name.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length < 2)
      {
        return new ValidationResult("Name must contain at least two words.");
      }
      return ValidationResult.Success;
    }
    return new ValidationResult("Invalid name format.");
  }

  public override string FormatErrorMessage(string name)
  {
    return base.FormatErrorMessage(name);
  }
}



public class CustomerAddDto
{
  
  [JsonPropertyName("name")]
  [NameFormatting]
  public string Name { get; set; } = default!;

  [JsonPropertyName("email")]
  [EmailAddress, StringLength(100), Required]
  public string Email { get; set; } = default!;

  [JsonPropertyName("phone")]
  [Phone, Required, StringLength(15)]
  public string Phone { get; set; } = default!;
}
