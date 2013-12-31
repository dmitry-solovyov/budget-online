using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BudgetOnline.Highchart.Core;
using BudgetOnline.Highchart.Core.Appearance;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Highchart.UI
{
    public abstract class GenericChart : CompositeDataBoundControl
    {

        public virtual ColorSet Colors
        {
            get
            {
                object o = ViewState["Colors"];
                if (o == null)
                    return new ColorSet();
                return (ColorSet)o;
            }
            set { ViewState["Colors"] = value; }
        }

        public virtual ThemeName Theme
        {
            get
            {
                object o = ViewState["Theme"];
                if (o == null)
                    return new ThemeName();
                return (ThemeName)o;
            }
            set { ViewState["Theme"] = value; }
        }

        public virtual Appearance Appearance
        {
            get
            {
                object o = ViewState["Appearance"];
                if (o == null)
                    return new Appearance();
                return (Appearance)o;
            }
            set { ViewState["Appearance"] = value; }
        }

        public virtual Legend Legend
        {
            get
            {
                object o = ViewState["Legend"];
                if (o == null)
                {
                    var legend = new Legend();
                    ViewState["Legend"] = legend;
                    return legend;
                }
                return (Legend)o;
            }
            set { ViewState["Legend"] = value; }
        }

        public virtual ToolTip Tooltip
        {
            get
            {
                object o = ViewState["ToolTip"];
                if (o == null)
                    return new ToolTip("'<b>'+ this.series.name +'</b><br/>'+ this.x +': '+ this.y");
                return (ToolTip)o;
            }
            set { ViewState["ToolTip"] = value; }
        }

        public virtual YAxis YAxis
        {
            get
            {
                object o = ViewState["YAxis"];
                if (o == null)
                    return new YAxis();
                return (YAxis)o;
            }
            set { ViewState["YAxis"] = value; }
        }

        public virtual XAxis XAxis
        {
            get
            {
                object o = ViewState["XAxis"];
                if (o == null)
                    return new XAxis();
                return (XAxis)o;
            }
            set { ViewState["XAxis"] = value; }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public virtual bool ShowCredits
        {
            get
            {
                object o = ViewState["ShowCredits"];
                if (o == null)
                    return false;
                return (bool)o;
            }

            set
            {
                ViewState["ShowCredits"] = value;
            }
        }

        [DefaultValue("")]
        [Localizable(true)]
        public virtual Title Title
        {
            get
            {
                object o = ViewState["Title"];
                if (o == null)
                    return new Title(string.Empty);
                return (Title)o;
            }
            set { ViewState["Title"] = value; }
        }

        [DefaultValue("")]
        [Localizable(true)]
        public virtual SubTitle SubTitle
        {
            get
            {
                object o = ViewState["SubTitle"];
                if (o == null)
                    return new SubTitle(string.Empty);
                return (SubTitle)o;
            }
            set { ViewState["SubTitle"] = value; }
        }
        
        [DefaultValue(RenderType.column)]
        public virtual RenderType RenderType
        {
            get
            {
                object o = ViewState["RenderType"];
                if (o == null)
                    return RenderType.column;
                return (RenderType)o;
            }
            set { ViewState["RenderType"] = value; }
        }

        private SerieCollection _series;
        public virtual SerieCollection Series
        {
            get
            {
                if (_series == null)
                {
                    _series = new SerieCollection();
                    if (IsTrackingViewState)
                        _series.TrackViewState();
                }
                return _series;
            }
        }

        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            return Series.Count;
        }
    }
}
