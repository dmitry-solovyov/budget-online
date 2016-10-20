using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Infrastructure.Security;

namespace BudgetOnline.Web.Models.StatisticsReports
{
	public class ReportByCategories : IReport
	{
		public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
		public ICategoryRepository CategoryRepository { get; set; }
		public IMembershipHelper MembershipHelper { get; set; }

		public string Code { get { return "report_by_categories"; } }
		public string Name { get { return "Отчет по категориям"; } }
		public string Description { get { return ""; } }

		public string GetFilterHtml(ControllerContext controllerContext)
		{
			return HtmlHelperExtensions.RenderRazorViewToString(controllerContext, "~/Views/Statistics/_ReportByCategories_Filter.cshtml", this);
		}

		public string GetOutputHtml(ControllerContext controllerContext)
		{
			var categoryText = controllerContext.RequestContext.HttpContext.Request.Form["Category"];
			int category;
			if (!int.TryParse(categoryText, NumberStyles.Integer, CultureInfo.CurrentUICulture, out category))
				return "<strong>Неверные параметры</strong>";

			controllerContext.Controller.ViewBag.Category = category;

			LoadExternalObjects();

			var totals = TransactionStatisticsRepository.GetStatistictsByCategory(
				MembershipHelper.CurrentUser.SectionId,
				new TransactionStatisticsSearchOptions { Categories = new[] { category } });

			return HtmlHelperExtensions.RenderRazorViewToString(controllerContext, "~/Views/Statistics/_ReportByCategories_Output.cshtml", totals);
		}


		private void LoadExternalObjects()
		{
			if (CategoryRepository == null)
				CategoryRepository = DependencyResolver.Current.GetService<ICategoryRepository>();

			if (MembershipHelper == null)
				MembershipHelper = DependencyResolver.Current.GetService<IMembershipHelper>();

			if (TransactionStatisticsRepository == null)
				TransactionStatisticsRepository = DependencyResolver.Current.GetService<ITransactionStatisticsRepository>();
		}

		public IEnumerable<Category> GetCategories()
		{
			LoadExternalObjects();

			return CategoryRepository.GetList(MembershipHelper.CurrentUser.SectionId).ToList();
		}
	}
}