using System;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core.Appearance
{
    [Serializable]
    public class Appearance
    {
                
        public string renderTo { get; set; }
        public string defaultSeriesType { get; set; }

        public bool? alignTicks  { get; set; }
        public Background backgroundColor { get; set; }

        public string borderColor { get; set; }
        public int? borderWidth { get; set; }
        public int? borderRadius { get; set; }
        public string className { get; set; }
        public bool? ignoreHiddenSeries { get; set; }
        public bool? inverted { get; set; }
        public int[] margin { get; set; }
        public int? marginTop { get; set; }
        public int? marginRight { get; set; }
        public int? marginBottom { get; set; }
        public int? marginLeft { get; set; }
        public bool? shadow  { get; set; }
        public bool? showAxes  { get; set; }

        public string plotBackgroundImage  { get; set; }
        public Background plotBackgroundColor { get; set; }
        public string plotBorderColor  { get; set; }
        public int? plotBorderWidth  { get; set; }
        public bool? plotShadow  { get; set; }

        public override string ToString()
        {

            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });           
            return string.Format("chart: {0},", ignored);

        }

    }
}
