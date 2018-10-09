using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(FacilAcesso.VImageCircle), typeof(FacilAcesso.iOS.VImageCircleRenderer))]
namespace FacilAcesso.iOS
{
    public class VImageCircleRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            CreateCircle();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
                var min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (nfloat)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.BackgroundColor = UIColor.White;
                Control.ClipsToBounds = true;

                var externalBorder = new CALayer();
                externalBorder.CornerRadius = Control.Layer.CornerRadius;
                externalBorder.Frame = new CGRect(-.5, -.5, min + 1, min + 1);
                externalBorder.BorderWidth = 0;

                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }
    }
}
