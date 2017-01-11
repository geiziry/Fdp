using DevExpress.Mvvm;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using Prism.Regions;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class ConnectionBaseViewModel : BindableBase, INavigationAware
    {
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

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var navigationService = navigationContext.NavigationService;
            ParentViewModel = navigationService.Region.Context as IDataSourceConnectionException;
            ParentViewModel.ConnectionException = string.Empty;
        }

        protected async void ManageProgress(Func<object, Task> action, string progressVisibility)
        {
            var visibility = this.GetType().GetProperty(progressVisibility);
            try
            {
                ParentViewModel.ConnectionException = string.Empty;
                UpdateProgressBarVisibility(visibility, true);
                await action(null).ConfigureAwait(false);
                UpdateProgressBarVisibility(visibility, false);
            }
            catch (Exception ex)
            {
                ParentViewModel.ConnectionException = ex.Message;
                UpdateProgressBarVisibility(visibility, false);
            }
        }

        public void UpdateProgressBarVisibility(PropertyInfo visibility, bool isVisible) 
            => visibility.SetValue(this, isVisible ? 
                Visibility.Visible : 
                Visibility.Collapsed);
    }
}