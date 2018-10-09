using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FacilAcesso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VSmallPicker), typeof(FacilAcesso.iOS.VSmallPickerRenderer))]
namespace FacilAcesso.iOS
{
    public class VSmallPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = Color.Transparent.ToUIColor();
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
                Control.Font = UIKit.UIFont.FromName(Control.Font.Name, ((nfloat)((Element as VSmallPicker)?.FontSize ?? 12.0)));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "FontSize")
            {
                if (Control != null)
                {
                    Control.Font = UIKit.UIFont.FromName(Control.Font.Name, ((nfloat)((Element as VSmallPicker)?.FontSize ?? 12.0)));
                }
            }
        }
    }
}
