using DevExpress.Mvvm;
using Fdp.InfraStructure;
using Prism.Modularity;
using System.Windows.Input;

namespace Fdp.Controls.ViewModels
{
    public class DashboardMenuViewModel
    {
        IModuleManager _moduleManager;


        public DashboardMenuViewModel(IModuleManager _moduleManager)
        {
            this._moduleManager = _moduleManager;
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
