using System;
using System.ComponentModel.DataAnnotations;
using BudgetOnline.UI.Models.SelectItems;

namespace BudgetOnline.Web.ViewModels.Charts
{
	public class ChartWithCategoriesViewModel : ChartViewModel
	{
		public SelectItemsModel Categories { get; set; }
        [UIHint("Date")]
		public DateTime FromDate { get; set; }
        [UIHint("Date")]
		public DateTime ToDate { get; set; }
        [Display(Name = "Показать только итоги")]
        public bool IsOnlyTotals { get; set; }

        public ChartWithCategoriesViewModel()
        {
            IsOnlyTotals = true;
        }
	}
}