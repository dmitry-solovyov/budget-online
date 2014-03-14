using System.Collections.Generic;
using System.Linq;

namespace BudgetOnline.UI.Models.SelectItems
{
    public class SelectItemsModel
    {
        public IEnumerable<SelectItemModel> Items { get; set; }

        public string Name { get; set; }
        public string CssClass { get; set; }
        public bool IsMultiselect { get; set; }

        public SelectItemsModel()
        {
            Items = Enumerable.Empty<SelectItemModel>();
        }

        public SelectItemsModel(IEnumerable<SelectItemModel> items, bool addEmpty = false)
        {
            Items = items;

            if (addEmpty)
                Items = new[] { new SelectItemModel { Text = "", Value = "0" } }.Concat(Items);
        }

        public void MergeSelected(IEnumerable<SelectItemModel> selected, bool sort = false)
        {
            var newItems = selected.Where(o => !Items.Any(x => x.Value.Equals(o.Value))).ToList();

            var existingItems = Items.Select(o => new SelectItemModel
                                        {
                                            Icon = o.Icon,
                                            Text = o.Text,
                                            Value = o.Value,
                                            Tooltip = o.Tooltip,
                                            Selected = selected.Any(x => x.Value.Equals(o.Value))
                                        }).ToList();

            newItems.AddRange(existingItems);
            if (sort)
                newItems = newItems.OrderBy(o => o.Text).ToList();


            Items = newItems;
        }

        public void SetSelected(string value)
        {
            Items =
                Items.Select(
                    o =>
                    new SelectItemModel
                        {
                            Icon = o.Icon,
                            Text = o.Text,
                            Value = o.Value,
                            Selected = o.Value.Equals(value),
                            Tooltip = o.Tooltip
                        });
        }
    }
}