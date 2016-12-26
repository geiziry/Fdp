using DevExpress.Mvvm;
using Fdp.InfraStructure.Interfaces;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class ConnectionBaseViewModel : BindableBase, INavigationAware
    {
        protected async void ManageProgress(Func<object, Task> action, string progressVisibility)
        {
            var visibility = this.GetType().GetProperty(progressVisibility);
            try
            {
                ParentViewModel.ConnectionException = string.Empty;
                visibility.SetValue(this, Visibility.Visible);
                await action(null).ConfigureAwait(false);
                visibility.SetValue(this, Visibility.Collapsed);
            }
            catch (Exception ex)
            {
                ParentViewModel.ConnectionException = ex.Message;
                visibility.SetValue(this, Visibility.Collapsed);
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var navigationService = navigationContext.NavigationService;
            ParentViewModel = navigationService.Region.Context as IDataSourceConnectionException;
            ParentViewModel.ConnectionException = string.Empty;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

    }
}
