using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FacilAcesso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VPicker), typeof(FacilAcesso.Droid.VPickerRenderer))]
namespace FacilAcesso.Droid
{
    public class VPickerRenderer : PickerRenderer
    {
        public VPickerRenderer(Context ctx) : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);
            var view = Element as VPicker;

            if (Control != null)
            {
                //Control.SetBackgroundResource(Resource.Drawable.VEntryBackground);
                //Control.SetPadding(30, 35, 30, 35);
                //Control.TextSize = 14;
                Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);

                //var thickness = (view as VPicker)?.Padding ?? new Thickness(30, 35, 30, 35);
                //Control.SetPadding((int)thickness.Left, (int)thickness.Top, (int)thickness.Right, (int)thickness.Bottom);
            }
        }
    }
}