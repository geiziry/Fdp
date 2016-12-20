using DevExpress.Mvvm;
using Fdp.Controls.CommonTypes;
using System.Collections.ObjectModel;
using System;

namespace Fdp.Controls.ViewModels
{
    public class DashboardViewModel : BindableBase
    {
        public DashboardViewModel()
        {
            AddDashboardItemCommand = new DelegateCommand<object>(AddItem);
            InfraStructure.ApplicationCommands.AddDashboardItemCommand.RegisterCommand(AddDashboardItemCommand);
            Items = new ObservableCollection<DashboardItem>()
            {
                new LogViewModel { Label = "LogView"},
                new PlotViewModel { Label = "PlotView"}
            };
        }

        private void AddItem(object item)
        {
            var dashboardItem = Activator.CreateInstance(Type.GetType(item.ToString())) as DashboardItem;
            dashboardItem.Label = item.ToString();
            Items.Add(dashboardItem);
        }

        public DelegateCommand<object> AddDashboardItemCommand { get; }

        public ObservableCollection<DashboardItem> Items { get; set; }

        private bool _IsCustomize;
        public bool IsCustomize
        {
            get { return _IsCustomize; }
            set
            {
                _IsCustomize = value;
                RaisePropertyChanged();
            }
        }
    }
}
