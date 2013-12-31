using System;
using BudgetOnline.Highchart.Core.Appearance;
using Newtonsoft.Json;

namespace BudgetOnline.Highchart.Core
{
    [Serializable]    
    public class Title
    {

        public string text { get; set; }
        public CSSObject style { get; set; }

        public Title(string titleText)
        {
            text = titleText;            
        }

        public override string ToString()
        {
            string ignored = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return string.Format("title: {0},", ignored);            
        }

    }
}
