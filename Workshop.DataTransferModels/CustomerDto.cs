using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Workshop.DataTransferModels;


// name,id,name,id

public class CustomerDto : DtoBase
{
  [JsonPropertyName("name")]
  public string Name { get; set; } = default!;

  [JsonPropertyName("email")]
  public string Email { get; set; } = default!;

  [JsonPropertyName("phone")]
  public string Phone { get; set; } = default!;
}

public class CustomerListDto : DtoBase
{
  [JsonPropertyName("name")]
  public string Name { get; set; } = default!;

}

public class CustomerAddDto
{
  [JsonPropertyName("name")]
  [Required, StringLength(100)]
  public string Name { get; set; } = default!;

  [JsonPropertyName("email")]
  [EmailAddress, StringLength(100), Required]
  public string Email { get; set; } = default!;

  [JsonPropertyName("phone")]
  [Phone, Required, StringLength(15)]
  public string Phone { get; set; } = default!;
}

public class CustomerPatchDto : DtoBase
{
  [Required]
  public string Field { get; set; } = default!;

  public string? Value { get; set; }
}
