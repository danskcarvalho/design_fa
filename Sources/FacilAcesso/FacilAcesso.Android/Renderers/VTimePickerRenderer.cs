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
using System.ComponentModel;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(VTimePicker), typeof(FacilAcesso.Droid.VTimePickerRenderer))]
namespace FacilAcesso.Droid
{
    public class VTimePickerRenderer : TimePickerRenderer
    {
        public VTimePickerRenderer(Context ctx) : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);
            var view = Element as VTimePicker;

            if (Control != null)
            {
                SetNullableText(view);
                SetPlaceholder(view);
                SetPlaceholderTextColor(view);

                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                //Control.SetBackgroundResource(Resource.Drawable.VEntryBackground);
                //Control.SetPadding(30, 35, 30, 35);
                //Control.TextSize = 14;

                //var thickness = (view as VTimePicker)?.Padding ?? new Thickness(30, 35, 30, 35);
                //Control.SetPadding((int)thickness.Left, (int)thickness.Top, (int)thickness.Right, (int)thickness.Bottom);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (VTimePicker)Element;

            if (e.PropertyName == VTimePicker.NullableTimeProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == VTimePicker.PlaceholderProperty.PropertyName)
                SetPlaceholder(view);
            else if (e.PropertyName == VTimePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

        }

        private void SetNullableText(VTimePicker view)
        {
            if (view == null)
                return;
            if (view.NullableTime == null)
                Control.Text = string.Empty;
        }

        private void SetPlaceholder(VTimePicker view)
        {
            if (view == null)
                return;
            Control.Hint = view.Placeholder;
        }

        private void SetPlaceholderTextColor(VTimePicker view)
        {
            if (view == null)
                return;
            if (view.PlaceholderTextColor != Color.Default)
            {
                Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}