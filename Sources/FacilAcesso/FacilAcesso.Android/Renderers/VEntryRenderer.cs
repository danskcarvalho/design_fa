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
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FacilAcesso;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(VEntry), typeof(FacilAcesso.Droid.VEntryRenderer))]
namespace FacilAcesso.Droid
{
    public class VEntryRenderer : EntryRenderer
    {
        public VEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);

                //var thickness = (e.NewElement as VEntry)?.Padding ?? new Thickness(30, 35, 30, 35);

                //Control.SetBackgroundResource(Resource.Drawable.VEntryBackground);
                //Control.SetPadding((int)thickness.Left, (int)thickness.Top, (int)thickness.Right, (int)thickness.Bottom);
                if (e.NewElement?.IsPassword == false)
                    Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
            }
        }
    }
}