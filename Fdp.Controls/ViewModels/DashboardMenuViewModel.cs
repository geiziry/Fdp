using DevExpress.Mvvm;
using Fdp.InfraStructure;
using Prism.Modularity;
using System.Windows.Input;
using System;
using Microsoft.Practices.Unity;

namespace Fdp.Controls.ViewModels
{
    public class DashboardMenuViewModel : ISupportServices
    {
        IModuleManager _moduleManager;


        public DashboardMenuViewModel(IUnityContainer container,
            IModuleManager _moduleManager)
        {
            this._moduleManager = _moduleManager;
            serviceContainer = new ServiceContainer(this);
            container.RegisterInstance<IServiceContainer>(serviceContainer,
                new ContainerControlledLifetimeManager());
        }

        private DelegateCommand _LoadDataModelCommand;
        public ICommand LoadDataModelCommand
        {
            get
            {
                return _LoadDataModelCommand ?? (_LoadDataModelCommand = new DelegateCommand(() =>
                    {
                        _moduleManager.LoadModule(Strings.DataModellerModule);
                    }));
            }
        }

        IServiceContainer serviceContainer;
        public IServiceContainer ServiceContainer
        {
            get { return serviceContainer; }
        }
    }
}
