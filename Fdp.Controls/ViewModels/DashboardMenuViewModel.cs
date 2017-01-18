using Akka.Actor;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Fdp.InfraStructure;
using Fdp.InfraStructure.AkkaHelpers;
using Fdp.InfraStructure.Interfaces;
using Fdp.InfraStructure.Prism;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Fdp.Controls.ViewModels
{
    [POCOViewModel]
    public class DashboardMenuViewModel : IRegionManagerAware
    {
        private readonly IFdpDialogService dialogService;
        private DelegateCommand _LoadDataModelCommand;

        public DashboardMenuViewModel(IFdpDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public IRegionManager _RegionManager { get; set; }

        [Dependency]
        public IActorRefFactory ActorSystem { get; set; }

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
                        ActorSystem.ActorSelection(ActorPaths.OracleCoordinatorActor.Path).Tell(PoisonPill.Instance);
                        ActorSystem.ActorSelection(ActorPaths.ProgressBarActor.Path).Tell(PoisonPill.Instance);
                    }));
            }
        }
    }
}