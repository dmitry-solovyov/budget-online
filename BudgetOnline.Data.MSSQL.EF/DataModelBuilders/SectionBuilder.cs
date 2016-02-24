using System.Data.Entity;
using BudgetOnline.Data.MSSQL.EF.DataModelBuilders.Base;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF.DataModelBuilders
{
    public class SectionBuilder : BaseBuilder<Section>
    {
        public SectionBuilder(DbModelBuilder builder) : base(builder) { }

        public override void Build()
        {
            EntityBuilder.HasKey(x => x.Id);
            EntityBuilder.Property(x => x.Id).IsRequired().HasColumnOrder(1);

            EntityBuilder.Property(x => x.Description)
                .HasMaxLength(512)
                .IsUnicode()
                .IsVariableLength();

            EntityBuilder.Property(x => x.IsDeleted).IsRequired();
            EntityBuilder.Property(x => x.IsDisabled).IsRequired();
        }
    }
}
