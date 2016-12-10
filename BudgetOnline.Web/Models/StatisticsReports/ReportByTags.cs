using System.Web.Mvc;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.UI.Extensions;
using BudgetOnline.Web.Infrastructure.Security;

namespace BudgetOnline.Web.Models.StatisticsReports
{
	public class ReportByTags : IReport
	{
		public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
		public IMembershipHelper MembershipHelper { get; set; }


		public string Code { get { return "report_by_tags"; } }
		public string Name { get { return "Отчет по тегам"; } }
		public string Description { get { return ""; } }

		public string GetFilterHtml(ControllerContext controllerContext)
		{
			return HtmlHelperExtensions.RenderRazorViewToString(controllerContext, "~/Views/Statistics/_ReportByTags_Filter.cshtml", this);
		}

		public string GetOutputHtml(ControllerContext controllerContext)
		{
			var tag = controllerContext.RequestContext.HttpContext.Request.Form["tag"];
			controllerContext.Controller.ViewBag.Tag = tag;

			LoadExternalObjects();

			var totals = TransactionStatisticsRepository.GetStatistictsByTag(
				MembershipHelper.CurrentUser.SectionId,
				new TransactionStatisticsSearchOptions { Tag = tag });


			return HtmlHelperExtensions.RenderRazorViewToString(controllerContext, "~/Views/Statistics/_ReportByTags_Output.cshtml", totals);
		}


		private void LoadExternalObjects()
		{
			if (TransactionStatisticsRepository == null)
				TransactionStatisticsRepository = DependencyResolver.Current.GetService<ITransactionStatisticsRepository>();

			if (MembershipHelper == null)
				MembershipHelper = DependencyResolver.Current.GetService<IMembershipHelper>();
		}
	}
}