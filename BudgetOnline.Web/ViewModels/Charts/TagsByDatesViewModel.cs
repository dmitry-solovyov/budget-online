using System;
using System.ComponentModel.DataAnnotations;
using BudgetOnline.UI.Attributes;
using BudgetOnline.UI.Models.SelectItems;

namespace BudgetOnline.Web.ViewModels.Charts
{
    public class TagsByDatesViewModel : ChartViewModel
	{
        public SelectItemsModel TagsList { get; set; }
        [UIHint("Date")]
        public DateTime FromDate { get; set; }
        [UIHint("Date")]
        public DateTime ToDate { get; set; }

        [UIHint("Autocomplete")]
        [AutocompleteOption("GetTags", "Transactions")]
        public string Tags { get; set; }
	}
}