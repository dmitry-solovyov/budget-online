using System.Linq;
using System.Web;
using DotNet.Highcharts.Options;

namespace BudgetOnline.Web.ViewModels.Statistics
{
	public class TransactionStatisticsViewModel
	{
		public TransactionStatisticsViewModel()
		{
			Series = new Series();
		}

		public string Title { get; set; }
		public string SubTitle { get; set; }

		public string[] xAxis { get; set; }
		public string[] yAxis { get; set; }

		public Series Series { get; private set; }

		#region Formatters

		public HtmlString GetXAxisJson()
		{
			var result = xAxis.Select(item => string.Format("'{0}'", item)).ToList();

			return new HtmlString(
				string.Format("[{0}]", string.Join(", ", result))
				);
		}

		//public HtmlString GetSeries()
		//{
		//    var result = new List<string>();
		//    foreach (var serie in Series.Items)
		//    {
		//        result.Add(
		//                string.Format("{{name: '{0}', data: [{1}]}}",
		//                    serie.Name,
		//                    string.Join(", ", 
		//                        serie.Points.Select(o => string.Format("{{y: {0}, title: '{1}', amount: {2} }}", 
		//                                o.Amount.ToString("#####0.00", CultureInfo.GetCultureInfo("en-GB")),
		//                                o.Title,
		//                                o.Amount.ToString("#####0.00", CultureInfo.GetCultureInfo("en-GB")))
		//                        ).ToArray()
		//                    )
		//                )
		//        );
		//    }

		//    return new HtmlString(
		//        string.Format("[{0}]", string.Join(", \n", result))
		//        );
		//}

		#endregion
	}
}