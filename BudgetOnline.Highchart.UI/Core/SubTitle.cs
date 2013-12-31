using System;
using System.Web.Script.Serialization;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]
    public class SubTitle : Title
    {
        public SubTitle(string subTitleText) : base(subTitleText)
        {

        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(text))
            {
                var jss = new JavaScriptSerializer();
                return string.Format("subtitle: {0},", jss.Serialize(this));
            }
            else
                return string.Empty;
        }
    }
}
