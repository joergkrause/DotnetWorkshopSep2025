using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Workshop.Persistence;

public class WorkshopContext : DbContext
{

  public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options)
  {
  }

  public DbSet<DomainModels.Customer> Customers { get; set; } = default!;
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<DomainModels.Customer>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Phone).IsRequired().HasMaxLength(15);
    });

    modelBuilder.Entity<DomainModels.Setting>().ToTable("Settings");
    modelBuilder.Entity<DomainModels.Setting>().HasKey(e => e.Id);
    modelBuilder.Entity<DomainModels.Setting>().HasData(
      new DomainModels.Setting { Id = 1, Name = "NameMaxLength", Value = 100 }
    );
  }

  public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<WorkshopContext>
  {
    public WorkshopContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<WorkshopContext>();
      var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      if (environment == "Development")
      {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WorkshopDevDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        return new WorkshopContext(optionsBuilder.Options);
      }
      throw new NotImplementedException();      
    }
  }

}
