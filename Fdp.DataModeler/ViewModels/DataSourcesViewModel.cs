using DevExpress.Mvvm;
using Fdp.DataModeller.Views;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Prism;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataModeller.ViewModels
{
    public class DataSourcesViewModel:BindableBase,IRegionManagerAware
    {


        public DataSourcesViewModel()
        {
        }

        private bool _IsOracle=true;
        public bool IsOracle
        {
            get { 
                if (_IsOracle)
                {
                    //IRegion DataSourceConnectionRegion = RegionManager.Regions[Strings.DataSourceConnectionRegion];
                    //var uri = new Uri("OracleConnectionView", UriKind.Relative);
                    //DataSourceConnectionRegion.RequestNavigate(uri);

                }
                else { }
                    //RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion,
                    //    typeof(SqlServerConnectionView).FullName);
                return _IsOracle;
            }
            set { _IsOracle = value;
                RaisePropertyChanged();
            }
        }

        public IRegionManager RegionManager { get; set ; }
    }
}
