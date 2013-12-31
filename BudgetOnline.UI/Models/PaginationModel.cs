using System;
using System.Linq;

namespace BudgetOnline.UI.Models
{
	public class PaginationModel
	{
		public string Name { get; set; }
		public PaginationControlSizes ControlSize { get; set; }
		public int Page { get; set; }
		public int PagesCount { get; set; }

		public int[] GetPages()
		{
			const int visiblePages = 4;
			int[] result;

			if (visiblePages > PagesCount)
				result = Enumerable.Range(1, PagesCount).ToArray();
			else
			{
				int halfOfPages = Convert.ToInt32(Convert.ToDecimal(visiblePages - 1) / 2);

				if (PagesCount < visiblePages)
					return new int[PagesCount];

				if (Page <= halfOfPages + 1)
					result = Enumerable.Range(1, visiblePages).ToArray();
				else if (Page > PagesCount - halfOfPages)
					result = Enumerable.Range(PagesCount - visiblePages + 1, visiblePages).ToArray();
				else
					result = Enumerable.Range(Page - halfOfPages, visiblePages).ToArray();
			}

			return result;
		}

	}

	public enum PaginationControlSizes
	{
		Normal,
		Large,
		Small,
	}
}