using System;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public class XAxis : Axis
    {
        public XAxis()
        {

        }

        public override string ToString()
        {
            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return string.Format("xAxis: {0},", ignored);
        }
    }
}
