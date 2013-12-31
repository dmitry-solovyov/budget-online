using System;
using BudgetOnline.Highchart.Core;
using BudgetOnline.Highchart.Core.PlotOptions;

namespace Highchart.UI
{
    public class AreaSplineChart : Chart, IChart
    {

        protected override void OnLoad(EventArgs e)
        {
            RenderType = RenderType.areaspline;
            Render();
            base.OnLoad(e);
        }

        public PlotOptionsAreaSpline PlotOptions
        {
            get
            {
                object o = ViewState["PlotOptionsAreaSpline"];
                if (o == null)
                    return new PlotOptionsAreaSpline();
                return (PlotOptionsAreaSpline)o;
            }
            set { ViewState["PlotOptionsAreaSpline"] = value; }
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
