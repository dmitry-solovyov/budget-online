using System.Collections.Generic;
using System.Linq;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
	public class ChartSeriesHelper
	{
		private readonly Dictionary<string, List<decimal>> _data = new Dictionary<string, List<decimal>>();

		public void Add(string name, decimal value)
		{
			if (!_data.ContainsKey(name))
			{
				_data.Add(name, new List<decimal>());
			}

			var dataItem = _data[name];
			dataItem.Add(value);

			_data[name] = dataItem;
		}

		public IEnumerable<ChartSerie> GetData()
		{
			return _data.Keys.Select(o => new ChartSerie{Name = o, Data = _data[o].ToArray()});
		}
	}

	public class ChartSerie
	{
		public string Name { get; set; }
		public decimal[] Data { get; set; }
	}
}