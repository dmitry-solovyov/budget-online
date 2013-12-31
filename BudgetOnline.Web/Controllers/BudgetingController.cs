using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.ViewModels.Budgeting;

namespace BudgetOnline.Web.Controllers
{
    public class BudgetingController : BaseController
    {
        private readonly string[] _excludeTags = new[] { "корректировка", "Верховинная" };

        public ITransactionRepository TransactionRepository { get; set; }
        public ITransactionCalculator TransactionCalculator { get; set; }
        public IPlannedTransactionRepository PlannedTransactionRepository { get; set; }
        public IPlannedTransactionCalculator PlannedTransactionCalculator { get; set; }
        public ICurrencyRepository CurrencyRepository { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
        public IDictionaries Dictionaries { get; set; }
        public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }

        public ActionResult StateByCurrentMonth()
        {
            var currency = Dictionaries.Currencies().FirstOrDefault(o => o.IsDefault);
            if (currency == null)
                return View();

            var model = new BudgetState
                            {
                                FromDate = new DateTime(DateTimeProvider.Now().Year, DateTimeProvider.Now().Month, 1),
                                ToDate = new DateTime(DateTimeProvider.Now().Year, DateTimeProvider.Now().Month, 1).AddMonths(1).AddDays(-1),
                                CurrencyId = currency.Id,
                                CurrencyName = currency.Name,
                                CurrencySymbol = currency.Symbol
                            };

            try
            {
                PopulateBudgetState(model, currency.Id);
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            var content = HtmlHelperExtensions.RenderRazorViewToString(ControllerContext,
                                                            "~/Views/Budgeting/StateByCurrentMonth.cshtml", model);

            if (Request.IsAjaxRequest())
            {
                var jsonResult = new JsonResult
                {
                    Data = new
                    {
                        Content = content,
                        UpdateTime = DateTimeProvider.Now().ToShortDateString() + " " + DateTimeProvider.Now().ToLongTimeString()
                    }
                };

                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }

            return new ContentResult { Content = content };

        }

        private void PopulateBudgetState(BudgetState model, int targetCurrencyId)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = model.FromDate,
                Date2 = model.ToDate,
                TransactionTypes = new[] { (int)TransactionTypes.Income, (int)TransactionTypes.Outcome },
                ExcludeTags = _excludeTags
            };

            var selectedCurrency = CurrencyRepository.Get(targetCurrencyId);
            if (selectedCurrency != null)
            {
                model.CurrencyId = selectedCurrency.Id;
                model.CurrencyName = selectedCurrency.Name;
            }

            var realTotals = TransactionRepository.GetListTotals(CurrentUser.SectionId, options).ToList();

            model.RealIncome = CurrencyRateCalculator.ConvertCurrency(realTotals.Where(o => !o.SumNegative), targetCurrencyId).Sum(o => o.Sum);
            model.RealOutcome = CurrencyRateCalculator.ConvertCurrency(realTotals.Where(o => o.SumNegative), targetCurrencyId).Sum(o => o.Sum);

            var plannedTotals = GetPlannedTotals(model.FromDate, model.ToDate, targetCurrencyId).ToList();

            model.PlanIncome = Math.Round(plannedTotals.Where(o => !o.SumNegative).Sum(o => o.Sum), 2);
            model.PlanOutcome = Math.Round(plannedTotals.Where(o => o.SumNegative).Sum(o => o.Sum), 2);

            model.PlanDaysInPeriod = Convert.ToInt32(model.ToDate.Subtract(model.FromDate).TotalDays);
            if (model.ToDate > DateTimeProvider.Now(DateTimeKind.Utc))
                model.RealDaysInPeriod = Convert.ToInt32(DateTimeProvider.Now(DateTimeKind.Utc).Subtract(model.FromDate).TotalDays);
            else
                model.RealDaysInPeriod = model.PlanDaysInPeriod;
            //var d = DateTime.DaysInMonth(DateTimeProvider.UtcNow().Year, DateTimeProvider.UtcNow().Month);

            if (model.RealDaysInPeriod > 0)
            {
                model.RealOutcomeDayAvg = Math.Round(model.RealOutcome / model.RealDaysInPeriod, 2);
                model.RealIncomeDayAvg = Math.Round(model.RealIncome / model.RealDaysInPeriod, 2);
            }
            else
            {
                model.RealOutcomeDayAvg = 0m;
                model.RealIncomeDayAvg = 0m;
            }

            model.PlanOutcomeDayAvg = Math.Round(model.PlanOutcome / model.PlanDaysInPeriod, 2);
            model.PlanIncomeDayAvg = Math.Round(model.PlanIncome / model.PlanDaysInPeriod, 2);

            //var realDaysByCalculatedPerDayPlan = Convert.ToInt32(Math.Ceiling(model.RealOutcome / model.PlanOutcomeDayAvg));

            model.PlanOutcomeCompletion = Math.Round(Math.Abs(model.RealOutcome) / Math.Abs(model.PlanOutcome) * 100, 2);
        }


        private IEnumerable<TransactionTotal> GetPlannedTotals(DateTime dateFrom, DateTime dateTo, int targetCurrencyId)
        {
            var options = new PlannedTransactionSearchOptions
                            {
                                Date1 = dateFrom,
                                Date2 = dateTo,
                                ExcludeTags = _excludeTags
                            };

            var plannedTotals = PlannedTransactionCalculator.GenerateActual(CurrentUser.SectionId, options, targetCurrencyId, false);

            return PlannedTransactionCalculator.GroupBy(plannedTotals, TransactionTotalGroupTypes.MonthSign);
        }
    }
}
