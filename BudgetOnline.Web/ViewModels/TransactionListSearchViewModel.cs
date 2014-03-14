using System;
using System.ComponentModel.DataAnnotations;
using BudgetOnline.UI.Attributes;
using BudgetOnline.UI.Models.SelectItems;

namespace BudgetOnline.Web.ViewModels
{
    public class TransactionListSearchViewModel
    {
        public TransactionListSearchViewModel() { CurrentPage = 1; }

        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
        public int? PagesCount { get; set; }

        [GridLayout(12)]
        public SelectItemsModel Pages { get; set; }

        [UIHint("Text")]
        public string Text { get; set; }
        [UIHint("Date")]
        public DateTime? FromDate { get; set; }
        [UIHint("Date")]
        public DateTime? ToDate { get; set; }

        public int? AccountId { get; set; }
        public SelectItemsModel Accounts { get; set; }

        public int? CategoryId { get; set; }
        public SelectItemsModel Categories { get; set; }
    }
}