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

        public DbSet<AccountRecord> Accounts { get; set; }
        public DbSet<AdminUserRecord> AdminUsers { get; set; }
        public DbSet<CategoryRecord> Categories { get; set; }
        public DbSet<CategorySectionMapRecord> CategorySectionMaps { get; set; }
        public DbSet<CurrencyRecord> Currencies { get; set; }
        public DbSet<CurrencySectionMapRecord> CurrencySectionMaps { get; set; }
        public DbSet<OperationTypeRecord> OperationTypes { get; set; }
        public DbSet<OperationTypeSectionMapRecord> OperationTypeSectionMaps { get; set; }
        public DbSet<PermissionRecord> Permissions { get; set; }
        public DbSet<PermissionSystemModuleMapRecord> PermissionSystemModuleMaps { get; set; }
        public DbSet<PermissionSystemModuleUserMapRecord> PermissionSystemModuleUserMaps { get; set; }
        public DbSet<SectionRecord> Sections { get; set; }
        public DbSet<SettingRecord> Settings { get; set; }
        public DbSet<SystemModuleRecord> SystemModules { get; set; }
        public DbSet<TagRecord> Tags { get; set; }
        public DbSet<TransactionRecord> Transactions { get; set; }
        public DbSet<TransactionCorrectionDetailRecord> TransactionCorrectionDetail { get; set; }
        public DbSet<TransactionDetailRecord> TransactionDetails { get; set; }
        public DbSet<TransactionTagMapRecord> TransactionTagMaps { get; set; }
        public DbSet<UserRecord> Users { get; set; }
        public DbSet<UserPasswordRecord> UserPasswords { get; set; }
        public DbSet<UserSessionRecord> UserSessions { get; set; }
        public DbSet<UserSessionStatusRecord> UserSessionStatuses { get; set; }


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
