using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Workshop.DataTransferModels;

public enum Operation
{
  Equals = 0,
  Contains = 1,
  StartsWith = 2,
  EndsWith = 3
}

public class QueryDto
{
  [JsonPropertyName("field")]
  public string Field { get; set; } = default!;
  
  [JsonPropertyName("op")]
  public Operation Operation { get; set; }

  [JsonPropertyName("value")]
  public string Value { get; set; } = default!;
}
