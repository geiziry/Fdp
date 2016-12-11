using Microsoft.Expression.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Input;

namespace Fdp.InfraStructure.Behaviors
{
    [System.Runtime.InteropServices.Guid("7DC276C1-4030-44F5-B3B5-A9F19A79488A")]
    public class ButtonClickBehavior:Behavior<Border>
    {



        public string MouseDownState
        {
            get { return (string)GetValue(MouseDownStateProperty); }
            set { SetValue(MouseDownStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseDownState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDownStateProperty =
            DependencyProperty.Register("MouseDownState", typeof(string), typeof(ButtonClickBehavior), new PropertyMetadata(null));




        public string MouseUpState
        {
            get { return (string)GetValue(MouseUpStateProperty); }
            set { SetValue(MouseUpStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseUpState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseUpStateProperty =
            DependencyProperty.Register("MouseUpState", typeof(string), typeof(ButtonClickBehavior), new PropertyMetadata(null));



        public string MouseOverState
        {
            get { return (string)GetValue(MouseOverStateProperty); }
            set { SetValue(MouseOverStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverStateProperty =
            DependencyProperty.Register("MouseOverState", typeof(string), typeof(ButtonClickBehavior), new PropertyMetadata(null));



        private FrameworkElement Parent;

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += OnLoaded;
            AssociatedObject.Unloaded += OnUnloaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= OnLoaded;
            AssociatedObject.Unloaded -= OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.MouseDown += MouseDown;
            AssociatedObject.MouseUp += MouseUp;
            AssociatedObject.MouseEnter += MouseEnter;
            AssociatedObject.MouseLeave += MouseLeave;

            VisualStateUtilities.TryFindNearestStatefulControl(AssociatedObject, out Parent);


        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.MouseDown -= MouseDown;
            AssociatedObject.MouseUp -= MouseUp;
            AssociatedObject.MouseEnter -= MouseEnter;
            AssociatedObject.MouseLeave -= MouseLeave;

        }

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateUtilities.GoToState(Parent, MouseOverState, true);
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            if (!(Parent as MenuItem).IsSubmenuOpen)
                VisualStateUtilities.GoToState(Parent,MouseUpState , true);
        }



        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
           if(!(Parent as MenuItem).IsSubmenuOpen)
            VisualStateUtilities.GoToState(Parent, MouseUpState, true);
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisualStateUtilities.GoToState(Parent, MouseDownState, true);
        }


    }
}
