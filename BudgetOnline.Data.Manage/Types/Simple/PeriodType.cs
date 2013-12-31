using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class PeriodType
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		
		public Int16? Hours { get; set; }
		public Int16? Days { get; set; }
		public Int16? WorkingDays { get; set; }
		public Int16? Weeks { get; set; }
		public Int16? Monthes { get; set; }
		public Int16? Quarters { get; set; }
		public Int16? Years { get; set; }

		public bool IsDefault { get; set; }
        public bool IsDisabled { get; set; }
	}
}
