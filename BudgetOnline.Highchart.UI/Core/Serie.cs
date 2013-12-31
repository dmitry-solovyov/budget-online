using System;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public class Serie
    {

        public string name;
        public string color { get; set; }
        public bool? showInLegend { get; set; }
        public bool? selected  { get; set; }
        public bool? visible { get; set; }

        public object[] data;
            
    }
}
