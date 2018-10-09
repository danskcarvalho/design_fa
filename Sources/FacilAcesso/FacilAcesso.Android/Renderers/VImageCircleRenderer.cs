using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: ExportRenderer(typeof(FacilAcesso.VImageCircle), typeof(FacilAcesso.Droid.VImageCircleRenderer))]
namespace FacilAcesso.Droid
{
    public class VImageCircleRenderer : ImageRenderer
    {
        public VImageCircleRenderer(Context ctx) : base(ctx)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Only enable hardware accelleration on lollipop
                if ((int)Android.OS.Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }

            }
        }

        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {
            try
            {

                var radius = Math.Min(Width, Height) / 2;

                var path = new Path();
                path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}
