using System.Web.Mvc;

namespace BudgetOnline.UI.PreCompiled.Extensions
{
	public static class ModelMetadataExtensions
	{
		//private static readonly Random Random = new Random();

		public static string GetSpan(this ModelMetadata metadata, string defaultValue = "")
		{
			if (metadata == null)
				return string.Empty;

			var result = RetrieveAdditionalValue(metadata, "span");
			if (string.IsNullOrWhiteSpace(result))
				return defaultValue;

			return result;
		}

		private static string RetrieveAdditionalValue(ModelMetadata metaData, string key)
		{
			if (metaData.AdditionalValues.ContainsKey(key))
				return metaData.AdditionalValues[key].ToString();

			return string.Empty;
		}
	}
}