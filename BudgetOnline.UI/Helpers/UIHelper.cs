using System;
using System.Text;

namespace BudgetOnline.UI.Helpers
{
	public static class UIHelper
	{
		private static readonly Random Random = new Random();

		public static string GenerateRandomCode(int length)
		{
			const string charPool = "ABCDEFGOPQRSTUVWXY1234567890ZabcdefghijklmHIJKLMNnopqrstuvwxyz";
			var sb = new StringBuilder();

			for (int i = 0; i < length; i++)
			{
				sb.Append(charPool[Random.Next(charPool.Length - 1)]);
			}

			return sb.ToString();
		}
	}
}