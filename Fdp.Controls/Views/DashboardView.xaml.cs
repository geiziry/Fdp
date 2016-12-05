﻿using Fdp.Controls.CommonTypes;
using Fdp.Controls.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fdp.Controls.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {


        public DashboardView()
        {
            InitializeComponent();
            //this is to avoid instantiating the viewmodel before the InitializeComponent is executed
            Loaded += (s, o) =>
            {
                this.DataContext = new DashboardViewModel();
            };
        }

    }
}
