namespace BudgetOnline.Highchart.Model
{
	using System.Collections.Generic;
	using System.Linq;

	public class SerieCollection
	{
		private IList<Serie> items = new List<Serie>();
		public Serie[] Items { get { return items.ToArray(); } }

		public void Add(Serie serie) 
		{ 
			items.Add(serie);
		}
	}
}
