using System;
using System.Windows;
using System.Windows.Controls;

namespace Fdp.InfraStructure.Behaviors
{
    public static class TextBoxAppendBehavior
    {
        public static readonly DependencyProperty TextToAppendProperty =
            DependencyProperty.RegisterAttached("TextToAppend", typeof(string), typeof(TextBoxAppendBehavior), new PropertyMetadata(null,OnTextToAppendChanged));

        private static void OnTextToAppendChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as TextBox;
            if (sender != null && e.NewValue != null)
            {
                sender.AppendText(e.NewValue as string);
                sender.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        public static string GetTextToAppend(DependencyObject obj) => (string)obj.GetValue(TextToAppendProperty);

        public static void SetTextToAppend(DependencyObject obj, string value)
        {
            obj.SetValue(TextToAppendProperty, value);
        }
    }
}