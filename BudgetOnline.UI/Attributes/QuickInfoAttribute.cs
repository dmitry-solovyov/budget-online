using System;

namespace BudgetOnline.UI.Attributes
{
    public class QuickInfoAttribute : Attribute
    {
        public QuickInfoAttribute()
        {
        }

        public string Name { get; set; }
        public string HelpUrl { get; set; }
    }
}
