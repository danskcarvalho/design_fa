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

[assembly: ExportRenderer(typeof(VDatePicker), typeof(FacilAcesso.Droid.VDatePickerRenderer))]
namespace FacilAcesso.Droid
{
    public class VDatePickerRenderer : DatePickerRenderer
    {
        public VDatePickerRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            var view = Element as VDatePicker;

            if (Control != null)
            {
                SetNullableText(view);
                SetPlaceholder(view);
                SetPlaceholderTextColor(view);

                //Control.SetBackgroundResource(Resource.Drawable.VEntryBackground);
                Control.SetPadding(30, 35, 30, 35);
                Control.TextSize = 14;

                var thickness = (view as VDatePicker)?.Padding ?? new Thickness(30, 35, 30, 35);
                Control.SetPadding((int)thickness.Left, (int)thickness.Top, (int)thickness.Right, (int)thickness.Bottom);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (VDatePicker)Element;

            if (e.PropertyName == VDatePicker.NullableDateProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == VDatePicker.PlaceholderProperty.PropertyName)
                SetPlaceholder(view);
            else if (e.PropertyName == VDatePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

        }

        private void SetNullableText(VDatePicker view)
        {
            if (view == null)
                return;
            if (view.NullableDate == null)
                Control.Text = string.Empty;
        }
        
        private void SetPlaceholder(VDatePicker view)
        {
            if (view == null)
                return;
            Control.Hint = view.Placeholder;
        }
        
        private void SetPlaceholderTextColor(VDatePicker view)
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