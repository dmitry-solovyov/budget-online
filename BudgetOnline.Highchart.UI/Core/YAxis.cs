using System;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public class YAxis : Axis
    {
        public YAxis()
        {

        }

        public override string ToString()
        {
            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return string.Format("yAxis: {0},", ignored);
        }
      
    }
}
