using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Common.Helpers;
using BudgetOnline.Data.Manage;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Models.SelectItems;
using BudgetOnline.Web.Infrastructure.Binders;
using BudgetOnline.Web.Infrastructure.Helpers;
using BudgetOnline.Web.ViewModels.Charts;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;


namespace BudgetOnline.Web.Controllers.Statistics
{
    public class TransactionChartsController : SecuredController
    {
        const string TitleOutgoingInPersents = "Процент расходов по категориям";
        const string TitleOutgoingByCategories = "Расходы по категориям";
        const string TitleOutgoingByTags = "Расходы по тэгам";
        private const string TitleTotal = "Итого";
        private const int DefaultPeriodOffsetMonths = 6;
        private const int DefaultPeriodOffsetDays = 14;

        public IDateTimeHelper DateTimeHelpers { get; set; }
        public ITransactionStatisticsRepository TransactionStatisticsRepository { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
        public ICurrencyRateCalculator CurrencyRateCalculator { get; set; }
        public IDictionaries Dictionaries { get; set; }

        [HttpGet]
        public ActionResult CategoriesByPeriodPercents()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = new DateTime(DateTimeProvider.Now().Year, DateTimeProvider.Now().Month, 1).AddMonths(-DefaultPeriodOffsetMonths).Date,
                Date2 = DateTimeProvider.Now().Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Monthly
            };

            var model = PopulateModelPercentageArea(options);

            ViewBag.Title = TitleOutgoingInPersents;

            return View(model);
        }

        [HttpPost]
        public ActionResult CategoriesByPeriodPercents([ModelBinder(typeof(CustomViewModelBinder))]ChartWithCategoriesViewModel model)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = model.FromDate.Date,
                Date2 = model.ToDate.Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Monthly
            };

            var postedModel = PopulateModelPercentageArea(options, model);

            return View(postedModel);
        }

        [HttpGet]
        public ActionResult CategoriesByPeriod()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = new DateTime(DateTimeProvider.Now().Year, DateTimeProvider.Now().Month, 1).AddMonths(-DefaultPeriodOffsetMonths).Date,
                Date2 = DateTimeProvider.Now().Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Monthly
            };

            var model = PopulateModelBasicColumnByCategories(options, new ChartWithCategoriesViewModel());

            ViewBag.Title = TitleOutgoingByCategories;

            return View(model);
        }

        [HttpPost]
        public ActionResult CategoriesByPeriod([ModelBinder(typeof(CustomViewModelBinder))]ChartWithCategoriesViewModel model)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = model.FromDate.Date,
                Date2 = model.ToDate.Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Monthly
            };

            var postedModel = PopulateModelBasicColumnByCategories(options, model);

            return View(postedModel);
        }

        [HttpGet]
        public ActionResult TagsByMonths()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = new DateTime(DateTimeProvider.Now().Year, DateTimeProvider.Now().Month, 1).AddMonths(-DefaultPeriodOffsetMonths).Date,
                Date2 = DateTimeProvider.Now().Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Monthly,
            };

            var model = PopulateModelBasicColumnByTags(options, new TagsByDatesViewModel());

            ViewBag.Title = TitleOutgoingByCategories;

            return View(model);
        }

        [HttpPost]
        public ActionResult TagsByMonths([ModelBinder(typeof(CustomViewModelBinder))]TagsByDatesViewModel model)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = model.FromDate.Date,
                Date2 = model.ToDate.Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Monthly,
                Tag = model.Tags
            };

            var postedModel = PopulateModelBasicColumnByTags(options, model, TitleOutgoingByTags);

            return View(postedModel);
        }


        [HttpGet]
        public ActionResult CategoriesByDays()
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = DateTimeProvider.Now().AddDays(-DefaultPeriodOffsetDays).Date,
                Date2 = DateTimeProvider.Now().Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Daily
            };

            var model = PopulateModelByDates(options, new ChartWithCategoriesViewModel());

            ViewBag.Title = TitleOutgoingByCategories;

            return View(model);
        }

        [HttpPost]
        public ActionResult CategoriesByDays([ModelBinder(typeof(CustomViewModelBinder))]ChartWithCategoriesViewModel model)
        {
            var options = new TransactionStatisticsSearchOptions
            {
                Date1 = model.FromDate.Date,
                Date2 = model.ToDate.Date,
                SumSign = -1,
                GroupBy = TimePeriodTypes.Daily
            };

            var postedModel = PopulateModelByDates(options, model);

            return View(postedModel);
        }

        private ChartWithCategoriesViewModel PopulateModelPercentageArea(TransactionStatisticsSearchOptions options, ChartWithCategoriesViewModel postModel = null)
        {
            var dates = DateTimeHelpers.SequenceOfDates(options.Date1.Value, options.Date2.Value, options.GroupBy ?? TimePeriodTypes.Monthly).ToList();

            SelectItemsModel listOfLegends;
            var series = GetSeriesHelper(options, 0, postModel != null ? postModel.Categories : null, false, ExtractByCategories, () => TitleTotal, out listOfLegends);

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Area })
                .SetTitle(new Title { Text = TitleOutgoingInPersents })
                .SetSubtitle(new Subtitle
                {
                    Text =
                        string.Format("Период: {0} to {1}", options.Date1.Value.ToLocalTime().Date.ToShortDateString(),
                                      options.Date2.Value.ToLocalTime().Date.ToShortDateString())
                })
                .SetXAxis(new XAxis
                {
                    Categories = dates.Select(o => o.ToString("MMM", CultureInfo.CurrentUICulture)).ToArray(),
                    TickmarkPlacement = Placement.On,
                    Labels = new XAxisLabels { Rotation = 300, Enabled = true, Y = 20 },
                })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Процент" } })
                .SetTooltip(new Tooltip
                {
                    Formatter =
                        "function() { return this.x +': '+ Highcharts.numberFormat(this.percentage, 1) +'% ('+ Highcharts.numberFormat(this.y, 0, ',') +' " + " грн." + ")'; }"
                })
                .SetPlotOptions(new PlotOptions
                {
                    Area = new PlotOptionsArea
                    {
                        Stacking = Stackings.Percent,
                        LineColor = ColorTranslator.FromHtml("#ffffff"),
                        LineWidth = 1,
                        Marker = new PlotOptionsAreaMarker
                        {
                            LineWidth = 1,
                            LineColor = ColorTranslator.FromHtml("#ffffff")
                        }
                    }
                })
                .SetSeries(
                    series.GetData().Select(
                        o =>
                        new Series { Name = o.Name, Data = new DotNet.Highcharts.Helpers.Data(o.Data.Select(x => (object)x).ToArray()) }).
                        ToArray());

            return new ChartWithCategoriesViewModel
            {
                FromDate = options.Date1.Value.ToLocalTime().Date,
                ToDate = options.Date2.Value.ToLocalTime().Date,
                Chart = chart,
                Categories = listOfLegends
            };

        }

        private ChartWithCategoriesViewModel PopulateModelBasicColumnByCategories(TransactionStatisticsSearchOptions options, ChartWithCategoriesViewModel postModel = null)
        {
            var dates = DateTimeHelpers.SequenceOfDates(options.Date1.Value.ToLocalTime(), options.Date2.Value.ToLocalTime(), options.GroupBy ?? TimePeriodTypes.Monthly).ToList();

            SelectItemsModel listOfLegends;
            var series = GetSeriesHelper(
                options,
                (postModel == null ? 5 : 0),
                postModel.Categories,
                postModel == null || postModel.IsOnlyTotals, ExtractByCategories,
                () => TitleTotal,
                out listOfLegends);


            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = TitleOutgoingByCategories })
                .SetSubtitle(new Subtitle
                {
                    Text =
                        string.Format("Период: {0} to {1}", options.Date1.Value.Date.ToShortDateString(),
                                      options.Date2.Value.Date.ToShortDateString())
                })
                .SetXAxis(new XAxis
                {
                    Categories = dates.Select(o => o.ToString("MMM", CultureInfo.CurrentUICulture)).ToArray(),
                    TickmarkPlacement = Placement.On,
                    Labels = new XAxisLabels { Rotation = 300, Enabled = true, Y = 20 },
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle { Text = "грн." }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Horizontal,
                    Align = HorizontalAligns.Center,
                    VerticalAlign = VerticalAligns.Bottom,
                    Floating = false,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' грн.'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0.2,
                        BorderWidth = 0,
                        Animation = new Animation(false),
                        DataLabels = new PlotOptionsColumnDataLabels { Align = HorizontalAligns.Center, Enabled = true }
                    },
                })
                .SetSeries(series.GetData().Select(
                        o =>
                        new Series { Name = o.Name, Data = new DotNet.Highcharts.Helpers.Data(o.Data.Select(x => (object)x).ToArray()) }).
                        ToArray());

            return new ChartWithCategoriesViewModel
            {
                FromDate = options.Date1.Value.Date,
                ToDate = options.Date2.Value.Date,
                Chart = chart,
                Categories = listOfLegends
            };
        }

        private TagsByDatesViewModel PopulateModelBasicColumnByTags(TransactionStatisticsSearchOptions options, TagsByDatesViewModel postModel = null, string title = TitleOutgoingByCategories)
        {
            var dates = DateTimeHelpers.SequenceOfDates(options.Date1.Value.ToLocalTime(), options.Date2.Value.ToLocalTime(), options.GroupBy ?? TimePeriodTypes.Monthly).ToList();

            SelectItemsModel listOfLegends;
            var series = GetSeriesHelper(options, (postModel == null ? 5 : 0), null, true, ExtractByDates, () => options.Tag, out listOfLegends);


            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = title })
                .SetSubtitle(new Subtitle
                {
                    Text =
                        string.Format("Период: {0} to {1}", options.Date1.Value.Date.ToShortDateString(),
                                      options.Date2.Value.Date.ToShortDateString())
                })
                .SetXAxis(new XAxis
                {
                    Categories = dates.Select(o => o.ToString("MMM", CultureInfo.CurrentUICulture)).ToArray(),
                    TickmarkPlacement = Placement.On,
                    Labels = new XAxisLabels { Rotation = 300, Enabled = true, Y = 20 },
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle { Text = "грн." }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Horizontal,
                    Align = HorizontalAligns.Center,
                    VerticalAlign = VerticalAligns.Bottom,
                    Floating = false,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' грн.'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0.2,
                        BorderWidth = 0,
                        Animation = new Animation(false),
                        DataLabels = new PlotOptionsColumnDataLabels { Align = HorizontalAligns.Center, Enabled = true }
                    },
                })
                .SetSeries(series.GetData().Select(
                        o =>
                        new Series { Name = o.Name, Data = new DotNet.Highcharts.Helpers.Data(o.Data.Select(x => (object)x).ToArray()) }).
                        ToArray());

            return new TagsByDatesViewModel
            {
                FromDate = options.Date1.Value.Date,
                ToDate = options.Date2.Value.Date,
                Chart = chart,
                Tags = postModel != null ? postModel.Tags : string.Empty,
                TagsList = listOfLegends
            };
        }

        private ChartWithCategoriesViewModel PopulateModelByDates(TransactionStatisticsSearchOptions options, ChartWithCategoriesViewModel postModel = null)
        {
            var dates = DateTimeHelpers.SequenceOfDates(options.Date1.Value, options.Date2.Value, options.GroupBy ?? TimePeriodTypes.Daily).ToList();

            SelectItemsModel listOfLegends;
            var series = GetSeriesHelper(options, (postModel == null ? 5 : 0), postModel.Categories, postModel == null || postModel.IsOnlyTotals, ExtractByCategories, () => TitleTotal, out listOfLegends);


            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = TitleOutgoingByCategories })
                .SetSubtitle(new Subtitle
                {
                    Text =
                        string.Format("Период: {0} to {1}", options.Date1.Value.Date.ToShortDateString(),
                                      options.Date2.Value.Date.ToShortDateString())
                })
                .SetXAxis(new XAxis
                {
                    Categories = dates.Select(o => o.ToString("dd.MM", CultureInfo.CurrentUICulture)).ToArray(),
                    TickmarkPlacement = Placement.On,
                    Labels = new XAxisLabels { Rotation = 300, Enabled = true, Y = 20 },
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle { Text = "грн." }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Horizontal,
                    Align = HorizontalAligns.Center,
                    VerticalAlign = VerticalAligns.Bottom,
                    Floating = false,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' грн.'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0.2,
                        BorderWidth = 0,
                        Animation = new Animation(false),
                        DataLabels = new PlotOptionsColumnDataLabels { Align = HorizontalAligns.Center, Enabled = true, }
                    },
                })
                .SetSeries(series.GetData().Select(
                        o =>
                        new Series { Name = o.Name, Data = new DotNet.Highcharts.Helpers.Data(o.Data.Select(x => (object)x).ToArray()) }).
                        ToArray());

            return new ChartWithCategoriesViewModel
            {
                FromDate = options.Date1.Value.Date,
                ToDate = options.Date2.Value.Date,
                Chart = chart,
                Categories = listOfLegends
            };
        }

        private IEnumerable<TransactionTotal> ExtractByCategories(TransactionStatisticsSearchOptions options)
        {
            var totals = TransactionStatisticsRepository.GetStatistictsByCategory(CurrentUser.SectionId, options)
                .Where(o => o.CategoryId.HasValue)
                .ToList();

            var defaultCurrencyId = GetDefaultCurrencyId();

            totals = CurrencyRateCalculator.ConvertCurrency(totals, defaultCurrencyId).ToList();

            var result = totals.GroupBy(o => new { o.CategoryId, o.CategoryName, o.Date })
                .Select(
                    o =>
                    new TransactionTotal { Date = o.Key.Date, CategoryId = o.Key.CategoryId, CategoryName = o.Key.CategoryName, Sum = o.Sum(x => x.Sum) })
                .OrderByDescending(o => Math.Abs(o.Sum))
                .ToList();

            return result;
        }

        private IEnumerable<TransactionTotal> ExtractByDates(TransactionStatisticsSearchOptions options)
        {
            var totals = TransactionStatisticsRepository.GetStatistictsByTag(CurrentUser.SectionId, options)
                .ToList();

            var defaultCurrencyId = GetDefaultCurrencyId();

            totals = CurrencyRateCalculator.ConvertCurrency(totals, defaultCurrencyId).ToList();

            var result = totals.GroupBy(o => new { o.Date })
                .Select(
                    o =>
                    new TransactionTotal { Date = o.Key.Date, CategoryId = 0, CategoryName = options.Tag, Sum = o.Sum(x => x.Sum) })
                .OrderByDescending(o => Math.Abs(o.Sum))
                .ToList();

            return result;
        }

        private ChartSeriesHelper GetSeriesHelper(TransactionStatisticsSearchOptions options, int takeRows, SelectItemsModel categories,
            bool onlyTotals,
            Func<TransactionStatisticsSearchOptions, IEnumerable<TransactionTotal>> transactionTotalsExtractor,
            Func<string> totalCaptionExtractor,
            out SelectItemsModel listOfLegends)
        {
            var periodType = options.GroupBy ?? TimePeriodTypes.Monthly;

            var totals = transactionTotalsExtractor(options).ToList();

            var selected = new SelectItemsModel
            {
                Items = totals.Select(o => new { o.CategoryId, o.CategoryName }).Distinct().Select(o => new SelectItemModel { Selected = true, Text = o.CategoryName, Value = o.CategoryId.Value.ToString(CultureInfo.CurrentUICulture) }),
            };

            if (categories != null)
                selected.MergeSelected(categories.Items);

            if (takeRows > 0 && selected.Items.Count(o => o.Selected) > takeRows)
            {
                var selectedValues = selected.Items.Where(o => o.Selected).Take(takeRows).Select(o => o.Value);

                selected.Items =
                    selected.Items
                        .Select(o => new SelectItemModel { Selected = selectedValues.Contains(o.Value), Text = o.Text, Value = o.Value });
            }

            var dates = DateTimeHelpers.SequenceOfDates(options.Date1.Value, options.Date2.Value, periodType).ToList();
            var series = new ChartSeriesHelper();
            var usedCategories = selected.Items.Where(o => o.Selected).ToList();

            foreach (var date in dates)
            {
                if (!onlyTotals)
                    foreach (var category in usedCategories)
                    {
                        var categoryid = int.Parse(category.Value);
                        var sum = totals.Where(o => DateTimeHelpers.IsEqualDates(periodType, o.Date.Value, date) && o.CategoryId == categoryid).Sum(o => o.Sum);
                        series.Add(category.Text, Math.Abs(sum));
                    }

                if (onlyTotals)
                {
                    var sum = totals.Where(o => DateTimeHelpers.IsEqualDates(periodType, o.Date.Value, date)
                        && usedCategories.Any(x => x.Value.Equals(o.CategoryId.ToString()))).Sum(o => o.Sum);
                    series.Add(totalCaptionExtractor(), Math.Abs(sum));
                }
            }

            listOfLegends = selected;

            return series;
        }

        private int GetDefaultCurrencyId()
        {
            var currencies = Dictionaries.Currencies();

            var defaultCurrency = currencies.FirstOrDefault(o => o.IsDefault);
            if (defaultCurrency != null)
                return defaultCurrency.Id;

            return 0;
        }
    }
}
