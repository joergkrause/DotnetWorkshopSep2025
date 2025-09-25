using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using static System.ComponentModel.DataAnnotations.Validator;
using Workshop.DataTransferModels;
using Workshop.Persistence.Respositories;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Workshop.BackendRestMinimal.Mappings;

public static class MapCustomerEndpointExtension
{
  public static IEndpointRouteBuilder MapCustomerEndpoint(this WebApplication app)
  {
    app.MapGet("/customer", async ([FromServices] ICustomerRepository customerRepo) =>
    {
      var customers = await customerRepo.GetAllAsync();
      return Results.Ok(customers);
    })
    .WithName("GetCustomers")
    .Produces<IEnumerable<CustomerListDto>>();

    app.MapGet("/customer/search", async ([FromServices] ICustomerRepository customerRepo, [FromQuery(Name = "search")] string searchName) =>
    {
      var customers = await customerRepo.GetAllAsync(searchName);
      return Results.Ok(customers);
    })
    .WithName("SearchCustomers")
    .Produces<IEnumerable<CustomerDto>>();

    app.MapPost("/customer/query", async ([FromServices] ICustomerRepository customerRepo, [FromBody] QueryDto query) =>
    {
      var customers = await customerRepo.QueryAsync(query);
      return Results.Ok(customers);
    })
    .WithName("QueryCustomers")
    .Produces<IEnumerable<CustomerDto>>();

    app.MapGet("/customer/{id:int}", async ([FromServices] ICustomerRepository customerRepo, [FromRoute] int id) =>
    {
      var customer = await customerRepo.GetByIdAsync(id);
      return customer is not null ? Results.Ok(customer) : Results.NotFound();
    })
    .WithName("GetCustomer")
    .Produces<CustomerDto>()
    .ProducesProblem(StatusCodes.Status404NotFound);

    // DataAnnotations Validation Example
    //app.MapPost("/customer", async ([FromServices] ICustomerRepository customerRepo, [FromBody] CustomerAddDto customer) =>
    //{
    //  var results = new List<ValidationResult>();
    //  var isValid = TryValidateObject(customer, new ValidationContext(customer), results, true);

    //  if (!isValid)
    //  {
    //    return Results.BadRequest(results);
    //  }

    //  var customerId = await customerRepo.AddAsync(customer);
    //  return Results.Created($"/customer/{customerId}", customer);
    //})
    //.WithName("AddCustomer")
    //.Produces<CustomerDto>()
    //.Produces<IEnumerable<ValidationResult>>(StatusCodes.Status400BadRequest)
    //;

    app.MapPost("/customer", async ([FromServices] ICustomerRepository customerRepo, IValidator<CustomerAddDto> validator, [FromBody] CustomerAddDto customer) =>
    {
      var validationResult = await validator.ValidateAsync(customer);
      if (!validationResult.IsValid)
      {
        return Results.BadRequest(validationResult.Errors);
      }

      var customerId = await customerRepo.AddAsync(customer);
      return Results.Created($"/customer/{customerId}", customer);
    })
    .WithName("AddCustomer")
    .Produces<CustomerDto>()
    .Produces<IEnumerable<ValidationResult>>(StatusCodes.Status400BadRequest)
    ;

    app.MapPut("/customer/{id:int}", async ([FromServices] ICustomerRepository customerRepo, [FromRoute] int id, [FromBody] CustomerDto customer) =>
    {
      if (id != customer.Id) return Results.BadRequest();
      await customerRepo.UpdateAsync(customer);
      return Results.Ok(customer);
    })
    .WithName("UpdateCustomer")
    .Produces<CustomerDto>()
    .ProducesProblem(StatusCodes.Status400BadRequest);

    app.MapDelete("/customer/{id:int}", async ([FromServices] ICustomerRepository customerRepo, [FromRoute] int id) =>
    {
      var customer = await customerRepo.GetByIdAsync(id);
      if (customer == null)
      {
        return Results.NotFound();
      }
      await customerRepo.DeleteAsync(id);
      return Results.NoContent();
    })
    .WithName("DeleteCustomer")
    .Produces(StatusCodes.Status204NoContent)
    .ProducesProblem(StatusCodes.Status404NotFound);

    return app;
  }
}
