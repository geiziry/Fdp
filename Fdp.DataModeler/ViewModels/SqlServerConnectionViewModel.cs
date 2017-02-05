using Akka.Actor;
using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.DataModeller.ActorModel.Actors;
using Fdp.DataModeller.ActorModel.Actors.SqlServerActors;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.AkkaHelpers;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class SqlServerConnectionViewModel : ConnectionBaseViewModel, IDisposable
    {
        private ObservableCollection<string> _Catalogs;

        private FdpSqlConnection _Connection = new FdpSqlConnection();

        private ObservableCollection<string> _DataSources;

        private Visibility _IsGettingCatalogs = Visibility.Collapsed;

        private Visibility _IsGettingSqlServers = Visibility.Collapsed;

        public SqlServerConnectionViewModel(IActorRefFactory ActorSystem)
        {
            InitializeActors(ActorSystem);
            GetNetworkServersCommand = new DelegateCommand(() =>
                SqlServerCoordinatorActor.Tell(new GetLocalNetworkServersMessage()));

            GetCatalogsCommand = new DelegateCommand(() =>
                SqlServerCoordinatorActor.Tell(new GetDatabaseListMessage(Connection)));
        }

        public ObservableCollection<string> Catalogs
        {
            get { return _Catalogs; }
            set
            {
                _Catalogs = value;
                RaisePropertyChanged();
            }
        }

        public FdpSqlConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertiesChanged();
            }
        }

        public ObservableCollection<string> DataSources
        {
            get { return _DataSources; }
            set
            {
                _DataSources = value;

                RaisePropertyChanged();
            }
        }

        public DelegateCommand GetCatalogsCommand { get; }

        public DelegateCommand GetNetworkServersCommand { get; }

        public Visibility IsGettingCatalogs
        {
            get { return _IsGettingCatalogs; }
            set
            {
                _IsGettingCatalogs = value;
                RaisePropertyChanged();
            }
        }

        public Visibility IsGettingSqlServers
        {
            get { return _IsGettingSqlServers; }
            set
            {
                _IsGettingSqlServers = value;
                RaisePropertyChanged();
            }
        }

        public IActorRef SqlServerCoordinatorActor { get; private set; }

        public void Dispose()
        {
            SqlServerCoordinatorActor.Tell(PoisonPill.Instance);
        }

        private void InitializeActors(IActorRefFactory actorSystem)
        {
            SqlServerCoordinatorActor =
                actorSystem.ActorOf(
                    Props.Create(() => new SqlServerCoordinatorActor(this)), ActorPaths.SqlServerCoordinatorActor.Name);
        }
    }
}