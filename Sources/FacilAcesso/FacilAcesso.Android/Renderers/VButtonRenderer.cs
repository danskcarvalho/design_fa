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
using FacilAcesso;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VButton), typeof(FacilAcesso.Droid.VButtonRenderer))]
namespace FacilAcesso.Droid
{
    public class VButtonRenderer : ButtonRenderer
    {
        public VButtonRenderer(Context ctx) : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.Gravity = GravityFlags.Center;
                Control.SetPadding(Control.PaddingLeft, 0, Control.PaddingRight, 0);
            }
        }
    }
}