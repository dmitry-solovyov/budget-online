using System;
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
	public class ReportByAccounts : IReport
	{
		public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
		public IAccountRepository AccountRepository { get; set; }
		public IMembershipHelper MembershipHelper { get; set; }

		public string Code { get { return "report_by_accounts"; } }
		public string Name { get { return "Отчет по счетам"; } }
		public string Description { get { return ""; } }

		public string GetFilterHtml(ControllerContext controllerContext)
		{
			return HtmlHelperExtensions.RenderRazorViewToString(controllerContext, "~/Views/Statistics/_ReportByAccounts_Filter.cshtml", this);
		}

		public string GetOutputHtml(ControllerContext controllerContext)
		{
			var accountText = controllerContext.RequestContext.HttpContext.Request.Form["Account"];
			int account;
			if (!int.TryParse(accountText, NumberStyles.Integer, CultureInfo.CurrentUICulture, out account))
				return "<strong>Неверные параметры</strong>";

			controllerContext.Controller.ViewBag.Account = account;

			LoadExternalObjects();

			var totals = TransactionStatisticsRepository.GetStatistictsByAccount(
				MembershipHelper.CurrentUser.SectionId,
				new TransactionStatisticsSearchOptions { Accounts = new[] { account } });

			return HtmlHelperExtensions.RenderRazorViewToString(controllerContext, "~/Views/Statistics/_ReportByAccounts_Output.cshtml", totals);
		}

		private void LoadExternalObjects()
		{
			if (AccountRepository == null)
				AccountRepository = DependencyResolver.Current.GetService<IAccountRepository>();

			if (MembershipHelper == null)
				MembershipHelper = DependencyResolver.Current.GetService<IMembershipHelper>();

			if (TransactionStatisticsRepository == null)
				TransactionStatisticsRepository = DependencyResolver.Current.GetService<ITransactionStatisticsRepository>();
		}

		public IEnumerable<Account> GetAccounts()
		{
			LoadExternalObjects();

			return AccountRepository.GetList(MembershipHelper.CurrentUser.SectionId).ToList();
		}
	}
}