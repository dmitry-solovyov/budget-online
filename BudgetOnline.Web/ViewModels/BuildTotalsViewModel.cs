using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BudgetOnline.UI.Attributes;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.SelectItems;

namespace BudgetOnline.Web.ViewModels
{
    public class BuildTotalsViewModel
    {
		[UIHint("Date")]
		[GridLayout(8)]
        public DateTime FinalDate { get; set; }
		
		[GridLayout(8)]
		public IdWithSelectList TargetCurrency { get; set; }

		[UIHint("Boolean")]
		[Display(Name = "Учитывать текущий остаток")]
		public bool UseActualTotals { get; set; }
        
		public IEnumerable<GeneratedPlannedTransactionListGroupedViewModel> Items { get; set; }

        public BuildTotalsViewModel()
        {
			TargetCurrency = new IdWithSelectList { Id = 0, Items = new SelectItemsModel() };
            Items = Enumerable.Empty<GeneratedPlannedTransactionListGroupedViewModel>();
        	UseActualTotals = true;
        }
    }
}