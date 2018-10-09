using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FacilAcesso
{
    public static class EventBehavior
    {
        public static readonly BindableProperty TappedProperty =
            BindableProperty.CreateAttached(
                "Tapped",
                typeof(ICommand),
                typeof(EventBehavior),
                null,
                propertyChanged: OnTappedChanged);
        public static readonly BindableProperty UnselectableProperty =
            BindableProperty.CreateAttached(
                "Unselectable",
                typeof(bool),
                typeof(EventBehavior),
                false,
                propertyChanged: OnUnselectableChanged);
        public static readonly BindableProperty SelectCommandProperty =
            BindableProperty.CreateAttached(
                "SelectCommand",
                typeof(ICommand),
                typeof(EventBehavior),
                null,
                propertyChanged: OnSelectedCommandChanged);
        public static readonly BindableProperty SearchedProperty =
            BindableProperty.CreateAttached(
                "Searched",
                typeof(ICommand),
                typeof(EventBehavior),
                null,
                propertyChanged: OnSearchChanged);

        public static ICommand GetSearched(BindableObject view)
        {
            return (ICommand)view.GetValue(SearchedProperty);
        }

        public static void SetSearched(BindableObject view, ICommand value)
        {
            view.SetValue(SearchedProperty, value);
        }

        public static ICommand GetTapped(BindableObject view)
        {
            return (ICommand)view.GetValue(TappedProperty);
        }

        public static void SetTapped(BindableObject view, ICommand value)
        {
            view.SetValue(TappedProperty, value);
        }

        public static bool GetUnselectable(BindableObject view)
        {
            return (bool)view.GetValue(UnselectableProperty);
        }

        public static void SetUnselectable(BindableObject view, bool value)
        {
            view.SetValue(UnselectableProperty, value);
        }

        public static ICommand GetSelectCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(SelectCommandProperty);
        }

        public static void SetSelectCommand(BindableObject view, ICommand value)
        {
            view.SetValue(SelectCommandProperty, value);
        }

        private static void OnSearchChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var myView = bindable as Entry;
            var action = newValue as ICommand;

            myView.SetUp(() =>
            {
                if (action.CanExecute(myView.Text))
                    action.Execute(myView.Text);
            });
        }

        static void OnTappedChanged(BindableObject view, object oldValue, object newValue)
        {
            var myView = view as View;
            if (myView == null)
                return;

            var cmd = (ICommand)newValue;

            if (cmd != null)
            {
                var removeTaps = myView.GestureRecognizers.Where(x => x is TapGestureRecognizer).ToList();
                foreach (var tgr in removeTaps)
                {
                    myView.GestureRecognizers.Remove(tgr);
                }

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    if (cmd.CanExecute(null))
                        cmd.Execute(null);
                };
                myView.GestureRecognizers.Add(tapGestureRecognizer);
            }
        }

        private static void OnSelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var myView = bindable as ListView;
            if (myView == null)
                return;

            var val = (ICommand)newValue;

            myView.ItemSelected -= MyView_ItemSelected2;
            if (val != null)
                myView.ItemSelected += MyView_ItemSelected2;
        }

        private static void MyView_ItemSelected2(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView.SelectedItem;
            if (selectedItem == null)
                return;

            if (listView.SelectedItem != null)
                listView.SelectedItem = null;
            var command = GetSelectCommand((BindableObject)sender);
            if (command.CanExecute(selectedItem))
                command.Execute(selectedItem);
        }

        static void OnUnselectableChanged(BindableObject view, object oldValue, object newValue)
        {
            var myView = view as ListView;
            if (myView == null)
                return;

            var val = (bool)newValue;

            if (val)
                myView.ItemSelected += MyView_ItemSelected;
            else
                myView.ItemSelected -= MyView_ItemSelected;
        }

        private static void MyView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var myView = sender as ListView;
            if(myView != null)
            {
                if (myView.SelectedItem != null)
                    myView.SelectedItem = null;
            }
        }
    }
}
