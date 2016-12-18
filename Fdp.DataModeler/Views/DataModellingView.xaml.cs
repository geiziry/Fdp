﻿using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
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

namespace Fdp.DataModeller.Views
{
    /// <summary>
    /// Interaction logic for DataModellingView.xaml
    /// </summary>
    public partial class DataModellingView : UserControl,ICreateRegionManagerScope
    {

        public DataModellingView()
        {
            InitializeComponent();
        }

        public bool CreateRegionManagerScope
        {
            get
            {
               return true;
            }
        }

    }
}