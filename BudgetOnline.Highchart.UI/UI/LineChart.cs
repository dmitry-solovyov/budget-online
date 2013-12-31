using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BudgetOnline.Highchart.Core;
using BudgetOnline.Highchart.Core.Appearance;
using BudgetOnline.Highchart.Core.PlotOptions;

namespace Highchart.UI
{
    public class LineChart: Chart, IChart
    {

        protected override void OnLoad(EventArgs e)
        {
                      
            Render();
            base.OnLoad(e);

        }

        public PlotOptionsLine PlotOptions
        {
            get
            {
                object o = ViewState["PlotOptionsLine"];
                if (o == null)
                    return new PlotOptionsLine();
                return (PlotOptionsLine)o;
            }
            set { ViewState["PlotOptionsLine"] = value; }
        }

        public void Render()
        {

            script =
@"var chart[@Id];
$(document).ready(function() {
    chart[@Id] = new Highcharts.Chart({    
    [@Theme]
    [@Colors]
    credits: { enabled: [@ShowCredits] },
    [@PlotOptions]
    [@Title]
    [@Subtitle]
    [@Legend]
    [@XAxis]
    [@YAxis]
    [@ToolTip]
    [@Series]
	});
});";

            script = script.Replace("[@PlotOptions]", PlotOptions.ToString());
            script = script.Replace("[@Id]", this.ClientID);

            var tm = new ThemeManager(Theme);
            Appearance = tm.Appearance;

            // sobreescrece as propriedades obrigatórias
            Appearance.renderTo = this.ClientID;
            Appearance.defaultSeriesType = RenderType.line.ToString();

            Title.style = tm.TitleStyle;
            SubTitle.style = tm.SubTitleStyle;
            XAxis.title.style = tm.SubTitleStyle;
            YAxis.title.style = tm.SubTitleStyle;

            if (tm.LegendStyle.hiddenStyle != null)
                Legend.itemHiddenStyle = tm.LegendStyle.hiddenStyle;

            if(tm.LegendStyle.hoverStyle != null)
                Legend.itemHoverStyle = tm.LegendStyle.hoverStyle;

            if(tm.LegendStyle.style != null)
                Legend.itemStyle = tm.LegendStyle.style;

            Colors = tm.ColorSet;

            script = script.Replace("[@Colors]", Colors.ToString());
            script = script.Replace("[@Theme]", Appearance.ToString());
            script = script.Replace("[@Legend]", Legend.ToString());
            script = script.Replace("[@ShowCredits]", ShowCredits.ToString().ToLower());
            script = script.Replace("[@Title]", Title.ToString());
            script = script.Replace("[@Subtitle]", SubTitle.ToString());
            script = script.Replace("[@ToolTip]", Tooltip.ToString());
            script = script.Replace("[@YAxis]", YAxis.ToString());
            script = script.Replace("[@XAxis]", XAxis.ToString());
            script = script.Replace("[@Series]", Series.ToString());

            Page.ClientScript.RegisterStartupScript(GetType(), "chart_" + ID, script, true);

        }

    }
}