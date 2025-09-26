using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.UseCases.ViewModels;

public class CustomerEditViewModel
{
  public int Id { get; set; }

  [Display(Name = "Vor- und Zuname")]
  [Required, StringLength(100)]
  public string Name { get; set; } = default!;

  [Display(Name = "E-Mail")]
  [Required, EmailAddress]
  public string Email { get; set; } = default!;

  [Display(Name = "Telefon")]
  [Required, Phone]
  public string Phone { get; set; } = default!;
}

public class CustomerNewViewModel
{
  [Required, StringLength(100)]
  public string Name { get; set; } = default!;

  [Required, EmailAddress]
  public string Email { get; set; } = default!;
  
  [Required, Phone]
  public string Phone { get; set; } = default!;
}

public class CustomerListViewModel
{
  public int Id { get; set; }

  public string Name { get; set; } = default!;
}
