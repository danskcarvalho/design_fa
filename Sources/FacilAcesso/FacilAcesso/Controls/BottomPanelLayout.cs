using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class BottomPanelLayout : Layout<View>
    {
        public static readonly BindableProperty MainViewProperty = BindableProperty.Create("MainView", typeof(View), typeof(BottomPanelLayout), null,
           propertyChanged: OnMainViewChanged);

        private static void OnMainViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BottomPanelLayout layout = bindable as BottomPanelLayout;
            View oldView = oldValue as View;
            if (oldView != null)
                layout.Children.Remove(oldView);
            View newView = newValue as View;
            layout.Children.Insert(0, newView);
        }

        public static readonly BindableProperty BottomViewProperty = BindableProperty.Create("BottomView", typeof(View), typeof(BottomPanelLayout), null,
           propertyChanged: OnBottomViewChanged);

        private static void OnBottomViewChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BottomPanelLayout layout = bindable as BottomPanelLayout;
            View oldView = oldValue as View;
            if (oldView != null)
                layout.Children.Remove(oldView);
            View newView = newValue as View;
            layout.Children.Add(newView);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (MainView != null)
                LayoutChildIntoBoundingRegion(MainView, new Rectangle(x, y, width, height));

            if (BottomView != null) {
                var measured = BottomView.Measure(width, height);
                LayoutChildIntoBoundingRegion(BottomView, new Rectangle(x, y + height - measured.Request.Height, width, measured.Request.Height));
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (MainView == null)
                return new SizeRequest();

            var mainViewSize = MainView.Measure(widthConstraint, heightConstraint);

            if (BottomView != null)
                BottomView.Measure(widthConstraint, heightConstraint);

            return mainViewSize;    
        }

        public View MainView
        {
            get => (View)GetValue(MainViewProperty);
            set => SetValue(MainViewProperty, value);
        }

        public View BottomView
        {
            get => (View)GetValue(BottomViewProperty);
            set => SetValue(BottomViewProperty, value);
        }
    }
}
