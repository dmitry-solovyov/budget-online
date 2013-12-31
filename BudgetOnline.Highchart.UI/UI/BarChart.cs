using System;
using BudgetOnline.Highchart.Core;
using BudgetOnline.Highchart.Core.PlotOptions;
using Highchart.UI;

namespace BudgetOnline.Highchart.UI
{
    public class BarChart : Chart, IChart
    {

        protected override void OnLoad(EventArgs e)
        {
            RenderType = RenderType.bar;
            Render();
            base.OnLoad(e);
        }

        public PlotOptionsBar PlotOptions
        {
            get
            {
                object o = ViewState["PlotOptionsBar"];
                if (o == null)
                    return new PlotOptionsBar();
                return (PlotOptionsBar)o;
            }
            set { ViewState["PlotOptionsBar"] = value; }
        }

        public void Render()
        {

            script =
@"var chart[@Id];
$(document).ready(function() {
    chart[@Id] = new Highcharts.Chart({
    chart: { renderTo: '[@Id]', defaultSeriesType: '[@RenderType]' },
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
            script = script.Replace("[@RenderType]", RenderType.ToString());
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
