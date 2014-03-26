using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.Attributes
{
	public class GridLayout : Attribute, IMetadataAware
	{
		private readonly int _span;
		public GridLayout(int span)
		{
			Validate(span);
			_span = span;
		}

		private void Validate(int span)
		{
			if (span < 0 || span > 12)
				throw new ArgumentException("Invalid span value");
		}

		#region Implementation of IMetadataAware

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			if (_span > 0)
				metadata.AdditionalValues["span"] = string.Format("col-md-{0}", _span);
		}

		#endregion
	}
}
