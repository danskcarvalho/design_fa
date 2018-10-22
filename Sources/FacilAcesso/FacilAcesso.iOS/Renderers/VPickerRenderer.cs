using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using FacilAcesso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VPicker), typeof(FacilAcesso.iOS.VPickerRenderer))]
namespace FacilAcesso.iOS
{
    public class VPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);
            var view = Element as VPicker;

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
                Control.BorderStyle = UITextBorderStyle.None;
                //Control.Layer.CornerRadius = 5;
                //Control.Layer.BorderWidth = 1;
                //Control.Layer.BorderColor = Color.FromHex("#D2E0E8").ToCGColor();
                //Control.BackgroundColor = Color.FromHex("#90FFFFFF").ToUIColor();
                //Control.Font = Control.Font.WithSize(14);
                //ResizeHeight();

                if (Element.SelectedIndex == -1)
                {
                    Control.Text = view.Placeholder;
                    Control.TextColor = view.PlaceholderColor.ToUIColor();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (VPicker)Element;

            if (view == null)
                return;
            ResizeHeight();

            if (e.PropertyName == "SelectedIndex")
            {

                if (Control != null)
                {
                    if (Element.SelectedIndex == -1)
                    {
                        Control.Text = view.Placeholder;
                        Control.TextColor = view.PlaceholderColor.ToUIColor();
                    }
                    else
                    {
                        Control.TextColor = view.TextColor.ToUIColor();
                    }
                }
            }
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
    }
}