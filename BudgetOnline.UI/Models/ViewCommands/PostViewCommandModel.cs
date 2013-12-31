using System.Collections.Generic;

namespace BudgetOnline.UI.Models.ViewCommands
{
	public class PostViewCommandModel : ViewCommandModel
	{
		public PostViewCommandModel()
		{
			CommandType = CommandType.Post;
		}

		public string Path { get; set; }
		public IEnumerable<KeyValuePair<string, string>> Parameters { get; set; }

		public override string ToString()
		{
			return string.Empty;
		}
	}
}