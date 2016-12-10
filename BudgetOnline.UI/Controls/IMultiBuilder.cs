using System.Web;
using BudgetOnline.UI.Controls.Tables;

namespace BudgetOnline.UI.Controls
{
	public interface IMultiBuilder<in TBuildType, in TModel>
	{
        HtmlString Build(TableDefinitions tableDefinitions, TBuildType type);
        HtmlString Build(TableDefinitions tableDefinitions, TBuildType type, TModel context);
	}
}
