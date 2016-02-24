using System;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Db.BudgetDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Db.BudgetDatabase context)
        {
            context.Users.AddOrUpdate(
              p => p.Id,
              new User
              {
                  Id = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                  Email = "admin@budget.com",
                  UserName = "Domain Admin",
                  CreatedBy = new Guid("{169D36CE-7556-4CCA-A715-07CA0D008310}"),
                  CreatedWhen = DateTime.UtcNow,
                  SectionId = null,
                  IsDisabled = false
              }
            );

            context.SaveChanges();
        }
    }
}
