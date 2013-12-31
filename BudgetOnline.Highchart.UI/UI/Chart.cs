using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.ObjectModel;
using BudgetOnline.Highchart.Core;
using BudgetOnline.Highchart.Core.Appearance;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;

namespace Highchart.UI
{

    [DefaultProperty("Title")]
    [ToolboxData("<{0}:Highchart runat=server></{0}:Highchart>")]
    public class Chart : GenericChart
    {
        
        internal string script;

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);

        }

        protected override void OnPreRender(EventArgs e)
        {
            
            string[] scripts = { "Highchart.HighchartAPI.highcharts.js", "Highchart.HighchartAPI.exporting.js" };

            foreach (string js in scripts)
            {

                string url = Page.ClientScript.GetWebResourceUrl(GetType(), js);

                if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), js))
                {
                    Page.ClientScript.RegisterClientScriptInclude(GetType(), js, url);
                }

            }

            base.OnPreRender(e);

        }

        public Chart()
        {
            Appearance = new Appearance { renderTo = this.ClientID };
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void PerformDataBinding(IEnumerable dataSource)
        {
            
            base.PerformDataBinding(dataSource);

            if (dataSource != null)
            {
            
                foreach (object obj in dataSource)
                {
                    var item = obj as Serie;
                    Series.Add(item);
                }

            }

        }
                
    }
}
