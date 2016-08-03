using System.Data.Entity;
using BudgetOnline.Data.MSSQL.EF.DataModelBuilders.Base;
using BudgetOnline.Data.MSSQL.EF.DataModels;

namespace BudgetOnline.Data.MSSQL.EF.DataModelBuilders
{
    public class AccountBuilder : BaseBuilder<SectionRecord>
    {
        public AccountBuilder(DbModelBuilder builder) : base(builder) { }

        public override void Build()
        {
        }
    }
}
