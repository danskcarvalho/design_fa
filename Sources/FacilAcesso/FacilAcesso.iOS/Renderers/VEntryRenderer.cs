using Xamarin.Forms;
using FacilAcesso;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Drawing;
using CoreGraphics;
using System;

[assembly: ExportRenderer(typeof(VEntry), typeof(FacilAcesso.iOS.VEntryRenderer))]
namespace FacilAcesso.iOS
{
    public class VEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            var view = Element as VEntry;

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
                Control.BorderStyle = UITextBorderStyle.None;

                //Control.Layer.CornerRadius = 5;
                //Control.Layer.BorderWidth = 1;
                //Control.Layer.BorderColor = Color.FromHex("#D2E0E8").ToCGColor();
                //Control.BackgroundColor = Color.FromHex("#90FFFFFF").ToUIColor();
                //ResizeHeight();
            }
        }

        private void ResizeHeight()
        {
            if (Element == null)
                return;

            if (Element.HeightRequest >= 0) return;

            var height = Math.Max(0, //Eu não sei se atribuir 0 é o correto, porém...
                new UITextField { Font = Control.Font }.IntrinsicContentSize.Height) * 2;

            Control.Frame = new CGRect(0.0f, 0.0f, (nfloat)Element.Width, (nfloat)height);

            Element.HeightRequest = height;
        }
    }
}