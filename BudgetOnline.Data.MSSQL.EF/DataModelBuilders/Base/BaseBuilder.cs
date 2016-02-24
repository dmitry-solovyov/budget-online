using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace BudgetOnline.Data.MSSQL.EF.DataModelBuilders.Base
{
    public abstract class BaseBuilder<T> : IModelBuilder
        where T : class
    {
        protected EntityTypeConfiguration<T> EntityBuilder { get; private set; }

        protected BaseBuilder()
        {
        }

        protected BaseBuilder(DbModelBuilder builder)
        {
            EntityBuilder = builder.Entity<T>();
        }

        public abstract void Build();
    }
}
