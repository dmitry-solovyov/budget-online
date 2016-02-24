using System.Data.Entity;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF.Db
{
    public class BudgetDatabase : DbContext
    {
        public BudgetDatabase()
            : base("BudgetDatabase")
        {
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            BuilderConfiguration.Build(modelBuilder);
        }
    }
}
