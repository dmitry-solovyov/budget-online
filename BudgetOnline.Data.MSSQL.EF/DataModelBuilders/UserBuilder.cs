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
        }
    }
}
