using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.DataAccess.DBConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class OracleConnectionViewModel : ConnectionBaseViewModel
    {
        private TNSNamesReader TnsNamesReader;
        public OracleConnectionViewModel()
        {
            TnsNamesReader = new TNSNamesReader();
            Connection = new OracleConnection();
            GetTnsNamesCommand = new DelegateCommand(() =>
              {
                  var Tns = TnsNamesReader.GetOracleHomes();
                  TnsNames =new ObservableCollection<string>(TnsNamesReader.LoadTNSNames(Tns.FirstOrDefault()));
              });
        }

        private OracleConnection _Connection;

        public OracleConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand GetTnsNamesCommand { get; set; }

        private ObservableCollection<string> _TnsNames;

        public ObservableCollection<string> TnsNames
        {
            get { return _TnsNames; }
            set
            {
                _TnsNames = value;
                RaisePropertyChanged();
            }
        }

        private string _Tns;

        public string Tns
        {
            get { return _Tns; }
            set { _Tns = value;
                if (!string.IsNullOrWhiteSpace(_Tns))
                {
                    Connection.DataSource = TnsNamesReader.GetDataSource(_Tns);
                    ParentViewModel.ConnectionException = Connection.DataSource;
                }
            }
        }


    }
}
