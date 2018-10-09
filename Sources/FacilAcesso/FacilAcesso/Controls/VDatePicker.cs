using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VDatePicker : DatePicker
    {
        public VDatePicker()
        {
        }

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create("Padding", typeof(Thickness), typeof(VDatePicker), new Thickness(30, 35, 30, 35));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static readonly BindableProperty NullableDateProperty =
            BindableProperty.Create("NullableDate", typeof(DateTime?), typeof(VDatePicker), null, BindingMode.TwoWay);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set
            {
                if (value != NullableDate)
                {
                    SetValue(NullableDateProperty, value);
                    UpdateDate();
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
            BindableProperty.Create("Placeholder", typeof(string), typeof(VDatePicker), string.Empty, BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(VDatePicker), Color.LightGray);

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
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
                    if (!NullableDate.HasValue)
                    {
                        Date = (DateTime)DateTime.Today;
                    }
                }
                else
                {
                    OnPropertyChanged(DateProperty.PropertyName);
                }
            }
            //});

            if (propertyName == DateProperty.PropertyName)
            {
                NullableDate = Date;
            }

            if (propertyName == NullableDateProperty.PropertyName)
            {
                if (NullableDate.HasValue)
                {
                    Date = NullableDate.Value;
                }
            }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                Date = NullableDate.Value;
            }
            else
            {
                Date = (DateTime)DateTime.Today;
            }
        }
    }
}
