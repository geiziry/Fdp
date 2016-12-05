using DevExpress.Mvvm;
using Fdp.Controls.CommonTypes;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Fdp.Controls.ViewModels
{
    public class DashboardViewModel : BindableBase
    {

        public DashboardViewModel()
        {
            Items = new ObservableCollection<DashboardItem>()
            {
                new LogViewModel { Label = "LogView" },
            new PlotViewModel { Label = "PlotView" }
            };

        }


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

        private DelegateCommand<string> _AddItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                return _AddItemCommand ?? (_AddItemCommand = new DelegateCommand<string>(s =>
                 {
                     switch (s)
                     {
                         case "LogView":
                             Items.Add(new LogViewModel { Label = "Log" });
                             break;
                         case "PlotView":
                             Items.Add(new PlotViewModel { Label = "Plot" });
                             break;
                         default:
                             break;
                     }
                 }));
            }
        }


    }

}
