using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using System.Collections.ObjectModel;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class SqlServerConnectionViewModel : ConnectionBaseViewModel
    {
        private ObservableCollection<string> _Catalogs;

        private FdpSqlConnection _Connection = new FdpSqlConnection();

        private ObservableCollection<string> _DataSources;

        private Visibility _IsGettingCatalogs = Visibility.Collapsed;

        private Visibility _IsGettingSqlServers = Visibility.Collapsed;

        public SqlServerConnectionViewModel()
        {
            //GetNetworkServersCommand = new DelegateCommand(() => ManageProgress(async o => DataSources =
            //                                new ObservableCollection<string>(await Connection.GetLocalNetworkServersAsync()
            //                                .ConfigureAwait(false)), nameof(IsGettingSqlServers)));

            //GetCatalogsCommand = new DelegateCommand(() => ManageProgress(async o => Catalogs =
            //                              new ObservableCollection<string>(await Connection.GetDatabaseListAsync()
            //                            .ConfigureAwait(false)), nameof(IsGettingCatalogs)));
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
    }
}