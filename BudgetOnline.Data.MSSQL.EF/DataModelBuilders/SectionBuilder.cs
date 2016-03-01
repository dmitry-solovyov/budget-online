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
        }
    }
}
