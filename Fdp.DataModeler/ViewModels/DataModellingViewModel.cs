using Fdp.InfraStructure.Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Fdp.InfraStructure;
using Fdp.DataModeller.Views;

namespace Fdp.DataModeller.ViewModels
{
    public class DataModellingViewModel : IRegionManagerAware
    {
        private IRegionManager _RegionManager;
        public IRegionManager RegionManager { get { return _RegionManager; } set { _RegionManager=value;
                //RegionManager.RegisterViewWithRegion(Strings.DataSourcesRegion, typeof(DataSourcesView));
                //RegionManager.RegisterViewWithRegion(Strings.DefineVariablesRegion, typeof(DefineVariablesView));
                //RegionManager.RegisterViewWithRegion(Strings.AddVariablesRegion, typeof(AddVariablesView));
            }
        }

        public DataModellingViewModel()
        {
            
        }

    }
}
