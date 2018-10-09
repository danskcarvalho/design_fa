using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VEntry : Entry
    {
        public static readonly BindableProperty PaddingProperty = 
            BindableProperty.Create("Padding", typeof(Thickness), typeof(VEntry), new Thickness(30, 35, 30, 35));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
    }
}
