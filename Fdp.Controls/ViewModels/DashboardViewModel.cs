using DevExpress.Mvvm;
using Fdp.Controls.CommonTypes;
using Fdp.InfraStructure;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fdp.Controls.ViewModels
{
    public class DashboardViewModel : BindableBase
    {
        IModuleManager _moduleManager;

        public DashboardViewModel(IModuleManager _moduleManager)
        {
            this._moduleManager = _moduleManager;
            Items = new ObservableCollection<DashboardItem>()
        {
            new LogViewModel {Label="LogView" },
            new PlotViewModel {Label="PlotView" }
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

        private DelegateCommand _LoadDataModelCommand;
        public ICommand LoadDataModelCommand
        {
            get
            {
                return _LoadDataModelCommand ?? (_LoadDataModelCommand = new DelegateCommand(() =>
                    {
                        _moduleManager.LoadModule(Strings.DataModellerModule);
                    }));
            }
        }

    }

}
