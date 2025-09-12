using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Persistence;

namespace Workshop.Backend.Tests
{
  [TestClass]
  public class IntegrationTests
  {

    [TestInitialize]
    public void InitDbAndDbContext()
    {
      // Initialize the database and DbContext here
      // This could involve setting up an in-memory database or a test database
      // For example, using Entity Framework Core's InMemory provider:
      var options = new DbContextOptionsBuilder<WorkshopContext>()
          .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkshopTestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
          .Options;
      var context = new WorkshopContext(options);
      // db exists?
      //context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      SeedtestDb();
    }

    [TestCleanup]
    public void CleanupDb()
    {
      // Clean up the database after each test
      var options = new DbContextOptionsBuilder<WorkshopContext>()
          .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkshopTestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
          .Options;
      using var context = new WorkshopContext(options);
      // remove Seeded data
      context.Customers.RemoveRange(context.Customers);
      context.SaveChanges();
    }

    private void SeedtestDb()
    {
      var options = new DbContextOptionsBuilder<WorkshopContext>()
          .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkshopTestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
          .Options;
      using var context = new WorkshopContext(options);
      // Seed the database with test data
      context.Customers.AddRange(
          new DomainModels.Customer { Name = "John Doe", Email = "test@test.de", Phone = "987654" }
          );
      context.Customers.AddRange(
          new DomainModels.Customer { Name = "John Doe", Email = "john@test.de", Phone = "321654" }
          );
      context.SaveChanges();

    }

    [TestMethod]
    public void DbContext_CanRetrieveSeededData()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<WorkshopContext>()
          .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkshopTestDb;Trusted_Connection=True;MultipleActiveResultSets=true")
          .Options;
      using var context = new WorkshopContext(options);
      // Act
      var customers = context.Customers.ToList();
      // Assert
      Assert.AreEqual(2, customers.Count);
    }

  }
}
