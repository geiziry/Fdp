using Fdp.InfraStructure.Prism;
using Prism.Regions;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System;

namespace Fdp.DataModeller.Views
{
    /// <summary>
    /// Interaction logic for DataSourcesView.xaml
    /// </summary>
    public partial class DataSourcesView : UserControl
    {
        public DataSourcesView()
        {
            InitializeComponent();
            Loaded += (s, o) =>
            {
                ExpandedHeight.SetValue(DoubleKeyFrame.ValueProperty, dockPanel.ActualHeight);
            };
        }

    }
}
