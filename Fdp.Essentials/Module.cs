using Fdp.Controls.Views;
using Fdp.InfraStructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Fdp.Essentials
{
    public class Module : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public Module(IUnityContainer _container, IRegionManager _regionManager)
        {
            this._container = _container;
            this._regionManager = _regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(Strings.MenuRegion, typeof(DashboradMenu));
            _regionManager.RegisterViewWithRegion(Strings.MainRegion, typeof(Dashboard));
        }
    }
}
