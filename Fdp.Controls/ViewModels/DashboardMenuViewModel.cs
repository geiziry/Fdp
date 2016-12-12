using DevExpress.Mvvm;
using Fdp.InfraStructure;
using Prism.Modularity;
using System.Windows.Input;
using System;
using Microsoft.Practices.Unity;
using DevExpress.Mvvm.UI;
using System.Windows;
using System.Collections.Generic;

namespace Fdp.Controls.ViewModels
{
    public class DashboardMenuViewModel : ISupportServices
    {
        IModuleManager _moduleManager;


        IServiceContainer serviceContainer;
        public IServiceContainer ServiceContainer
        {
            get { return serviceContainer; }
        }

        private IDialogService DialogService { get { return serviceContainer.GetService<IDialogService>(); } }


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
                        UICommand result = DialogService.ShowDialog(
                            dialogCommands: new List<UICommand>() { new UICommand
                            {
                            Caption="Ok",Command=new DelegateCommand(()=>
                            {

                            }),
                            IsDefault=true,
                            Id=MessageBoxResult.OK

                            } },
                            title: "Data Sources",
                            documentType: "DataModellingView",
                            parameter: "Parameter",
                            parentViewModel: this
                            );
                        //_moduleManager.LoadModule(Strings.DataModellerModule);
                    }));
            }
        }

    }
}
