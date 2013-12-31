using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BudgetOnline.Web.Models
{
	public class ListWithMultiSelects
	{
		public IEnumerable<SelectListItem> Items { get; set; }

		public ListWithMultiSelects()
		{
			Items = Enumerable.Empty<SelectListItem>();
		}
	}
}