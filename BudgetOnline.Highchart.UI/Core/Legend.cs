using System;
using BudgetOnline.Highchart.Core.Appearance;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BudgetOnline.Highchart.Core
{

    [Serializable]
    public class Legend
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public Layout? layout { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Align? align { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VerticalAlign? verticalAlign { get; set; }

        public int? x { get; set; }
        public int? y { get; set; }

        public string backgroundColor { get; set; }
        public string borderColor { get; set; }

        public int? borderRadius { get; set; }
        public int? borderWidth { get; set; }

        public bool? enabled { get; set; }
        public bool? shadow { get; set; }

        public ItemStyle itemStyle { get; set; }
        public ItemStyle itemHoverStyle { get; set; }
        public ItemStyle itemHiddenStyle { get; set; }

        public Legend()
        {
            enabled = true;
        }

        public override string ToString()
        {
            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return string.Format("legend: {0},", ignored);
        }

    }

}
