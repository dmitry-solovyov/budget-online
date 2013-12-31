using BudgetOnline.UI.Helpers;

namespace BudgetOnline.UI.Models
{
	public abstract class UIControlModel
	{
		public string Name { get; set; }

		private string _uniqeId;
		public string UniqeId
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_uniqeId))
					_uniqeId = UIHelper.GenerateRandomCode(10);

				return _uniqeId;
			}
		}
	}
}