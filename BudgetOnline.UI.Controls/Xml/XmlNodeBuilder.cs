using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace BudgetOnline.UI.Controls.Xml
{
	public class XmlNodeBuilder<T> : IBuilder
		where T : class, IBuilder
	{
		protected string ContainerTag = "div";
		protected bool IsHideIfEmpty = false;
		protected Func<HtmlString> ContentExtractor;
		protected List<Tuple<string, string>> ListOfAttributes;
		protected List<Func<HtmlString>> ListOfChilds;
		protected XmlNodeBuilderSettings XmlSettings;

		public XmlNodeBuilder()
		{

		}

		public XmlNodeBuilder(XmlNodeBuilderSettings settings)
		{
			XmlSettings = settings;
		}

		private List<Tuple<string, string>> GetAttr()
		{
			return ListOfAttributes ?? (ListOfAttributes = new List<Tuple<string, string>>());
		}

		private List<Func<HtmlString>> GetChilds()
		{
			return ListOfChilds ?? (ListOfChilds = new List<Func<HtmlString>>());
		}

		public virtual T Id(string id)
		{
			GetAttr().Add(new Tuple<string, string>("id", id));

			return this as T;
		}

		public virtual T Tag(string tag)
		{
			ContainerTag = tag;
			return this as T;
		}

		public virtual T Content(string content)
		{
			if (content != null)
				ContentExtractor = () => new HtmlString(content);

			return this as T;
		}

		public virtual T Content(HtmlString content)
		{
			if (content != null)
				ContentExtractor = () => content;

			return this as T;
		}

		public virtual T Content(Func<HtmlString> content)
		{
			if (content != null)
				ContentExtractor = content;

			return this as T;
		}

		public virtual T Content(IBuilder builder)
		{
			ContentExtractor = builder.Build;
			return this as T;
		}

		public virtual T Attr(string attr, string value)
		{
			if (string.IsNullOrWhiteSpace(attr) || string.IsNullOrWhiteSpace(value))
				return this as T;

			GetAttr().Add(new Tuple<string, string>(attr, value));
			return this as T;
		}

		public virtual T Child(Func<IBuilder> child)
		{
			GetChilds().Add(child().Build);
			return this as T;
		}

		public virtual T Child(Func<HtmlString> child)
		{
			GetChilds().Add(child);
			return this as T;
		}

		public virtual T HideIfEmpty(bool hideIfEmpty)
		{
			IsHideIfEmpty = hideIfEmpty;
			return this as T;
		}

		public virtual HtmlString Build()
		{
			var doc = new XmlDocument();

			var element = doc.CreateElement(ContainerTag);


			if (ListOfAttributes != null)
				foreach (var attribute in ListOfAttributes)
				{
					var existingNode = element.Attributes.GetNamedItem(attribute.Item1, string.Empty);
					if (existingNode != null)
					{
						existingNode.Value += " " + attribute.Item2;
					}
					else
					{
						var attr = doc.CreateAttribute(attribute.Item1);
						attr.Value = attribute.Item2;
						element.Attributes.Append(attr);
					}
				}

			//append children
			if (ListOfChilds != null && ListOfChilds.Count > 0)
			{
				var childOutputs = new List<string>();
				foreach (var child in ListOfChilds)
					if (child != null)
					{
						var childOutput = child().ToHtmlString();
						childOutputs.Add(childOutput);
					}
				element.InnerXml = string.Join("", childOutputs.ToArray());
			}
			else
				if (ContentExtractor != null)
				{
					var retrievedContent = ContentExtractor().ToHtmlString();
					if (!string.IsNullOrWhiteSpace(retrievedContent))
						element.InnerXml = retrievedContent;
				}

			if (IsHideIfEmpty && !element.HasAttributes && !element.HasChildNodes)
				return new HtmlString(string.Empty);


			doc.AppendChild(element);

			if (XmlSettings != null)
				SetEmptyTagsCollapsed(doc.DocumentElement, XmlSettings.CollapseEmptyTags);

			//#region Option for render

			//var sb = new StringBuilder();

			//var xmlset = new XmlWriterSettings();
			//xmlset.Indent = false;
			//xmlset.NewLineChars = string.Empty;
			//xmlset.NewLineOnAttributes = false;
			//xmlset.OmitXmlDeclaration = true;
			//xmlset.CloseOutput = true;

			//var xmlw = XmlWriter.Create(sb, xmlset);
			//doc.WriteTo(xmlw);
			//var s = sb.ToString();

			//#endregion

			//if (!isRootExists)
			//	return doc.ChildNodes[0].InnerXml;

			return new HtmlString(doc.InnerXml);
		}

		private void SetEmptyTagsCollapsed(XmlElement node, bool collapseIfEmpty)
		{
			if ((!node.IsEmpty && node.ChildNodes.Count == 0) || !collapseIfEmpty)
				node.IsEmpty = collapseIfEmpty;

			// avoid overwriting settings for child elements
			//foreach (XmlNode child in node.ChildNodes)
			//    if (child.NodeType == XmlNodeType.Element)
			//    {
			//        SetEmptyTagsCollapsed((XmlElement)child, collapseIfEmpty);
			//    }
		}
	}
}
