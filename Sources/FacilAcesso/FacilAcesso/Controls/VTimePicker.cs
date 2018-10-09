using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VTimePicker : TimePicker
    {
        public VTimePicker()
        {
        }

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create("Padding", typeof(Thickness), typeof(VDatePicker), new Thickness(30, 35, 30, 35));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static readonly BindableProperty NullableTimeProperty =
            BindableProperty.Create("NullableTime", typeof(TimeSpan?), typeof(VTimePicker), null, BindingMode.TwoWay);

        public TimeSpan? NullableTime
        {
            get { return (TimeSpan?)GetValue(NullableTimeProperty); }
            set
            {
                if (value != NullableTime)
                {
                    SetValue(NullableTimeProperty, value);
                    UpdateTime();
                }
            }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(VTimePicker), string.Empty, BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(VTimePicker), Color.LightGray);

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateTime();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            //Device.OnPlatform(() =>
            //{
            if (propertyName == IsFocusedProperty.PropertyName)
            {
                if (IsFocused)
                {
                    if (!NullableTime.HasValue)
                    {
                        Time = default(TimeSpan);
                    }
                }
                else
                {
                    OnPropertyChanged(TimeProperty.PropertyName);
                }
            }
            //});

            if (propertyName == TimeProperty.PropertyName)
            {
                NullableTime = Time;
            }

            if (propertyName == NullableTimeProperty.PropertyName)
            {
                if (NullableTime.HasValue)
                {
                    Time = NullableTime.Value;
                }
            }
        }

        private void UpdateTime()
        {
            if (NullableTime.HasValue)
            {
                Time = NullableTime.Value;
            }
            else
            {
                Time = default(TimeSpan);
            }
        }
    }
}
