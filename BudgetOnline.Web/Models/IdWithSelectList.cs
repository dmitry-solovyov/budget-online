using System.Web.Mvc;

namespace BudgetOnline.Web.Models
{
	public class IdWithSelectList
	{
		public int Id { get; set; }
		public SelectList Items { get; set; }
		public SelectList ItemsIconed { get; set; }
	}
}