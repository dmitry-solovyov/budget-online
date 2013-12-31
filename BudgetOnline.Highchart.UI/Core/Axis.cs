using System;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public abstract class Axis
    {
                
        public Title title { get; set; }
        public string align { get; set; }
        public string[] categories { get; set; }
        public string alternateGridColor { get; set; }
        public string gridLineColor { get; set; }
        public bool? endOnTick { get; set; }
        public int? gridLineWidth { get; set; }

        public string lineColor { get; set; }
        public int? lineWidth { get; set; }
        public bool? opposite { get; set; }
        public bool? reversed { get; set; }

        public Axis()
        {
            title = new Title(string.Empty);
        }

    }

}
