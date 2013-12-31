using System;

namespace BudgetOnline.UI.Models
{
    public class DateEditModel
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public bool IsValid { get; set; }
		public int Span { get; set; }
    }
}