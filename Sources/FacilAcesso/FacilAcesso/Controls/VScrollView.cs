using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VScrollView : ScrollView
    {
        public static readonly BindableProperty VContentProperty =
          BindableProperty.Create(
            "VContent", typeof(View), typeof(VScrollView), null, propertyChanged: OnVContentChanged);

        public View VContent
        {
            get { return (View)GetValue(VContentProperty); }
            set { SetValue(VContentProperty, value); }
        }

        static void OnVContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as VScrollView).Content = newValue as View;
        }
    }
}
