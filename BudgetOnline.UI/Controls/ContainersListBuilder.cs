using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetOnline.UI.Controls
{
	public class ContainersListBuilder : IBuilder
	{
		protected readonly List<ContainerBuilder> Builders = new List<ContainerBuilder>();

		public ContainersListBuilder Clear()
		{
			Builders.Clear();
			return this;
		}

		public ContainersListBuilder AddBuilder(ContainerBuilder containerBuilder, int index = -1)
		{
			if (index < 0 && index >= Builders.Count)
				Builders.Add(containerBuilder);
			else
				Builders.Insert(index, containerBuilder);

			return this;
		}

		public HtmlString Build()
		{
			return 
				new HtmlString(
					string.Join(
						"\r\n",
						from builder in Builders where builder != null select builder.Build().ToHtmlString().ToArray()
					)
				);
		}
	}
}
