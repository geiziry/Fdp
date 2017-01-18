using Fdp.DataModeller.Views;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace Fdp.DataModeller.ViewModels
{
    public class DataModellingViewModel : IRegionManagerAware, INavigationAware
    {
        private readonly IUnityContainer container;
        public DataModellingViewModel(IUnityContainer container)
        {
            this.container = container;
        }

        public IRegionManager _RegionManager { get; set; }
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