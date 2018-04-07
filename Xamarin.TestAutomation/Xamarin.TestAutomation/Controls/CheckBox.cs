using Mobile.EventArguments;
using Mobile.Utility;
using System;
using Xamarin.Forms;

namespace Mobile.Controls
{
	public class CheckBox : AbsoluteLayout
	{
		public static readonly ImageSource IconActionAccept;

		static CheckBox()
		{
			switch(Device.RuntimePlatform)
			{
				case Device.iOS:
					IconActionAccept = "check.png";
					break;
				case Device.Android:
					IconActionAccept = "ic_action_accept.png";
					break;
				case Device.WinPhone:
					IconActionAccept = "check.png";
					break;
				default:
					break;
			}
		}

		#region Binding Properties

		public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(CheckBox), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: CheckedPropertyBindingPropertyChanged);
		private static void CheckedPropertyBindingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((CheckBox)bindable).uxTickImage.IsVisible = (bool)newValue;
			((CheckBox)bindable).OnRaiseCheckStateChanged(new CheckStateChangedEventArgs((bool)newValue));
		}

		#endregion

		public Boolean Checked
		{
			get { return (Boolean)base.GetValue(CheckBox.CheckedProperty); }
			set
			{
				if((Boolean)base.GetValue(CheckBox.CheckedProperty) != value)
				{
					base.SetValue(CheckBox.CheckedProperty, value);
				}
			}
		}

		private WeakEvent<CheckStateChangedEventArgs>.Source _checkStateChangedEvent = new WeakEvent<CheckStateChangedEventArgs>.Source();
		public event EventHandler<CheckStateChangedEventArgs> CheckStateChanged
		{
			add { _checkStateChangedEvent.Add(value); }
			remove { _checkStateChangedEvent.Remove(value); }
		}

		private Image uxTickImage;
		private RoundedBox uxRoundedBox;
		public CheckBox()
		{

			uxRoundedBox = new RoundedBox
			{
				HeightRequest = 20,
				WidthRequest = 20,
				CornerRadius = 2,
				IsVisible = true,
				InputTransparent = true
			};

            uxTickImage = new Image()
            {
                BackgroundColor = Color.Transparent,
                Source = IconActionAccept,
                HeightRequest = 20,
                WidthRequest = 20,
                IsVisible = false,
                AutomationId = this.AutomationId + "Image"
			};

			HeightRequest = 20;
			WidthRequest = 20;
			Children.Add(uxRoundedBox, new Rectangle(0, 0, 20, 20), AbsoluteLayoutFlags.None);
			Children.Add(uxTickImage, new Rectangle(0, 0, 20, 20), AbsoluteLayoutFlags.None);

			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += new EventHandler(TapGestureRecognizer_Tapped);
			this.GestureRecognizers.Add(tapGestureRecognizer);

			Padding = 0;
		}

		private void TapGestureRecognizer_Tapped(Object sender, EventArgs e)
		{
			if(IsEnabled)
			{
				Checked = !Checked;
			}
		}

		protected virtual void OnRaiseCheckStateChanged(CheckStateChangedEventArgs e)
		{
			_checkStateChangedEvent.RaiseEvent(this, e);
		}

        protected bool GetChecked() => Checked;

        

    }
}
