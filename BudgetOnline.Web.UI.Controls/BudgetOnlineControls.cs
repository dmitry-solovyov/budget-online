using BudgetOnline.UI.Controls;
using BudgetOnline.UI.Controls.Buttons;

namespace BudgetOnline.Web.UI.Controls
{
	public class BudgetOnlineWebControls
	{
		public ButtonBuilder ButtonLarge()
		{
			return new ButtonBuilder().Css("btn btn-lg");
		}

		public ButtonBuilder ButtonNormal()
		{
			return new ButtonBuilder().Css("btn");
		}

		public ButtonBuilder ButtonNormal(ButtonCaptionTypes captionType)
		{
			var builder = new SubmitButtonBuilder().Css("btn btn-default");
			SetButtonCaption(captionType, builder);

			return builder;
		}

		public ButtonBuilder ButtonSmall()
		{
			return new ButtonBuilder().Css("btn btn-sm");
		}

		public ButtonBuilder ButtonMini()
		{
			return new ButtonBuilder().Css("btn btn-xs");
		}

		public ButtonBuilder SubmitLargeButton()
		{
			return new SubmitButtonBuilder().Css("btn btn-lg btn-primary");
		}

        public ButtonBuilder PrimaryButton()
		{
			return new SubmitButtonBuilder().Css("btn btn-primary");
		}

		public ButtonBuilder SubmitNormalButton(ButtonCaptionTypes captionType)
		{
			var builder = new SubmitButtonBuilder().Css("btn btn-primary");
			SetButtonCaption(captionType, builder);

			return builder;
		}


		public ImageBuilder Image()
		{
			return new ImageBuilder();
		}

		public PanelBuilder Panel()
		{
			return new PanelBuilder();
		}

		public AutoCompleteTextBoxBuilder AutoComplete()
		{
			return new AutoCompleteTextBoxBuilder();
		}

		public FormBuilder Form(FormTypes type = FormTypes.Undefined)
		{
			var builder = new FormBuilder();
			switch (type)
			{
				case FormTypes.Inline:
					builder.Css("form-inline");
					break;

				case FormTypes.Horizontal:
					builder.Css("form-horizontal");
					break;

				default:
					break;
			}

			return builder;
		}

		public MenuBuilder Menu(MenuTypes menuType = MenuTypes.Menu)
		{
			return new MenuBuilder(menuType);
		}

		public ListBuilder<T> List<T>()
			where T : class
		{
			return new ListBuilder<T>();
		}

		public TableBuilder<T> Table<T>()
			where T : class
		{
			return new TableBuilder<T>();
		}

		public TableBoundColumnBuilder<T> TableColumn<T>()
			where T : class
		{
			return new TableBoundColumnBuilder<T>();
		}

		public CommandsListBuilder CommandsList()
		{
			return new CommandsListBuilder();
		}


		public enum FormTypes
		{
			Undefined,
			Inline,
			Horizontal
		}

		public enum ButtonCaptionTypes
		{
			Save,
			Cancel,
			Ok,
			Submit,
			Proceed
		}

		public static void SetButtonCaption(ButtonCaptionTypes captionType, ButtonBuilder builder)
		{
			switch (captionType)
			{
				case ButtonCaptionTypes.Ok:
					builder.Caption("Ок");
					break;
				case ButtonCaptionTypes.Save:
					builder.Caption("Сохранить");
					break;
				case ButtonCaptionTypes.Cancel:
					builder.Caption("Отмена");
					break;
				case ButtonCaptionTypes.Proceed:
					builder.Caption("Продолжить");
					break;
				case ButtonCaptionTypes.Submit:
					builder.Caption("Подтвердить");
					break;
				default:
					break;
			}
		}

		public ProgressIndicatorBuilder ProgressIndicator()
		{
			return new ProgressIndicatorBuilder();
		}

	}
}
