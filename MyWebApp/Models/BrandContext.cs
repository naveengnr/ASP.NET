using Microsoft.EntityFrameworkCore;

namespace CrudOperationsInNetCore.Model
{
   public class BrandContext : DbContext
   {

      public BrandContext(DbContextOptions<BrandContext> options) : base(options)
      {

      }
      public DbSet<Employee> employee {get; set;}

   }

}