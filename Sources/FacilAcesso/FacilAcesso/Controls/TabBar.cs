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
    public class TabBarButton : BindableObject
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(TabBarButton), null);
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(TabBarButton), null);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
    }
    public class TabEventArgs
    {
        public TabEventArgs()
        {

        }
        public TabEventArgs(int tabIndex)
        {
            this.TabIndex = tabIndex;
        }

        public bool Cancel { get; set; }
        public int TabIndex { get; private set; }
    }
    public enum TabPlacement
    {
        Top,
        Bottom
    }

    [ContentProperty("Tabs")]
    public class TabBar : Grid
    {
        protected override void OnBindingContextChanged()
        {
            foreach (var item in Tabs)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
            base.OnBindingContextChanged();
        }
        public static readonly BindableProperty TabPlacementProperty = BindableProperty.Create("TabPlacement", typeof(TabPlacement), typeof(TabBar), TabPlacement.Top,
            propertyChanged: OnTabPlacementChanged);
        private static void OnTabPlacementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabBar).OnTabPlacementChanged();
        }
        private void OnTabPlacementChanged()
        {
            RecreateTabs();
        }
        public TabPlacement TabPlacement
        {
            get => (TabPlacement)GetValue(TabPlacementProperty);
            set => SetValue(TabPlacementProperty, value);
        }

        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int), typeof(TabBar), -1,
            propertyChanged: OnSelectedIndexChanged);
        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as TabBar).OnSelectedIndexChanged();
        }
        private void OnSelectedIndexChanged()
        {
            if (TabPlacement == TabPlacement.Top)
            {
                foreach (var item in Children)
                {
                    if (item is StackLayout && ((StackLayout)item).BackgroundColor == Color.White)
                    {
                        var stripe = item as StackLayout;
                        if (Grid.GetColumn(stripe) == SelectedIndex)
                            stripe.IsVisible = true;
                        else
                            stripe.IsVisible = false;
                    }
                }
            }
            else
            {
                foreach (var item in Children)
                {
                    if (item is StackLayout)
                    {
                        var wholeButton = item as StackLayout;
                        foreach (var subitem in wholeButton.Children)
                        {
                            if (subitem is StackLayout)
                            {
                                var stripe = subitem as StackLayout;
                                if (Grid.GetColumn(wholeButton) == SelectedIndex)
                                    stripe.IsVisible = true;
                                else
                                    stripe.IsVisible = false;
                            }
                        }
                    }
                }
            }
        }
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public string SelectedValue
        {
            get => Tabs[SelectedIndex].Text;
        }

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(TabBar), null);
        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public TabBar()
        {
            Padding = 0;
            BackgroundColor = Color.FromHex("#5A96D2");
            ColumnSpacing = 0;
            RowSpacing = 0;
            
            Tabs = new ObservableCollection<TabBarButton>();
            Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        private void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    var tabItem = item as TabBarButton;
                    tabItem.PropertyChanged -= TabItem_PropertyChanged;
                }
            }
            //make sure that all property changed in tabs only raise one event in this view...
            foreach (var item in Tabs)
            {
                item.PropertyChanged -= TabItem_PropertyChanged;
                item.PropertyChanged += TabItem_PropertyChanged;
            }

            RecreateTabs();
        }

        private void TabItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RecreateTabs();
        }

        private void RecreateTabs()
        {
            if (TabPlacement == TabPlacement.Top)
                RecreateTopTabs();
            else
                RecreateBottomTabs();
        }

        private void RecreateBottomTabs()
        {
            Children.Clear();
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();

            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            //create column definitions
            for (int i = 0; i < Tabs.Count; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            }

            //create the tabs
            foreach (var i in Enumerable.Range(0, Tabs.Count))
            {
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (sender, e) =>
                {
                    var args = new TabEventArgs(i);
                    TabSelected?.Invoke(this, args);
                    if (SelectedCommand != null && SelectedCommand.CanExecute(args))
                        SelectedCommand.Execute(args);
                    if (!args.Cancel)
                    {
                        SelectedIndex = i;
                    }
                };

                var stackLayout = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0
                };
                stackLayout.GestureRecognizers.Add(tap);

                var image = new Image()
                {
                    Source = Tabs[i].ImageSource,
                    WidthRequest = 30,
                    HeightRequest = 30,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                var label = new Label()
                {
                    Text = Tabs[i].Text,
                    FontSize = 10,
                    TextColor = Color.White,
                    Margin = 5,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };
                var stripe = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    IsVisible = SelectedIndex == i,
                    HeightRequest = 4,
                    Margin = new Thickness(0, 0, 0, 1)
                };
                stackLayout.Children.Add(image);
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(stripe);

                Grid.SetRow(stackLayout, 0);
                Grid.SetColumn(stackLayout, i);
                Children.Add(stackLayout);
            }
        }

        private void RecreateTopTabs()
        {
            Children.Clear();
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();

            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(5) });

            //create column definitions
            for (int i = 0; i < Tabs.Count; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            }

            //create the tabs
            foreach (var i in Enumerable.Range(0, Tabs.Count))
            {
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (sender, e) =>
                {
                    var args = new TabEventArgs(i);
                    TabSelected?.Invoke(this, args);
                    if (SelectedCommand != null && SelectedCommand.CanExecute(args))
                        SelectedCommand.Execute(args);
                    if (!args.Cancel)
                    {
                        SelectedIndex = i;
                    }
                };

                var label = new Label()
                {
                    Text = Tabs[i].Text,
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 7),
                    FontSize = 14
                };
                label.GestureRecognizers.Add(tap);

                var stripe = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    IsVisible = SelectedIndex == i,
                    Margin = new Thickness(0, 0, 0, 1)
                };
                stripe.GestureRecognizers.Add(tap);

                var background = new StackLayout()
                {
                    IsVisible = true,
                    BackgroundColor = Color.FromHex("#5A96D2"),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                background.GestureRecognizers.Add(tap);

                Grid.SetRow(background, 0);
                Grid.SetColumn(background, i);
                Grid.SetRowSpan(background, 2);
                Children.Add(background);

                Grid.SetRow(label, 0);
                Grid.SetColumn(label, i);
                Children.Add(label);

                Grid.SetRow(stripe, 1);
                Grid.SetColumn(stripe, i);
                Children.Add(stripe);
            }
        }

        public ObservableCollection<TabBarButton> Tabs
        {
            get; private set;
        }

        public event EventHandler<TabEventArgs> TabSelected;
    }
}
