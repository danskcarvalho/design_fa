using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UIKit;
using FacilAcesso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VTimePicker), typeof(FacilAcesso.iOS.VTimePickerRenderer))]
namespace FacilAcesso.iOS
{
    public class VTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);
            var view = Element as VTimePicker;

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

            var view = (VTimePicker)Element;

            if (view == null)
                return;

            if (e.PropertyName == VTimePicker.NullableTimeProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == VTimePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

            ResizeHeight();
        }

        private void SetNullableText(VTimePicker view)
        {
            if (view.NullableTime == null)
                Control.Text = string.Empty;
        }

        /// <summary>
        /// Resizes the height.
        /// </summary>
        private void ResizeHeight()
        {
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
        private void SetPlaceholderTextColor(VTimePicker view)
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
}
