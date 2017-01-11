using Fdp.Controls.Views;
using Fdp.Essentials.Services;
using Fdp.Essentials.ViewModels;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Fdp.Essentials
{
    public class Module : IModule
    {
        IUnityContainer container;
        IRegionManager _regionManager;
        public Module(IUnityContainer _container, IRegionManager _regionManager)
        {
            this.container = _container;
            this._regionManager = _regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<IFdpDialogService, FdpDialogViewModel>(new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion(Strings.MenuRegion, typeof(DashboardMenuView));
            _regionManager.RegisterViewWithRegion(Strings.MainRegion, typeof(DashboardView));
        }
    }
}
