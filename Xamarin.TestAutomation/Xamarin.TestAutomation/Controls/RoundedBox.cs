using Xamarin.Forms;

namespace Mobile.Controls
{
    public class RoundedBox : BoxView
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(double), typeof(RoundedBox), 0.0);
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(double), typeof(RoundedBox), 1.0);
        //TODO: Change the color to midgray
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(RoundedBox), Color.Gray);

        public double CornerRadius
        {
            get { return (double)base.GetValue(CornerRadiusProperty); }
            set { base.SetValue(CornerRadiusProperty, value); }
        }

        public double BorderWidth
        {
            get { return (double)base.GetValue(BorderWidthProperty); }
            set { base.SetValue(BorderWidthProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)base.GetValue(BorderColorProperty); }
            set { base.SetValue(BorderColorProperty, value); }
        }

        public RoundedBox()
        {
			BackgroundColor = Color.White;
		}
	}
}
