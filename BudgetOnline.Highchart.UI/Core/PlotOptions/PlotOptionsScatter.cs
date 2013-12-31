using System;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core.PlotOptions
{
    [Serializable]
    public class PlotOptionsScatter : PlotOptionsSeries
    {

        public int? lineWidth  { get; set; }
        public int? pointStart  { get; set; }
        public int? pointInterval  { get; set; }
        
        public override string ToString()
        {
            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

            if (!string.IsNullOrEmpty(ignored))
            {
                return string.Format("plotOptions: {{ series: {0} }},", ignored);
            }
            else
            {
                return string.Empty;
            }
        }

    }

}
