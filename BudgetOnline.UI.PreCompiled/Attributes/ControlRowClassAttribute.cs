﻿using System;
using System.Web.Mvc;

namespace BudgetOnline.UI.PreCompiled.Attributes
{
	public class ControlRowClassAttribute : Attribute, IMetadataAware
	{
		private readonly string _style;
		public ControlRowClassAttribute(string style)
		{
			_style = style;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["ControlRowClass"] = _style;
		}

		public string Title
		{
			get { return _style; }
		}
	}
}
