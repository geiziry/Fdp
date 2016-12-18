using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using Microsoft.Practices.Unity;
using DevExpress.Mvvm.UI;
using System;
using Fdp.InfraStructure.Interfaces;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Prism;
using Prism.Regions;

namespace Fdp.Controls.ViewModels
{
    [POCOViewModel]
    public class DashboardMenuViewModel:IRegionManagerAware //:ISupportServices
    {
        private IUnityContainer container;
        private readonly IFdpDialogService dialogService;
        private DelegateCommand _LoadDataModelCommand;

        public DashboardMenuViewModel(IUnityContainer container,IFdpDialogService dialogService)
        {
            this.container = container;
            this.dialogService = dialogService;
            //serviceContainer = new ServiceContainer(this);
            //container.RegisterInstance<IServiceContainer>(serviceContainer,
            //    new ContainerControlledLifetimeManager());
        }

        public ICommand LoadDataModelCommand
        {
            get
            {
                return _LoadDataModelCommand ?? (_LoadDataModelCommand = new DelegateCommand(() =>
                    {

                        //DialogService dialogService = serviceContainer.GetService<IDialogService>() as DialogService;
                        //this.GetService<IDialogService>() as DialogService;
                        //dialogService.ViewLocator = container.Resolve<IViewLocator>();

                        var Commandslist = new List<UICommand>() { new UICommand
                            {
                            Caption="Ok",Command=new DelegateCommand(()=>
                            {

                            }),
                            IsDefault=true,
                            Id=MessageBoxResult.OK

                            }, new UICommand {Caption="Cancel",Command=new DelegateCommand(()=>
                            {

                            }),
                            IsDefault=true,
                            Id=MessageBoxResult.Cancel

                            } };
                        UICommand result = dialogService.ShowDialog(
                            dialogCommands: Commandslist,
                            Title: "Data Sources",
                            ViewType: "Fdp.DataModeller.dll"
                            .GetTypeFromAssembly(@"Fdp.DataModeller.Views.DataModellingView")
                            );
                    }));
            }
        }

        public IRegionManager RegionManager { get; set; }

        //private IServiceContainer serviceContainer;
        //public IServiceContainer ServiceContainer
        //{
        //    get
        //    {
        //        return serviceContainer;
        //    }
        //}
    }
}
