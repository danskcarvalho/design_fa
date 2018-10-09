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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Util;

[assembly: ExportRenderer(typeof(FacilAcesso.VImage), typeof(FacilAcesso.Droid.VImageRenderer))]
namespace FacilAcesso.Droid
{
    public class VImageRenderer : ImageRenderer
    {
        private bool _panStarted = false;
        private bool _pinchStarted = false;
        private double _originDistance;

        private float startX = 0;
        private float startY = 0;

        public VImageRenderer(Context ctx) : base(ctx)
        {

        }

        public float PxToDp(float px)
        {
            DisplayMetrics displayMetrics = Context.Resources.DisplayMetrics;
            return px / ((float)displayMetrics.DensityDpi / (int)DisplayMetricsDensity.Default);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            _panStarted = false;
            _pinchStarted = false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var totalX = e.GetX();
            var totalY = e.GetY();

            var rawX = PxToDp(e.RawX);
            var rawY = PxToDp(e.RawY);

            var element = Element as VImage;

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _pinchStarted = false;
                    _panStarted = true;

                    startX = PxToDp(e.RawX);
                    startY = PxToDp(e.RawY);

                    element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Started, e.ActionIndex, 0, 0));

                    break;

                case MotionEventActions.Move:
                    if (e.PointerCount > 1)
                    {
                        var x1 = e.GetX(1);
                        var y1 = e.GetY(1);

                        if (_panStarted)
                        {
                            EndPan(element, rawX - startX, rawY - startY);
                        }

                        if (!_pinchStarted)
                        {
                            _pinchStarted = true;

                            element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Started));
                            _originDistance = GetDistance(totalX, totalY, x1, y1);
                        }
                        else
                        {
                            var distance = GetDistance(totalX, totalY, x1, y1);
                            var scale = distance / _originDistance;
                            _originDistance = distance;

                            var centre = new Point(Math.Min(totalX, x1) + Math.Abs(x1 - totalX) / 2, Math.Min(totalY, y1) + Math.Abs(y1 - totalY) / 2);
                            var origin = new Point(centre.X / this.Width, centre.Y / this.Height);

                            element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Running, scale, origin));
                        }
                    }
                    else if (_panStarted)
                    {
                        element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Running, e.ActionIndex, rawX - startX, rawY - startY));
                    }

                    break;

                case MotionEventActions.Up:
                    if (_panStarted)
                    {
                        EndPan(element, rawX - startX, rawY - startY);
                    }
                    else if (_pinchStarted)
                    {
                        element.OnPinchUpdated(this, new PinchGestureUpdatedEventArgs(GestureStatus.Completed));
                        _pinchStarted = false;
                    }
                    break;

                default:
                    return base.OnTouchEvent(e);
            }

            return true;
        }

        private double GetDistance(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private void EndPan(VImage element, float x, float y)
        {
            element.OnPanUpdated(this, new PanUpdatedEventArgs(GestureStatus.Completed, 0, x, y));
            _panStarted = false;
        }
    }
}