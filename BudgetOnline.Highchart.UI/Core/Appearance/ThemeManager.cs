using System.Collections.ObjectModel;

namespace BudgetOnline.Highchart.Core.Appearance
{
    public class ThemeManager
    {

        private ThemeName SelectedTheme { get; set; }

        public ThemeManager(ThemeName useTheme)
        {
            SelectedTheme = useTheme;
        }
        
        public Appearance Appearance
        {
            get
            {
                switch (SelectedTheme)
                {
                    case ThemeName.none:
                        return new Appearance();
                    case ThemeName.gray:
                        return new Appearance { backgroundColor = new Background { linearGradient = new[] { 0, 0, 0, 400 }, stops = new Collection<object[]> { new object[] { 0, "rgb(96, 96, 96)" }, new object[] { 1, "rgb(16, 16, 16)" } } } };
                    case ThemeName.pink:
                        return new Appearance { backgroundColor = new Background { linearGradient = new[] { 0, 0, 250, 500 }, stops = new Collection<object[]> { new object[] { 0, "rgba(255, 255, 255, .75)" }, new object[] { 1, "rgba(252, 221, 217, .75)" } } } };
                    case ThemeName.darkblue:
                        return new Appearance { backgroundColor = new Background { linearGradient = new[] { 0, 0, 250, 500 }, stops = new Collection<object[]> { new object[] { 0, "rgb(48, 48, 96)" }, new object[] { 1, "rgb(0, 0, 0)" } } }, plotBorderColor = "#CCCCCC", borderColor = "#000000" };
                    default:
                        return new Appearance();
                }
            }
        }

        public CSSObject TitleStyle
        {
            get
            {
                switch (SelectedTheme)
                {
                    case ThemeName.none:
                        return new CSSObject();
                    case ThemeName.gray:
                    case ThemeName.darkblue:
                        return new CSSObject { color = "#FFFFFF", font = "bold 16px 'Trebuchet MS', Verdana, sans-serif" };
                    case ThemeName.pink:
                        return new CSSObject { color = "#7F3753", font = "16px bold Lucida Grande, Lucida Sans Unicode, Verdana, Arial, Helvetica, sans-serif" };                                           
                    default:
                        return new CSSObject();
                }
            }
        }

        public ColorSet ColorSet 
        {
            get
            {
                switch (SelectedTheme)
                {
                    case ThemeName.none:
                        return new ColorSet();
                    case ThemeName.pink:
                        return new ColorSet { colors = new[] {"#6C4B6A", "#529CA0", "#A57972", "#5D7C9B", "#72727F", "#DFA09B", "#7C3A49", "808AA9" } };
                    case ThemeName.gray:
                    case ThemeName.darkblue:
                        return new ColorSet { colors = new[] {"#DDDF0D", "#55BF3B", "#DF5353", "#7798BF", "#aaeeee", "#ff0066", "#eeaaee", "#55BF3B", "#DF5353", "#7798BF", "#aaeeee"} };
                    default:
                        return new ColorSet();
                }
            }
        }

        public CSSObject SubTitleStyle
        {
            get
            {
                switch (SelectedTheme)
                {
                    case ThemeName.none:
                        return new CSSObject();
                    case ThemeName.gray:
                    case ThemeName.darkblue:
                        return new CSSObject { color = "#DDDDDD", font = "bold 12px 'Trebuchet MS', Verdana, sans-serif" };
                    case ThemeName.pink:
                        return new CSSObject { color = "#b7748c", font = "12px Lucida Grande, Lucida Sans Unicode, Verdana, Arial, Helvetica, sans-serif" };                                           
                    default:
                        return new CSSObject();
                }
            }
        }

        public LegendStyle LegendStyle 
        { 
            get 
            {
                switch (SelectedTheme)
                {
                    case ThemeName.none:
                        return new LegendStyle();
                    case ThemeName.gray:
                        return new LegendStyle { hiddenStyle = new ItemStyle { color = "#333333" }, hoverStyle = new ItemStyle { color = "#FFFFFF" }, style = new ItemStyle { color = "#CCCCCC" } };
                    case ThemeName.darkblue:
                        return new LegendStyle { hiddenStyle = new ItemStyle { color = "#A0A0A0" } };
                    case ThemeName.pink:
                        return new LegendStyle { hiddenStyle = new ItemStyle { color = "#333333" }, hoverStyle = new ItemStyle { color = "#3E576F" }, style = new ItemStyle { color = "#7F3753" } };                        
                    default:
                        return new LegendStyle();
                }
            } 
        }
        
    }

}
