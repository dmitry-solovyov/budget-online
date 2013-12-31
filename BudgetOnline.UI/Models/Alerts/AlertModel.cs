namespace BudgetOnline.UI.Models.Alerts
{
	public class AlertModel
	{
		public string Name { get; set; }
		public string MessageSuffix { get; set; }
		public string Message { get; set; }
		public bool IsForbidClose { get; set; }
	}
}