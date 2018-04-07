using Android.Graphics;
using Mobile.Controls;
using Mobile.Droid.ControlRenderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedBox), typeof(RoundedBoxRenderer))]
namespace Mobile.Droid.ControlRenderers
{
    public class RoundedBoxRenderer : BoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            SetWillNotDraw(false);

            Invalidate();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName)
            {
                case nameof(RoundedBox.CornerRadius):
                case nameof(RoundedBox.WidthRequest):
                case nameof(RoundedBox.HeightRequest):
                case nameof(RoundedBox.BackgroundColor):
                case nameof(RoundedBox.BorderColor):
				case nameof(RoundedBox.BorderWidth):
                    Invalidate();
                    break;
                default:
                    break;
            }

        }

        public override void Draw(Canvas canvas)
        {
            RoundedBox roundedBox;
            Rect rect;
            Paint border;
            Paint background;
            float borderWidth;
            float cornerRadius;
            int widthRequest;
            int heightRequest;
            int pointAdjustment;

            roundedBox = Element as RoundedBox;
            borderWidth = (int)Context.ToPixels(roundedBox.BorderWidth);
            widthRequest = (int)Context.ToPixels(roundedBox.Bounds.Width);
            heightRequest = (int)Context.ToPixels(roundedBox.Bounds.Height);
            cornerRadius = Context.ToPixels(roundedBox.CornerRadius);
            pointAdjustment = (int)borderWidth / 2;

            border = new Paint()
            {
                Color = roundedBox.BorderColor.ToAndroid(),
                StrokeWidth = borderWidth,
                AntiAlias = true
            };
            border.SetStyle(Paint.Style.Stroke);

            background = new Paint()
            {
                Color = roundedBox.BackgroundColor.ToAndroid(),
                AntiAlias = true
            };

            rect = new Rect();
            GetDrawingRect(rect);
            rect.Right = widthRequest - pointAdjustment;
            rect.Bottom = heightRequest - pointAdjustment;
            rect.Left = pointAdjustment;
            rect.Top = pointAdjustment;

            canvas.DrawRoundRect(new RectF(rect), cornerRadius, cornerRadius, background);
            canvas.DrawRoundRect(new RectF(rect), cornerRadius, cornerRadius, border);

        }
    }
}