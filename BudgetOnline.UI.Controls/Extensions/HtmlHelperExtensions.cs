using System.Text;

namespace System.Web.Mvc
{
	public static class HtmlHelperExtensions
	{
		private static readonly Random Random = new Random();

		public static string GetUniqId(this HtmlHelper helper, int length)
		{
			return GenerateRandomCode(length);
		}

		private static string GenerateRandomCode(int length)
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