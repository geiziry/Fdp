using DevExpress.Mvvm;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using Prism.Regions;
using System.Reflection;
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
        }

        public void UpdateProgressBarVisibility(PropertyInfo visibility, bool isVisible)
            => visibility.SetValue(this, isVisible ?
                Visibility.Visible :
                Visibility.Collapsed);
    }
}