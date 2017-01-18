using Akka.Actor;
using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.DataModeller.ActorModel.Actors;
using Fdp.DataModeller.ActorModel.Actors.OracleActors;
using Fdp.DataModeller.ActorModel.Actors.OracleActors.UI;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.AkkaHelpers;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class OracleConnectionViewModel : ConnectionBaseViewModel
    {
        private FdpOracleConnection _Connection = new FdpOracleConnection();
        private Visibility _IsGettingUsers = Visibility.Collapsed;
        private string _Tns;
        private ObservableCollection<string> _TnsNames;
        private ObservableCollection<string> _usersList;

        public OracleConnectionViewModel(IActorRefFactory ActorSystem)
        {
            InitializeActors(ActorSystem);
            GetOracleUsersCommand = new DelegateCommand<object>((o) =>
            {
                if (o == null)
                    OracleCoordinatorActor.Tell(new GetOracleUsersMessage(Connection));
            });

            GetTnsNamesCommand = new DelegateCommand<object>((o) =>
              {
                  if (o == null)
                      ActorSystem.ActorSelection(ActorPaths.GetTnsNamesActor.Path)
                      .Tell(new TnsNamesTextMessage(), OracleCoordinatorActor);
              });

            GetTnsFileCommand = new DelegateCommand(() =>
                         ActorSystem.ActorSelection(ActorPaths.GetTnsNamesActor.Path)
                        .Tell(new TnsNamesFileMessage(), OracleCoordinatorActor));
        }

        [Dependency]
        public IOracleConnectionBuildingService _oracleConnectionBuildingService { get; set; }

        public IActorRef ProgressBarActor { get; private set; }

        public FdpOracleConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand<object> GetOracleUsersCommand { get; set; }

        public DelegateCommand GetTnsFileCommand { get; set; }

        public DelegateCommand<object> GetTnsNamesCommand { get; set; }

        public Visibility IsGettingUsers
        {
            get { return _IsGettingUsers; }
            set
            {
                _IsGettingUsers = value;
                RaisePropertyChanged();
            }
        }

        public IActorRef OracleCoordinatorActor { get; private set; }

        public string Tns
        {
            get { return _Tns; }
            set
            {
                _Tns = value;
                if (!string.IsNullOrWhiteSpace(_Tns))
                {
                    Connection.DataSource = _oracleConnectionBuildingService.GetDataSource(_Tns);
                    ParentViewModel.TextToAppend = Connection.DataSource;
                }
            }
        }

        public ObservableCollection<string> TnsNames
        {
            get { return _TnsNames; }
            set
            {
                _TnsNames = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                RaisePropertyChanged();
            }
        }

        public bool IsCoordinatorActorAlive { get; set; }
        private void InitializeActors(IActorRefFactory actorSystem)
        {
            var f=actorSystem.ActorSelection(ActorPaths.OracleCoordinatorActor.Path);

            if (!IsCoordinatorActorAlive)
            {
                ProgressBarActor =
                    actorSystem.ActorOf(
                        Props.Create(() => new ProgressBarActor(this)),"ProgressBar");
                OracleCoordinatorActor =
                    actorSystem.ActorOf(
                        Props.Create(() => new OracleCoordinatorActor(this, ProgressBarActor)), "OracleCoordinator");
                IsCoordinatorActorAlive = true;
            }
        }
    }
}