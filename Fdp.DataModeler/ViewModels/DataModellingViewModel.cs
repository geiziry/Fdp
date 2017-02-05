using Fdp.DataModeller.Views;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class DataModellingViewModel : IRegionManagerAware, INavigationAware,IDisposable
    {
        private readonly IUnityContainer container;
        public DataModellingViewModel(IUnityContainer container)
        {
            this.container = container;
        }

        public IRegionManager _RegionManager { get; set; }

        public void Dispose()
        {
            var view = _RegionManager.Regions[Strings.DataSourcesRegion].Views
                .FirstOrDefault<object>(v => 
                    (v as FrameworkElement)?.DataContext is IRegionManagerAware) as FrameworkElement;

            if (view!=null)
            {
                var viewModel = view.DataContext as IRegionManagerAware;
                var region = viewModel._RegionManager.Regions.FirstOrDefault(x => x.Name == Strings.DataSourceConnectionRegion);

                foreach (FrameworkElement childview in region.Views)
                {
                    if (childview.DataContext is IDisposable)
                    {
                        (childview.DataContext as IDisposable).Dispose();
                    }
                }
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _RegionManager.RegisterViewWithRegion(Strings.AddVariablesRegion, typeof(AddVariablesView));
            _RegionManager.RegisterViewWithRegion(Strings.DefineVariablesRegion, typeof(DefineVariablesView));
            DataSourcesView dataSourcesView = container.Resolve<DataSourcesView>();
            _RegionManager.Regions[Strings.DataSourcesRegion].Add(dataSourcesView, "DataSources", true);

        }
    }
}