using System;

namespace BudgetOnline.Web.ViewModels.Budgeting
{
	public class BudgetState
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public int CurrencyId { get; set; }
		public string CurrencyName { get; set; }
		public string CurrencySymbol { get; set; }
	
		public decimal PlanOutcome { get; set; }
		public decimal PlanOutcomeDayAvg { get; set; }

		public decimal PlanIncome { get; set; }
		public decimal PlanIncomeDayAvg { get; set; }

		public decimal RealOutcome { get; set; }
		public decimal RealOutcomeDayAvg { get; set; }
		
		public decimal RealIncome { get; set; }
		public decimal RealIncomeDayAvg { get; set; }

		public decimal PlanDaysInPeriod { get; set; }
		public decimal RealDaysInPeriod { get; set; }

		public decimal PlanOutcomeCompletion { get; set; }
	}
}