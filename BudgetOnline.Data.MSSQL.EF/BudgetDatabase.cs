using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF
{
    public class BudgetDatabase : DbContext
    {
        public BudgetDatabase()
            : base("BudgetDatabase")
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySectionMap> CategorySectionMaps { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencySectionMap> CurrencySectionMaps { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<OperationTypeSectionMap> OperationTypeSectionMaps { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionSystemModuleMap> PermissionSystemModuleMaps { get; set; }
        public DbSet<PermissionSystemModuleUserMap> PermissionSystemModuleUserMaps { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SystemModule> SystemModules { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCorrectionDetail> TransactionCorrectionDetail { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<TransactionTagMap> TransactionTagMaps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserSessionStatus> UserSessionStatuses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<SqlDefaultValueAttribute, string>(
                "SqlDefaultValue",
                (p, attributes) => attributes.Single().DefaultValue));

            BuilderConfiguration.Build(modelBuilder);
        }
    }
}
