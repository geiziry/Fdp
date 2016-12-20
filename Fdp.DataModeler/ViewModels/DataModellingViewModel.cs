using Fdp.InfraStructure.Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Fdp.InfraStructure;
using Fdp.DataModeller.Views;
using Microsoft.Practices.Unity;

namespace Fdp.DataModeller.ViewModels
{
    public class DataModellingViewModel : IRegionManagerAware, INavigationAware
    {
        public IRegionManager _RegionManager { get; set; }

        private readonly IUnityContainer container;

        public DataModellingViewModel(IUnityContainer container)
        {
            this.container = container;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _RegionManager.RegisterViewWithRegion(Strings.AddVariablesRegion, typeof(AddVariablesView));
            _RegionManager.RegisterViewWithRegion(Strings.DefineVariablesRegion, typeof(DefineVariablesView));
            DataSourcesView dataSourcesView = container.Resolve<DataSourcesView>();
            _RegionManager.Regions[Strings.DataSourcesRegion].Add(dataSourcesView, "DataSources");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
