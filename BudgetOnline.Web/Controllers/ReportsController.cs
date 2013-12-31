using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Models.StatisticsReports;

namespace BudgetOnline.Web.Controllers
{
	public class ReportsController : SecuredController
	{
		public IAccountRepository AccountRepository { get; set; }
		public ICategoryRepository CategoryRepository { get; set; }
		public ITransactionRepository TransactionRepository { get; set; }
		public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
		public ITransactionTagRepository TransactionTagRepository { get; set; }
		public ICurrencyRepository CurrencyRepository { get; set; }

		public IEnumerable<IReport> Reports { get; set; }

		public ReportsController()
		{
			Reports = new IReport[]
			          	{
							new ReportByTags(),
							new ReportByAccounts(),
							new ReportByCategories(),
			          	};
		}

		[HttpGet]
		public ActionResult Index()
		{
			return View(Reports);
		}

		[HttpPost]
		public ActionResult GetReportFilter(string reportCode)
		{
			var report = Reports.FirstOrDefault(o => o.Code.Equals(reportCode));
			if (report == null)
				return Content("<strong>Отчет не доступен</strong>", "text/xml");


			string resultHtml = report.GetFilterHtml(ControllerContext);
			if (string.IsNullOrWhiteSpace(resultHtml))
			{
				resultHtml = "<strong>У выбранного отчета нет параметров</strong>";
			}

			return Content(resultHtml, "text/xml");
		}

		[HttpPost]
		public ActionResult GetReportOutput(FormCollection form)
		{
			var report = Reports.FirstOrDefault(o => o.Code.Equals(form["ReportCode"]));
			if (report == null)
				return Content("<strong>Неверные параметры</strong>", "text/xml");

			string resultHtml = report.GetOutputHtml(ControllerContext);
			if (string.IsNullOrWhiteSpace(resultHtml))
			{
				resultHtml = "<strong>Отчет не доступен</strong>";
			}

			return Content(resultHtml, "text/xml");
		}

		[HttpPost]
		public ActionResult GetData(FormCollection form)
		{
			string tag = form["tag"];

			var search = new TransactionStatisticsSearchOptions();
			search.GroupBy = TimePeriodTypes.Monthly;
			search.Tag = tag;

			var items = Enumerable.Empty<TransactionTotal>();
			if (!string.IsNullOrWhiteSpace(tag))
				items = TransactionStatisticsRepository.GetStatistictsByTag(CurrentUser.SectionId, search);

			return Json(new { Tag = tag, Items = items });
		}

		private void CalculateStatistics1()
		{
			var totals = TransactionRepository.GetListTotals(CurrentUser.SectionId, new TransactionStatisticsSearchOptions());
		}
	}
}
