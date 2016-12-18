using DevExpress.Mvvm;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Prism;
using Prism.Regions;
using System.Linq;

namespace Fdp.DataModeller.ViewModels
{
    public class DataSourcesViewModel : BindableBase, IRegionManagerAware
    {


        public DataSourcesViewModel()
        {
        }

        private bool _IsAddDataSource;
        public bool IsAddDataSource
        {
            get { return _IsAddDataSource; }
            set { _IsAddDataSource = value;
                if(_IsAddDataSource &&!_RegionManager.Regions[Strings.DataSourceConnectionRegion].ActiveViews.Any())
                    _RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion, "Oracle");

                RaisePropertyChanged();
            }
        }


        private bool _IsOracle = true;
        public bool IsOracle
        {
            get
            {
                return _IsOracle;
            }
            set
            {
                _IsOracle = value;
                if (!_IsOracle)
                    _RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion, "Sql");
                else
                    _RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion, "Oracle");

                RaisePropertyChanged();
            }
        }

        public IRegionManager _RegionManager { get; set; }

    }
}
