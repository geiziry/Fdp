using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.LayoutControl;
using Fdp.Controls.CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Fdp.Controls.Behaviors
{
    public class LayoutControlItemsSourceBehavior:Behavior<LayoutControl>
    {
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }
        public static readonly DependencyProperty ItemTemplateSelectorProperty =
            DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(LayoutControlItemsSourceBehavior), new UIPropertyMetadata(null));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(LayoutControlItemsSourceBehavior), new UIPropertyMetadata(null));

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =


            DependencyProperty.Register("ItemsSource", typeof(object), typeof(LayoutControlItemsSourceBehavior), new UIPropertyMetadata(null,
                new PropertyChangedCallback((d, e) =>
                {
                    ((LayoutControlItemsSourceBehavior)d).OnItemsSourceChanged(e.OldValue, e.NewValue);
                })));

        protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
        {
            if (newValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged += OnItemsSourceCollectionChanged;
            }
            ArrangeChildren();
        }

        protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
                AssociatedObject.Children.Clear();

            if (e.NewItems != null)
                foreach (var item in e.NewItems)
                    AddItem(item);
            if (e.OldItems != null)
                foreach (var item in e.OldItems)
                    RemoveItem(item as DashboardItem);
        }

        protected virtual void ArrangeChildren()
        {
            AssociatedObject.Orientation = System.Windows.Controls.Orientation.Vertical;
            if (ItemsSource is IEnumerable)
                foreach (var item in (ItemsSource as IEnumerable))
                    AddItem(item);
        }
        protected virtual void RemoveItem(object current)
        {
            var lg = AssociatedObject.Children.Cast<object>().Select(x => x).Where(y => (y as FrameworkElement).Name == "RootGroup").First() as LayoutGroup;
            LayoutItem element = lg.Children.OfType<LayoutItem>().Where(el => (((LayoutItem)el).DataContext).Equals(current)).FirstOrDefault();
            lg.Children.Remove(element);
        }

        protected virtual void AddItem(object current)
        {
            LayoutItem lItem = new LayoutItem();// { DataContext = current };
            lItem.DataContext = current;
            if (current is DashboardItem)
            {
                DashboardItem di = current as DashboardItem;
                LayoutControl.SetTabHeader(lItem, di.Label);
            }

            var itemContent = new ContentControl();// { Content = current };
            itemContent.Content = current;
            lItem.Content = itemContent;

            Binding b1 = new Binding("ItemTemplate");
            b1.Mode = BindingMode.TwoWay;
            b1.Source = this;
            itemContent.SetBinding(ContentControl.ContentTemplateProperty, b1);

            Binding b2 = new Binding("ItemTemplateSelector");
            b2.Source = this;
            b2.Mode = BindingMode.OneWay;
            itemContent.SetBinding(ContentControl.ContentTemplateSelectorProperty, b2);

            var lg = AssociatedObject.Children.Cast<object>().Select(x => x).Where(y => (y as FrameworkElement).Name == "RootGroup").First() as LayoutGroup;
            if (lg != null)
                lg.Children.Add(lItem);
        }

    }
}
