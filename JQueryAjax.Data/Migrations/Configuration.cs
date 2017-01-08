namespace JQueryAjax.Data.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JQueryAjax.Data.EmployeeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JQueryAjax.Data.EmployeeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Employees.AddOrUpdate(
              new Employee { Name = "Andrew Peters",Salary=3000000,CreatedDate=DateTime.Now,Status=true },
              new Employee { Name = "Brice Lambson", Salary = 4000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Rowan Miller", Salary = 3000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Andrew Peters 2", Salary = 3000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Brice Lambson 2", Salary = 5000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Rowan Miller 2", Salary = 6000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Andrew Peters 3", Salary = 7000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Brice Lambson 3", Salary = 8000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Rowan Miller 3", Salary = 9000000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Andrew Peters 4", Salary = 2400000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Brice Lambson 4", Salary = 6700000, CreatedDate = DateTime.Now, Status = true },
              new Employee { Name = "Rowan Miller 4", Salary = 7800000, CreatedDate = DateTime.Now, Status = true }
            );

        }
    }
}
