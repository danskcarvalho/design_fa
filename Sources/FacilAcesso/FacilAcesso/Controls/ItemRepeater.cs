using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public class ItemRepeater : StackLayout
    {
        public static readonly BindableProperty DefaultTemplateProperty =
           BindableProperty.Create("DefaultTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty FirstTemplateProperty =
           BindableProperty.Create("FirstTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty LastTemplateProperty =
           BindableProperty.Create("LastTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty EvenTemplateProperty =
           BindableProperty.Create("EvenTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty OddTemplateProperty =
           BindableProperty.Create("OddTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty SeparatorTemplateProperty =
           BindableProperty.Create("SeparatorTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty EmptyTemplateProperty =
           BindableProperty.Create("EmptyTemplate", typeof(DataTemplate), typeof(ItemRepeater), null, propertyChanged: OnPropertyChanged);
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(ItemRepeater), null, propertyChanged: OnItemsSourceChanged);

        public DataTemplate DefaultTemplate
        {
            get => (DataTemplate)GetValue(DefaultTemplateProperty);
            set => SetValue(DefaultTemplateProperty, value);
        }
        public DataTemplate FirstTemplate
        {
            get => (DataTemplate)GetValue(FirstTemplateProperty);
            set => SetValue(FirstTemplateProperty, value);
        }
        public DataTemplate LastTemplate
        {
            get => (DataTemplate)GetValue(LastTemplateProperty);
            set => SetValue(LastTemplateProperty, value);
        }
        public DataTemplate EvenTemplate
        {
            get => (DataTemplate)GetValue(EvenTemplateProperty);
            set => SetValue(EvenTemplateProperty, value);
        }
        public DataTemplate OddTemplate
        {
            get => (DataTemplate)GetValue(OddTemplateProperty);
            set => SetValue(OddTemplateProperty, value);
        }
        public DataTemplate SeparatorTemplate
        {
            get => (DataTemplate)GetValue(SeparatorTemplateProperty);
            set => SetValue(SeparatorTemplateProperty, value);
        }
        public DataTemplate EmptyTemplate
        {
            get => (DataTemplate)GetValue(EmptyTemplateProperty);
            set => SetValue(EmptyTemplateProperty, value);
        }
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisRepeater = bindable as ItemRepeater;
            thisRepeater.CreateList();
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisRepeater = bindable as ItemRepeater;
            if (oldValue is INotifyCollectionChanged)
                (oldValue as INotifyCollectionChanged).CollectionChanged -= thisRepeater.OnCollectionChanged;
            if (newValue is INotifyCollectionChanged)
                (newValue as INotifyCollectionChanged).CollectionChanged += thisRepeater.OnCollectionChanged;

            thisRepeater.CreateList();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.CreateList();
        }

        private void CreateList()
        {
            this.Children.Clear(); //remove all children

            List<object> allObjects = null;
            if (ItemsSource == null)
                allObjects = new List<object>();
            else
                allObjects = ItemsSource.Cast<object>().ToList();

            if(allObjects.Count == 0)
            {
                if(EmptyTemplate != null)
                    this.Children.Add(GetViewFromTemplate(EmptyTemplate));
            }
            else
            {
                for (int i = 0; i < allObjects.Count; i++)
                {
                    if(i == 0)
                    {
                        if (FirstTemplate != null)
                        {
                            var view = GetViewFromTemplate(FirstTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if(EvenTemplate != null)
                        {
                            var view = GetViewFromTemplate(EvenTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if(DefaultTemplate != null)
                        {
                            var view = GetViewFromTemplate(DefaultTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }

                        if (allObjects.Count != 1 && SeparatorTemplate != null)
                            this.Children.Add(GetViewFromTemplate(SeparatorTemplate));
                    }
                    else if(i == (allObjects.Count - 1))
                    {
                        if (LastTemplate != null)
                        {
                            var view = GetViewFromTemplate(LastTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if (i % 2 == 0 && EvenTemplate != null)
                        {
                            var view = GetViewFromTemplate(EvenTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if (i % 2 != 0 && OddTemplate != null)
                        {
                            var view = GetViewFromTemplate(OddTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if (DefaultTemplate != null)
                        {
                            var view = GetViewFromTemplate(DefaultTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                    }
                    else
                    {
                        if (i % 2 == 0 && EvenTemplate != null)
                        {
                            var view = GetViewFromTemplate(EvenTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if (i % 2 != 0 && OddTemplate != null)
                        {
                            var view = GetViewFromTemplate(OddTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }
                        else if (DefaultTemplate != null)
                        {
                            var view = GetViewFromTemplate(DefaultTemplate);
                            view.BindingContext = allObjects[i];
                            this.Children.Add(view);
                        }

                        if (SeparatorTemplate != null)
                            this.Children.Add(GetViewFromTemplate(SeparatorTemplate));
                    }
                }
            }
        }

        private View GetViewFromTemplate(DataTemplate template)
        {
            var value = template.CreateContent();
            if (value is ViewCell)
                return (value as ViewCell).View;
            else if (value is View)
                return value as View;
            else
                throw new InvalidOperationException($"invalid view {value.GetType().FullName}");
        }
    }
}
