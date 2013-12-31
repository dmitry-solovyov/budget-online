using System;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core.PlotOptions
{
    [Serializable]
    public class PlotOptionsColumn : PlotOptionsSeries
    {
        public string pointer { get; set; }
        public int? pointStart  { get; set; }
        public int? pointInterval  { get; set; }        
        public bool? shadow  { get; set; }
        
        public string borderColor  { get; set; }
        public int? borderRadius  { get; set; }
        public int? borderWidth  { get; set; }
        public bool? colorByPoint  { get; set; }
        public int? groupPadding  { get; set; }
        public int? minPointLength  { get; set; }
        public int? pointPadding  { get; set; }
        public int? pointWidth  { get; set; }

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
