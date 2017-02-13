using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Fdp.Essentials.Views;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Interfaces;
using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Linq;

namespace Fdp.Essentials.ViewModels
{
    public class FdpDialogViewModel : IFdpDialogService
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager _regionManager;
        private IRegionManager scopedRegion;
        public FdpDialogViewModel(IUnityContainer container, IRegionManager _regionManager)
        {
            this.container = container;
            this._regionManager = _regionManager;
        }

        public UICommand ShowDialog(string Title,string ViewType,
            IEnumerable<UICommand>dialogCommands)
        {
            var dxDialogWindow = new FdpDialogView
            {
                Title=Title,
                CommandsSource=dialogCommands,
            };
            
            scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(dxDialogWindow, scopedRegion);

            scopedRegion.RequestNavigate(Strings.DataModellingRegion, ViewType);
            var result = dxDialogWindow.ShowDialogWindow();

            var view = scopedRegion.Regions[Strings.DataModellingRegion].Views
                        .FirstOrDefault<object>(x => x.GetType().Name == ViewType);
            scopedRegion.Regions[Strings.DataModellingRegion].Remove(view);
            RegionManager.SetRegionManager(dxDialogWindow, null);
            return result;
        }
    }
}
