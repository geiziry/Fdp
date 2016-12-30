using DevExpress.Xpf.Core;
using System;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core.Native;
using System.Windows.Media;
using System.Windows.Documents;

namespace Fdp.InfraStructure.Behaviors
{
    public class DimmParentWindowBehavior:Behavior<DXWindow>
    {
        DXWindow AssociatedWindow { get { return AssociatedObject; } }

        private ShellAdorner adorner;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedWindow.Loaded += Loaded;
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            var ParentWnd = AssociatedWindow.Owner as Window;

            ContentPresenter adornedElement =
                (ContentPresenter)LayoutHelper.FindElement(ParentWnd, (el) => { return el is ContentPresenter; });
            adorner = new ShellAdorner(adornedElement)
            {
                Child = new Border() { Background = new SolidColorBrush(Color.FromArgb(0xAD, 0x81, 0x81, 0x81)) }
            };

            AdornerLayer.GetAdornerLayer(adornedElement).Add(adorner);

            AssociatedWindow.Closed += Closed;
        }

        private void Closed(object sender, EventArgs e)
        {
            DXDialogWindow dialog = sender as DXDialogWindow;
            dialog.Closed -= Closed;
            ContentPresenter adornedElement =
                (ContentPresenter)LayoutHelper.FindElement(dialog.Owner, el => { return el is ContentPresenter; });
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            adornerLayer.Remove(adorner);
        }
    }
}
