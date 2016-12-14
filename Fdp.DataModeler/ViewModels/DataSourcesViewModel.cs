using DevExpress.Mvvm;
using Fdp.DataModeller.Views;
using Fdp.InfraStructure;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataModeller.ViewModels
{
    public class DataSourcesViewModel:BindableBase
    {
        IRegionManager _regionManager;


        public DataSourcesViewModel(IRegionManager _regionManager)
        {
            this._regionManager = _regionManager;
        }

        private bool _IsOracle=true;
        public bool IsOracle
        {
            get { 
                if (_IsOracle)
                    _regionManager.RequestNavigate(Strings.DataSourceConnectionRegion,typeof(OracleConnectionView).FullName);
                else
                    _regionManager.RequestNavigate(Strings.DataSourceConnectionRegion,typeof(SqlServerConnectionView).FullName);
                return _IsOracle;
            }
            set { _IsOracle = value;
                RaisePropertyChanged();
            }
        }

    }
}
