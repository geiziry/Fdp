using DevExpress.Mvvm.UI;
using Fdp.InfraStructure;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.Controls.ViewModels
{
    public class MenuViewLocator : IViewLocator
    {
        IUnityContainer container;
        IRegionManager _regionManager;


        public MenuViewLocator(IUnityContainer container, IRegionManager _regionManager)
        {
            this.container = container;
            this._regionManager = _regionManager;
        }

        public object ResolveView(string name)
        {
            if (name == "DataModellingView")
            {
                Assembly assembly = Assembly.LoadFrom(@"Fdp.DataModeller.dll");
                Type DataModellingView = assembly.GetType("Fdp.DataModeller.Views.DataModellingView");
                var dataModellingView = Activator.CreateInstance(DataModellingView);
                var scopedRegion = _regionManager.CreateRegionManager();
                RegionManager.SetRegionManager(dataModellingView as DependencyObject, scopedRegion);

                return dataModellingView;
            }
            return null;
        }

        public Type ResolveViewType(string name)
        {
            throw new NotImplementedException();
        }
    }
}
