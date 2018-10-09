using Xamarin.Forms;
using FacilAcesso;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System;
using UIKit;
using CoreGraphics;
using Foundation;

[assembly: ExportRenderer(typeof(VDatePicker), typeof(FacilAcesso.iOS.VDatePickerRenderer))]
namespace FacilAcesso.iOS
{
    public class VDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            var view = Element as VDatePicker;
            
            if (Control != null)
            {
                Control.Layer.CornerRadius = 5;
                Control.Layer.BorderWidth = 1;
                Control.Layer.BorderColor = Color.FromHex("#D2E0E8").ToCGColor();
                Control.BackgroundColor = Color.FromHex("#90FFFFFF").ToUIColor();
                Control.Font = Control.Font.WithSize(14);

                if (view != null)
                {
                    SetNullableText(view);
                    SetPlaceholderTextColor(view);
                }
                ResizeHeight();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (VDatePicker)Element;

            if (view == null)
                return;

            if (e.PropertyName == VDatePicker.NullableDateProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == VDatePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

            ResizeHeight();
        }

        private void SetNullableText(VDatePicker view)
        {
            if (view.NullableDate == null)
                Control.Text = string.Empty;
        }

        /// <summary>
        /// Resizes the height.
        /// </summary>
        private void ResizeHeight()
        {
            if (Element == null)
                return;
            
            if (Element.HeightRequest >= 0) return;

            var height = Math.Max(Bounds.Height,
                new UITextField { Font = Control.Font }.IntrinsicContentSize.Height) * 2;

            Control.Frame = new CGRect(0.0f, 0.0f, (nfloat)Element.Width, (nfloat)height);

            Element.HeightRequest = height;
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(VDatePicker view)
        {
            if (!string.IsNullOrEmpty(view.Placeholder))
            {
                var foregroundUIColor = view.PlaceholderTextColor.ToUIColor(CheckColorExtensions.SeventyPercentGrey);
                var backgroundUIColor = view.BackgroundColor.ToUIColor();
                var targetFont = Control.Font;
                Control.AttributedPlaceholder = new NSAttributedString(view.Placeholder, targetFont, foregroundUIColor, backgroundUIColor);
            }
        }
    }

    public static class CheckColorExtensions
    {
        internal static readonly UIColor SeventyPercentGrey = new UIColor(0.7f, 0.7f, 0.7f, 1);
        public static bool IsDefault(this Color color) => Color.Default == color;
    }
}