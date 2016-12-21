using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace Fdp.Controls.Views
{
    /// <summary>
    /// Interaction logic for ButtonEditProgress.xaml
    /// </summary>
    public partial class ButtonEditProgress
    {
        public ButtonEditProgress()
        {
            InitializeComponent();
        }

        public ObservableCollection<string> ComboBoxItemsSource
        {
            get { return (ObservableCollection<string>)GetValue(ComboBoxItemsSourceProperty); }
            set { SetValue(ComboBoxItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropListItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxItemsSourceProperty =
            DependencyProperty.Register("ComboBoxItemsSource", typeof(ObservableCollection<string>), typeof(ButtonEditProgress), new PropertyMetadata(null));

        public string ComboBoxEditValue
        {
            get { return (string)GetValue(ComboBoxEditValueProperty); }
            set { SetValue(ComboBoxEditValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComboBoxEditValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComboBoxEditValueProperty =
            DependencyProperty.Register("ComboBoxEditValue", typeof(string), typeof(ButtonEditProgress),
                new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });

        public Visibility ProgressBarVisibility
        {
            get { return (Visibility)GetValue(ProgressBarVisibilityProperty); }
            set { SetValue(ProgressBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProgressBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProgressBarVisibilityProperty =
            DependencyProperty.Register("ProgressBarVisibility", typeof(Visibility), typeof(ButtonEditProgress), new PropertyMetadata(Visibility.Collapsed));

        public DelegateCommand<object> RefereshButtonCommand
        {
            get { return (DelegateCommand<object>)GetValue(RefereshButtonCommandProperty); }
            set { SetValue(RefereshButtonCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RefereshButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefereshButtonCommandProperty =
            DependencyProperty.Register("RefereshButtonCommand", typeof(DelegateCommand<object>), typeof(ButtonEditProgress), new PropertyMetadata(null));
    }
}
