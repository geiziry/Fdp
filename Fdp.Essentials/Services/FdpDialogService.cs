using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Fdp.InfraStructure.Interfaces;
using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;

namespace Fdp.Essentials.Services
{
    public class FdpDialogService : IFdpDialogService
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager _regionManager;

        public FdpDialogService(IUnityContainer container, IRegionManager _regionManager)
        {
            this.container = container;
            this._regionManager = _regionManager;
        }

        public UICommand ShowDialog(string Title,Type ViewType,
            IEnumerable<UICommand>dialogCommands)
        {
            DXDialogWindow dxDialogWindow = new DXDialogWindow
            {
                Title=Title,
                Content = GetView(ViewType),
                CommandsSource=dialogCommands,
                WindowStartupLocation= WindowStartupLocation.CenterOwner
            };
            this.UpdateOwner(dxDialogWindow);
            //RegionManager.SetRegionManager(dxDialogWindow, _regionManager);

            //RegionManagerAware.SetRegionManagerAware(dxDialogWindow, _regionManager);

            //Interaction.GetBehaviors(dxDialogWindow).Add(new DimmParentWindowBehavior());
            return dxDialogWindow.ShowDialogWindow();
        }

        private void UpdateOwner(DXDialogWindow dxDialogWindow)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(dxDialogWindow);
            windowInteropHelper.Owner = NativeMethods.GetActiveWindow();
        }

        private object GetView(Type viewType)
        {
            var view = container.Resolve(viewType);
            var scoped = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(view as DependencyObject, scoped);
            RegionManagerAware.SetRegionManagerAware(view, scoped);

            return view;
        }
    }
}
