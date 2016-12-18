﻿using Prism.Regions;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Fdp.DataModeller.Views
{
    /// <summary>
    /// Interaction logic for DataSourcesView.xaml
    /// </summary>
    public partial class DataSourcesView : UserControl
    {
        public DataSourcesView()
        {
            //RegionManager.SetRegionManager(this, _regionManager);
            InitializeComponent();
            Loaded += (s, o) =>
            {
                ExpandedHeight.SetValue(DoubleKeyFrame.ValueProperty, dockPanel.ActualHeight);
            };
        }

    }
}