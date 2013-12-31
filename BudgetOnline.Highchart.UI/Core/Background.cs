using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public class Background
    {

        public int[] linearGradient { get; set; }        
        public Collection<object[]> stops { get; set; }

        public override string ToString()
        {
            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return string.Format("{0},", ignored);           
        }
        
    }
}
