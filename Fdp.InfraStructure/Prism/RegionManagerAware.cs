﻿using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.InfraStructure.Prism
{
    public static class RegionManagerAware
    {
        public static void SetRegionManagerAware(object item,IRegionManager regionManager)
        {
            var rmAware = item as IRegionManagerAware;
            if (rmAware!=null)
                rmAware._RegionManager = regionManager;

            var rmAwareFrameworkElement = item as FrameworkElement;
            if (rmAwareFrameworkElement!=null)
            {
                var rmAwareDataContext = rmAwareFrameworkElement.DataContext as IRegionManagerAware;
                if (rmAwareDataContext != null)
                    rmAwareDataContext._RegionManager = regionManager;
            }

        }
    }
}
