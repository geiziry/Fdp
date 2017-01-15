﻿using DevExpress.Mvvm;
using Fdp.InfraStructure;
using Fdp.InfraStructure.Interfaces;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using Fdp.InfraStructure.Prism;
using Prism.Regions;
using System.Linq;

namespace Fdp.DataModeller.ViewModels
{
    public class DataSourcesViewModel : BindableBase, IRegionManagerAware, IDataSourceConnectionException
    {
        public DataSourcesViewModel()
        {
            EmptyErrorListCommand = new DelegateCommand(() => ConnectionException = null);
        }
        private bool _IsAddDataSource;
        public bool IsAddDataSource
        {
            get { return _IsAddDataSource; }
            set
            {
                _IsAddDataSource = value;
                if (_IsAddDataSource && !_RegionManager.Regions[Strings.DataSourceConnectionRegion].ActiveViews.Any())
                    _RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion, "OracleConnectionView");

                RaisePropertyChanged();
            }
        }

        private bool _IsOracle = true;
        public bool IsOracle
        {
            get { return _IsOracle; }
            set
            {
                _IsOracle = value;
                if (!_IsOracle)
                    _RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion, "SqlServerConnectionView");
                else
                    _RegionManager.RequestNavigate(Strings.DataSourceConnectionRegion, "OracleConnectionView");

                RaisePropertyChanged();
            }
        }

        private string _ConnectionException;

        public string ConnectionException
        {
            get { return _ConnectionException; }
            set
            {
                _ConnectionException = value;
                RaisePropertiesChanged(new string[]{"ConnectionException",
                    "ConnectionExceptionNotNull" });
            }
        }

        public bool ConnectionExceptionNotNull => !string.IsNullOrEmpty(ConnectionException);

        public DelegateCommand EmptyErrorListCommand { get; set; }
        public IRegionManager _RegionManager { get; set; }

        private string _textToAppend;

        public string TextToAppend
        {
            get { return _textToAppend; }
            set { _textToAppend = value;
                RaisePropertyChanged();
            }
        }

    }
}
