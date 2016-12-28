using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace Fdp.Controls.Views
{
    /// <summary>
    /// Interaction logic for ButtonEditProgress.xaml
    /// </summary>
    public partial class ButtonEditProgress
    {
        // Using a DependencyProperty as the backing store for ComboBoxEditValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxEditValueProperty =
            DependencyProperty.Register("ComboBoxEditValue", typeof(string), typeof(ButtonEditProgress),
                new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        // Using a DependencyProperty as the backing store for DropListItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxItemsSourceProperty =
            DependencyProperty.Register("ComboBoxItemsSource", typeof(ObservableCollection<string>), typeof(ButtonEditProgress), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ComboBoxSelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxSelectedItemProperty =
            DependencyProperty.Register("ComboBoxSelectedItem", typeof(string), typeof(ButtonEditProgress),
                new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

        // Using a DependencyProperty as the backing store for IsTextEditable.  This enables animation, styling, binding, etc...
        public static readonly new DependencyProperty IsTextEditableProperty =
            DependencyProperty.Register("IsTextEditable", typeof(bool), typeof(ButtonEditProgress), new PropertyMetadata(true));

        // Using a DependencyProperty as the backing store for NullText.  This enables animation, styling, binding, etc...
        public static readonly new DependencyProperty NullTextProperty =
            DependencyProperty.Register("NullText", typeof(string), typeof(ButtonEditProgress), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ProgressBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarVisibilityProperty =
            DependencyProperty.Register("ProgressBarVisibility", typeof(Visibility), typeof(ButtonEditProgress), new PropertyMetadata(Visibility.Collapsed));

        // Using a DependencyProperty as the backing store for RefereshButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefereshButtonCommandProperty =
            DependencyProperty.Register("RefereshButtonCommand", typeof(DelegateCommand<object>), typeof(ButtonEditProgress), new PropertyMetadata(null));

        public ButtonEditProgress()
        {
            InitializeComponent();
        }

        public string ComboBoxEditValue
        {
            get { return (string)GetValue(ComboBoxEditValueProperty); }
            set { SetValue(ComboBoxEditValueProperty, value); }
        }

        public ObservableCollection<string> ComboBoxItemsSource
        {
            get { return (ObservableCollection<string>)GetValue(ComboBoxItemsSourceProperty); }
            set { SetValue(ComboBoxItemsSourceProperty, value); }
        }
        public string ComboBoxSelectedItem
        {
            get { return (string)GetValue(ComboBoxSelectedItemProperty); }
            set { SetValue(ComboBoxSelectedItemProperty, value); }
        }
        public new bool IsTextEditable
        {
            get { return (bool)GetValue(IsTextEditableProperty); }
            set { SetValue(IsTextEditableProperty, value); }
        }

        public new string NullText
        {
            get { return (string)GetValue(NullTextProperty); }
            set { SetValue(NullTextProperty, value); }
        }

        public Visibility ProgressBarVisibility
        {
            get { return (Visibility)GetValue(ProgressBarVisibilityProperty); }
            set { SetValue(ProgressBarVisibilityProperty, value); }
        }
        public DelegateCommand<object> RefereshButtonCommand
        {
            get { return (DelegateCommand<object>)GetValue(RefereshButtonCommandProperty); }
            set { SetValue(RefereshButtonCommandProperty, value); }
        }
    }
}