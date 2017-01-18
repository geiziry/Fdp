using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Editors;
using System;
using System.Linq;
using System.Windows;

namespace Fdp.InfraStructure.Behaviors
{
    public static class ComboBoxBehaviors
    {
        public static readonly DependencyProperty ShowPopupProperty =
            DependencyProperty.RegisterAttached("ShowPopup",
                typeof(bool),
                typeof(ComboBoxBehaviors),
                new PropertyMetadata(false, OnShowingPopup));

        public static bool GetShowPopup(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowPopupProperty);
        }

        public static void SetShowPopup(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowPopupProperty, value);
        }

        private static void OnShowingPopup(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var combobox = LayoutTreeHelper.GetVisualChildren(d).OfType<ComboBoxEdit>().FirstOrDefault();

            if (combobox != null)
            {
                combobox.PopupOpening += Combobox_PopupOpening;
                combobox.ShowPopup();
                combobox.PopupOpening -= Combobox_PopupOpening;
            }
        }

        private static void Combobox_PopupOpening(object sender, OpenPopupEventArgs e)
        {
            e.Cancel = true;
        }
    }
}