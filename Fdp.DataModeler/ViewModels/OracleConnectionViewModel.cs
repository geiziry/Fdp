using Akka.Actor;
using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.DataModeller.ActorModel.Actors;
using Fdp.DataModeller.ActorModel.Actors.OracleActors.UI;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
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
        private readonly IActorRef _oracleCoordinatorActor;
        private readonly IActorRef _progressBarActor;

        //private TNSNamesReader TnsNamesReader;
        [Dependency]
        public IOracleConnectionBuildingService _oracleConnectionBuildingService { get; set; }

        public OracleConnectionViewModel(IActorRefFactory ActorSystem)
        {
            _progressBarActor =
                ActorSystem.ActorOf(
                    Props.Create(() => new ProgressBarActor(this)));
            _oracleCoordinatorActor =
                ActorSystem.ActorOf(
                    Props.Create(() => new OracleCoordinatorActor(this,_progressBarActor)));

            GetOracleUsersCommand = new DelegateCommand(() =>
            _oracleCoordinatorActor.Tell(new GetOracleUsersMessage(Connection)));

            #region commented

            //TnsNamesReader = new TNSNamesReader();
            //GetTnsNamesCommand = new DelegateCommand(() =>
            //{
            //    var Tns = TnsNamesReader.GetOracleHomes();
            //    TnsNamesReader.SetTnsFileText(Tns.FirstOrDefault(), true);
            //    TnsNames = new ObservableCollection<string>(TnsNamesReader.LoadTnsNames());
            //});

            //GetTnsFileCommand = new DelegateCommand(() =>
            //{
            //    FileDialog dialog = new OpenFileDialog();

            //    if (dialog.ShowDialog() == true)
            //    {
            //        TnsNamesReader.SetTnsFileText(dialog.FileName);
            //        TnsNames = new ObservableCollection<string>(TnsNamesReader.LoadTnsNames());
            //    }
            //});

            //GetOracleUsersCommand = new DelegateCommand(() => ManageProgress(async o => await GetOracleUsers(), nameof(IsGettingUsers)));

            #endregion commented
        }

        public FdpOracleConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand GetOracleUsersCommand { get; set; }

        public DelegateCommand GetTnsFileCommand { get; set; }

        public DelegateCommand GetTnsNamesCommand { get; set; }

        public Visibility IsGettingUsers
        {
            get { return _IsGettingUsers; }
            set
            {
                _IsGettingUsers = value;
                RaisePropertyChanged();
            }
        }

        public string Tns
        {
            get { return _Tns; }
            set
            {
                _Tns = value;
                if (!string.IsNullOrWhiteSpace(_Tns))
                {
                    Connection.DataSource = _oracleConnectionBuildingService.GetDataSource(_Tns);
                    ParentViewModel.ConnectionException = Connection.DataSource;
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

    }
}