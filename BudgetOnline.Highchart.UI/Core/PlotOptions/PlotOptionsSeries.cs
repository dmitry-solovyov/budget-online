using System;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core.PlotOptions
{
    [Serializable]
    public abstract class PlotOptionsSeries
    {

        public bool? allowPointSelect { get; set; }
        public bool? animation  { get; set; }
        public string color { get; set; }
        public string cursor { get; set; }
        public bool? enableMouseTracking  { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Stackting? stacking  { get; set; }
        public bool? stickyTracking { get; set; }

    }
}
