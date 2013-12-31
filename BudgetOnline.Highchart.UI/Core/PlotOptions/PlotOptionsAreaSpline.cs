using System;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core.PlotOptions
{
    
    [Serializable]
    public class PlotOptionsAreaSpline : PlotOptionsSeries
    {

        public string fillColor { get; set; }
        public double? fillOpacity  { get; set; }
        public string lineColor  { get; set; }
        public int? threshold  { get; set; }
        public int? lineWidth  { get; set; }
        public int? pointStart  { get; set; }
        public int? pointInterval  { get; set; }
        public bool? shadow  { get; set; }
             
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
