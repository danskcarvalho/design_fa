using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FacilAcesso
{
    [ContentProperty("Tabs")]
    public class TabNavigation : StackLayout
    {
        protected override void OnBindingContextChanged()
        {
            foreach (var item in Header)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
            foreach (var item in Tabs)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
            base.OnBindingContextChanged();
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int), typeof(TabNavigation), -1,
           propertyChanged: OnSelectedIndexChanged, defaultBindingMode: BindingMode.TwoWay);
        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabNavigation).OnSelectedIndexChanged();
        }
        private void OnSelectedIndexChanged()
        {
            if (TabBar != null)
                TabBar.SelectedIndex = SelectedIndex;

            foreach (var tab in Tabs)
            {
                tab.IsVisible = false;
            }

            if (SelectedIndex >= 0 && SelectedIndex < Tabs.Count)
            {
                ContentLayout.Children[SelectedIndex].IsVisible = true;
                Tabs[SelectedIndex].HorizontalOptions = LayoutOptions.FillAndExpand;
                Tabs[SelectedIndex].VerticalOptions = LayoutOptions.FillAndExpand;
            }
        }
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(TabNavigation), null);
        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public static readonly BindableProperty PlacementProperty = BindableProperty.Create("Placement", typeof(TabPlacement), typeof(TabNavigation), TabPlacement.Top,
            propertyChanged: OnPlacementPropertyChanged);
        private static void OnPlacementPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabNavigation).OnPlacementPropertyChanged();
        }
        private void OnPlacementPropertyChanged()
        {
            Children.Clear();
            if(Placement == TabPlacement.Top)
            {
                Children.Add(TabBar);
                Children.Add(HeaderLayout);
                Children.Add(ContentLayout);
            }
            else
            {
                Children.Add(ContentLayout);
                Children.Add(HeaderLayout);
                Children.Add(TabBar);
            }

            TabBar.TabPlacement = Placement;
        }
        public TabPlacement Placement
        {
            get { return (TabPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.CreateAttached("Title", typeof(string), typeof(TabNavigation), null,
            propertyChanged: OnTitlePropertyChanged);
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.CreateAttached("ImageSource", typeof(ImageSource), typeof(TabNavigation), null,
            propertyChanged: OnImageSourcePropertyChanged);
        public static void SetTitle(BindableObject target, string value)
        {
            target.SetValue(TitleProperty, value);
        }
        public static string GetTitle(BindableObject target)
        {
            return (string)target.GetValue(TitleProperty);
        }
        public static void SetImageSource(BindableObject target, ImageSource value)
        {
            target.SetValue(ImageSourceProperty, value);
        }
        public static ImageSource GetImageSource(BindableObject target)
        {
            return (ImageSource)target.GetValue(ImageSourceProperty);
        }
        private static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var currentView = bindable as View;
            if (currentView != null)
            {
                var parent = GetTabParent(currentView);
                if (parent != null)
                {
                    parent.OnTitleChanged(currentView);
                }
            }
        }
        private static void OnImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var currentView = bindable as View;
            if (currentView != null)
            {
                var parent = GetTabParent(currentView);
                if (parent != null)
                {
                    parent.OnImageSourceChanged(currentView);
                }
            }
        }

        private static readonly BindableProperty TabParentProperty = BindableProperty.CreateAttached("TabParent", typeof(TabNavigation), typeof(TabNavigation), null);
        private static void SetTabParent(BindableObject target, TabNavigation value)
        {
            target.SetValue(TabParentProperty, value);
        }
        private static TabNavigation GetTabParent(BindableObject target)
        {
            return (TabNavigation)target.GetValue(TabParentProperty);
        }

        private void OnTitleChanged(View view)
        {
            var indexOf = Tabs.IndexOf(view);
            if(indexOf >= 0 && indexOf < TabBar.Tabs.Count)
            {
                TabBar.Tabs[indexOf].Text = GetTitle(view) ?? string.Empty;
            }
        }
        private void OnImageSourceChanged(View view)
        {
            var indexOf = Tabs.IndexOf(view);
            if (indexOf >= 0 && indexOf < TabBar.Tabs.Count)
            {
                TabBar.Tabs[indexOf].ImageSource = GetImageSource(view);
            }
        }

        private StackLayout HeaderLayout = null;
        private TabBar TabBar = null;
        private Grid ContentLayout = null;

        public TabNavigation()
        {
            Padding = 0;
            Spacing = 0;

            HeaderLayout = new StackLayout()
            {
                Margin = 0,
                Spacing = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            TabBar = new TabBar()
            {
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ContentLayout = new Grid()
            {
                Margin = 0,
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            ContentLayout.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            ContentLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

            Children.Add(TabBar);
            Children.Add(HeaderLayout);
            Children.Add(ContentLayout);

            Header = new ObservableCollection<View>();
            Tabs = new ObservableCollection<View>();

            TabBar.TabSelected += TabBar_TabSelected;
            Header.CollectionChanged += Header_CollectionChanged;
            Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        private void TabBar_TabSelected(object sender, TabEventArgs e)
        {
            if (SelectedCommand != null && SelectedCommand.CanExecute(e))
            {
                SelectedCommand.Execute(e);
            }

            if (!e.Cancel)
            {
                this.SelectedIndex = e.TabIndex;
            }
        }

        private void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (var t in Tabs)
                SetTabParent(t, null);

            TabBar.Tabs.Clear();
            ContentLayout.Children.Clear();

            foreach (var t in Tabs)
            {
                SetTabParent(t, this);
                TabBar.Tabs.Add(new TabBarButton() { Text = GetTitle(t) ?? string.Empty, ImageSource = GetImageSource(t) });
            }

            foreach (var tab in Tabs)
            {
                Grid.SetColumn(tab, 0);
                Grid.SetRow(tab, 0);
                tab.IsVisible = false;

                ContentLayout.Children.Add(tab);
            }

            if(SelectedIndex >= 0 && SelectedIndex < Tabs.Count)
            {
                ContentLayout.Children[SelectedIndex].IsVisible = true;
                Tabs[SelectedIndex].HorizontalOptions = LayoutOptions.FillAndExpand;
                Tabs[SelectedIndex].VerticalOptions = LayoutOptions.FillAndExpand;
            }
        }
        
        private void Header_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HeaderLayout.Children.Clear();

            foreach (var c in Header)
            {
                HeaderLayout.Children.Add(c);
            }
        }

        public ObservableCollection<View> Header
        {
            get;
            private set;
        }

        public ObservableCollection<View> Tabs
        {
            get;
            private set;
        }
    }
}
