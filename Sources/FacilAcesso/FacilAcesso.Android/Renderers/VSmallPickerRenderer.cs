using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FacilAcesso;
using System.ComponentModel;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(VSmallPicker), typeof(FacilAcesso.Droid.VSmallPickerRenderer))]
namespace FacilAcesso.Droid
{
    public class VSmallPickerRenderer : PickerRenderer
    {
        public VSmallPickerRenderer(Context ctx) : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                //Control.SetBackgroundResource(Resource.Drawable.VLinkBackground);
                //Control.SetTextSize(Android.Util.ComplexUnitType.Dip, ((float)((Element as VSmallPicker)?.FontSize ?? 12.0)));
                //Control.SetPadding(10, 0, 0, 0);
                Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "FontSize")
            {
                if (Control != null)
                {
                    Control.SetTextSize(Android.Util.ComplexUnitType.Dip, ((float)((Element as VSmallPicker)?.FontSize ?? 12.0)));
                }
            }
        }
    }
}