using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using System.Collections.ObjectModel;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class SqlServerConnectionViewModel : ConnectionBaseViewModel
    {
        public SqlServerConnectionViewModel()
        {
            GetNetworkServersCommand = new DelegateCommand(() => ManageProgress(async (o) => DataSources =
                                            new ObservableCollection<string>(await Connection.GetLocalNetworkServersAsync()
                                            .ConfigureAwait(false)), nameof(IsGettingSqlServers)));

            GetCatalogsCommand = new DelegateCommand(() => ManageProgress(async (o) => Catalogs=
                                          new ObservableCollection<string>(await Connection.GetDatabaseListAsync()
                                        .ConfigureAwait(false)), nameof(IsGettingCatalogs)));
        }

        private SqlServerConnection _Connection = new SqlServerConnection();

        public SqlServerConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertiesChanged();
            }
        }

        public DelegateCommand GetNetworkServersCommand { get; }
        public DelegateCommand GetCatalogsCommand { get; }

        private ObservableCollection<string> _DataSources;

        public ObservableCollection<string> DataSources
        {
            get { return _DataSources; }
            set
            {
                _DataSources = value;

                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _Catalogs;

        public ObservableCollection<string> Catalogs
        {
            get { return _Catalogs; }
            set
            {
                _Catalogs = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _IsGettingSqlServers = Visibility.Collapsed;

        public Visibility IsGettingSqlServers
        {
            get { return _IsGettingSqlServers; }
            set
            {
                _IsGettingSqlServers = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _IsGettingCatalogs = Visibility.Collapsed;

        public Visibility IsGettingCatalogs
        {
            get { return _IsGettingCatalogs; }
            set
            {
                _IsGettingCatalogs = value;
                RaisePropertyChanged();
            }
        }
    }
}
