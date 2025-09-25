using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.DomainModels;

public class Setting : EntityBase
{
  public string Name { get; set; } = null!;
  public int Value { get; set; }
}
