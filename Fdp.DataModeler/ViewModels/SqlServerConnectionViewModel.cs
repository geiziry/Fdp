using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.InfraStructure.Interfaces;
using Prism.Common;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Fdp.DataModeller.ViewModels
{
    public class SqlServerConnectionViewModel : BindableBase, INavigationAware
    {
        public SqlServerConnectionViewModel()
        {
            GetNetworkServersCommand = new DelegateCommand(() =>
            {
                ManageProgress(async (o) => DataSources = new ObservableCollection<string>(await Connection.GetLocalNetworkServersAsync().ConfigureAwait(false)), nameof(IsGettingSqlServers));
            });

            GetCatalogsCommand = new DelegateCommand(() =>
            {
                ManageProgress(async (o) => Catalogs = new ObservableCollection<string>(await Connection.GetDatabaseListAsync().ConfigureAwait(false)), nameof(IsGettingCatalogs));
            });
        }

        private async void ManageProgress(Func<object, Task> action, string progressVisibility)
        {
            ParentViewModel.ConnectionException = string.Empty;
            var visibility = this.GetType().GetProperty(progressVisibility);
            visibility.SetValue(this, Visibility.Visible);
            await action(null);
            visibility.SetValue(this, Visibility.Collapsed);

        }


        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var navigationService = navigationContext.NavigationService;
            ParentViewModel = navigationService.Region.Context as IDataSourceConnectionException;
            Connection.ExceptionRaised += Connection_ExceptionRaised;
        }

        private void Connection_ExceptionRaised(object sender, string e)
        {
            if (ParentViewModel != null)
                ParentViewModel.ConnectionException = e;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Connection.ExceptionRaised -= Connection_ExceptionRaised;

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

        private IDataSourceConnectionException _ParentViewModel;

        public IDataSourceConnectionException ParentViewModel
        {
            get { return _ParentViewModel; }
            set
            {
                _ParentViewModel = value;
                RaisePropertyChanged();
            }
        }


    }
}
