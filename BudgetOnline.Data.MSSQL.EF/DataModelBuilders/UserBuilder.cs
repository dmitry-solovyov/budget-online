using System.Data.Entity;
using BudgetOnline.Data.MSSQL.EF.DataModelBuilders.Base;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF.DataModelBuilders
{
    public class UserBuilder : BaseBuilder<User>
    {
        public UserBuilder(DbModelBuilder builder) : base(builder) { }

        public override void Build()
        {
            EntityBuilder.HasKey(x => x.Id);
            EntityBuilder.Property(x => x.Id).IsRequired().HasColumnOrder(1);

            EntityBuilder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired()
                .IsVariableLength();

            EntityBuilder.Property(x => x.UserName)
                .HasMaxLength(255)
                .IsRequired()
                .IsVariableLength();

            EntityBuilder
                .HasOptional(x => x.Section)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.SectionId);
        }
    }
}
