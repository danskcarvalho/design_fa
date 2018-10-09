using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class VCustomPickerEntry : VEntry
    {
    }

    public class VCustomPicker : ContentView
    {
        public event EventHandler SelectedIndexChanged;

        public static BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int),
            typeof(VCustomPicker), -1, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged,
            validateValue: ValidateIndex);
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable<object>),
            typeof(VCustomPicker), null, propertyChanged: OnItemsSourceChanged);
        public static BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object),
            typeof(VCustomPicker), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);
        public static BindableProperty TextPaddingProperty = BindableProperty.Create("TextPadding", typeof(Thickness),
            typeof(VCustomPicker), new Thickness(0, 0), propertyChanged: OnTextPaddingChanged);
        public static BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string),
            typeof(VCustomPicker), null, propertyChanged: OnPlaceholderChanged);

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }
        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        public Thickness TextPadding
        {
            get => (Thickness)GetValue(TextPaddingProperty);
            set => SetValue(TextPaddingProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public VCustomPicker()
        {
            Padding = 0;
            SetContent();
        }

        private VCustomPickerEntry _Entry;
        private StackLayout _StackLayout;

        private void SetContent()
        {
            var grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };
            grid.Children.Add(_Entry = new VCustomPickerEntry());
            grid.Children.Add(_StackLayout = new StackLayout());

            _StackLayout.BackgroundColor = Color.Transparent;
            var tap = new TapGestureRecognizer();
            tap.Tapped += Tapped;
            _StackLayout.GestureRecognizers.Add(tap);

            Content = grid;
        }

        private async void Tapped(object sender, EventArgs e)
        {
            var page = this.GetParentPage() as VContentPage;
            if(page != null)
            {
                await page.DisplayModal(CreateModal(page));
            }
        }

        private VContentView _Modal = null;
        private VContentView CreateModal(VContentPage parent)
        {
            if (_Modal != null)
                return _Modal;
            _Modal = new VCustomPickerModal(
                ItemsSource.Select(x => x.ToString()).ToList(),
                Placeholder,
                (i) =>
                {
                    SelectedIndex = i;
                },
                parent);
            return _Modal;
        }

        private static bool ValidateIndex(BindableObject bindable, object value)
        {
            var newIndex = (int)value;
            return newIndex == -1 || newIndex >= 0;
        }
        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customPicker = bindable as VCustomPicker;
            var newIndex = (int)newValue;
            if (newIndex == -1)
            {
                if (customPicker.SelectedItem != null)
                    customPicker.SelectedItem = null;

                customPicker._Entry.Text = string.Empty;
            }
            else
            {
                //no item source...
                if (customPicker.ItemsSource == null)
                {
                    customPicker._Entry.Text = string.Empty;
                }
                else
                {

                    object newObject = customPicker.ItemsSource.Skip(newIndex).FirstOrDefault();
                    if (newObject != null)
                    {
                        if (!object.Equals(customPicker.SelectedItem, newObject))
                            customPicker.SelectedItem = newObject;

                        customPicker._Entry.Text = newObject.ToString();
                    }
                    else
                        throw new InvalidOperationException("Index too big.");
                }
            }
            customPicker.SelectedIndexChanged?.Invoke(customPicker, EventArgs.Empty);
        }
        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customPicker = bindable as VCustomPicker;
            customPicker._Entry.Placeholder = (string)newValue;
        }
        private static void OnTextPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customPicker = bindable as VCustomPicker;
            customPicker._Entry.Padding = (Thickness)newValue;
        }
        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customPicker = bindable as VCustomPicker;
            if (newValue == null)
            {
                if (customPicker.SelectedIndex != -1)
                    customPicker.SelectedIndex = -1;

                customPicker._Entry.Text = string.Empty;
                return;
            }
            else
            {
                //no item source...
                if (customPicker.ItemsSource == null)
                {
                    customPicker._Entry.Text = newValue.ToString();
                    return;
                }

                int? indexOf = null;
                int currentIndex = 0;
                foreach (var item in customPicker.ItemsSource)
                {
                    if(object.Equals(item, newValue))
                    {
                        indexOf = currentIndex;
                        break;
                    }
                    currentIndex++;
                }
                if (indexOf.HasValue)
                {
                    if (customPicker.SelectedIndex != indexOf)
                        customPicker.SelectedIndex = indexOf.Value;

                    customPicker._Entry.Text = newValue.ToString();
                }
                else
                    throw new InvalidOperationException("Item not present on ItemsSource.");
            }
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var customPicker = bindable as VCustomPicker;
            customPicker._Modal = null;
            if(customPicker.SelectedItem != null)
            {
                int? indexOf = null;
                int currentIndex = 0;
                foreach (var item in customPicker.ItemsSource)
                {
                    if (object.Equals(item, customPicker.SelectedItem))
                    {
                        indexOf = currentIndex;
                        break;
                    }
                    currentIndex++;
                }

                if (indexOf.HasValue)
                {
                    if (customPicker.SelectedIndex != indexOf)
                        customPicker.SelectedIndex = indexOf.Value;

                    customPicker._Entry.Text = customPicker.SelectedItem.ToString();
                }
                else
                    throw new InvalidOperationException("Item not present on ItemsSource.");
            }
            else if(customPicker.SelectedIndex != -1)
            {
                object newObject = customPicker.ItemsSource.Skip(customPicker.SelectedIndex).FirstOrDefault();
                if (newObject != null)
                {
                    if (!object.Equals(customPicker.SelectedItem, newObject))
                        customPicker.SelectedItem = newObject;

                    customPicker._Entry.Text = newObject.ToString();
                }
                else
                    throw new InvalidOperationException("Index too big.");
            }
        }
    }

    public static class ViewExtensions
    {
        /// <summary>
        /// Gets the page to which an element belongs
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="element">Element.</param>
        public static Page GetParentPage(this VisualElement element)
        {
            if (element != null)
            {
                var parent = element.Parent;
                while (parent != null)
                {
                    if (parent is Page)
                    {
                        return parent as Page;
                    }
                    parent = parent.Parent;
                }
            }
            return null;
        }
    }
}
