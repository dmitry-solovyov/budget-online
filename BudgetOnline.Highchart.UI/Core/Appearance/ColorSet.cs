using System;
using System.Linq;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core.Appearance
{
    [Serializable]
    public class ColorSet
    {
        
        public string[] colors { get; set; }

        public override string ToString()
        {
            if (colors != null && colors.Count() > 0)
            {
                string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return ignored.Replace("{", string.Empty).Replace("}", string.Empty) + ",";
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
