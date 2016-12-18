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
    public class DashboardMenuViewModel:IRegionManagerAware 
    {
        private readonly IFdpDialogService dialogService;
        private DelegateCommand _LoadDataModelCommand;

        public DashboardMenuViewModel(IFdpDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public ICommand LoadDataModelCommand
        {
            get
            {
                return _LoadDataModelCommand ?? (_LoadDataModelCommand = new DelegateCommand(() =>
                    {


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

        public IRegionManager _RegionManager { get; set; }

    }
}
