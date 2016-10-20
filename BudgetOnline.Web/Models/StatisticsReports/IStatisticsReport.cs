using System.Web.Mvc;

namespace BudgetOnline.Web.Models.StatisticsReports
{
	public interface IReport
	{
		string Code { get; }
		string Name { get; }
		string Description { get; }
		string GetFilterHtml(ControllerContext controllerContext);
		string GetOutputHtml(ControllerContext controllerContext);
	}
}