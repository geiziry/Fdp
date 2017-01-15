using System.Windows;
using System.Windows.Controls;

namespace Fdp.InfraStructure.Behaviors
{
    public static class TextBoxAppendBehavior
    {
        public static readonly DependencyProperty TextToAppendProperty =
            DependencyProperty.RegisterAttached("TextToAppend", typeof(string), typeof(TextBoxAppendBehavior), new PropertyMetadata(null, null, OnTextToAppendChanged));

        public static string GetTextToAppend(DependencyObject obj) => (string)obj.GetValue(TextToAppendProperty);

        public static void SetTextToAppend(DependencyObject obj, string value)
        {
            obj.SetValue(TextToAppendProperty, value);
        }
        private static object OnTextToAppendChanged(DependencyObject d, object baseValue)
        {
            var sender = d as TextBox;
            if (sender != null && baseValue != null)
            {
                sender.AppendText(baseValue as string);
                sender.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }

            return null;
        }
    }
}