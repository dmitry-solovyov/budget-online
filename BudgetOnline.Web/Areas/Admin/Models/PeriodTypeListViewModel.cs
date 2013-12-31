using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class PeriodTypeListViewModel
	{
		public int Id { get; set; }
		[DisplayName("Период")]
		public string Name { get; set; }
		[DisplayName("Заблокирован")]
		public bool IsDisabled { get; set; }

		public DateTime CreatedWhen { get; set; }
		public DateTime? UpdatedWhen { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }
	}
}