using System;
using System.Web.Script.Serialization;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public class ToolTip
    {

        public ToolTip(string format)
        {
            formatter = format;
        }

        public string formatter { get; set; }

        public override string ToString()
        {
            var jss = new JavaScriptSerializer();
            return string.Format("tooltip: {{ formatter: function() {{ return {0}; }} }},", formatter);
        }

    }
}
