using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.DataAccess.DBConnection;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fdp.DataModeller.ViewModels
{
    public class OracleConnectionViewModel : ConnectionBaseViewModel
    {
        private OracleConnection _Connection;
        private string _Tns;
        private ObservableCollection<string> _TnsNames;
        private TNSNamesReader TnsNamesReader;

        public OracleConnectionViewModel()
        {
            TnsNamesReader = new TNSNamesReader();
            Connection = new OracleConnection();
            GetTnsNamesCommand = new DelegateCommand(() =>
              {
                  var Tns = TnsNamesReader.GetOracleHomes();
                  TnsNamesReader.SetTnsFileText(Tns.FirstOrDefault(), true);
                  TnsNames = new ObservableCollection<string>(TnsNamesReader.LoadTnsNames());
              });

            GetTnsFileCommand = new DelegateCommand(() =>
              {
                  FileDialog dialog = new OpenFileDialog();

                  if (dialog.ShowDialog() == true)
                  {
                      TnsNamesReader.SetTnsFileText(dialog.FileName);
                      TnsNames = new ObservableCollection<string>(TnsNamesReader.LoadTnsNames());
                  }
              });
        }
        public OracleConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand GetTnsFileCommand { get; set; }
        public DelegateCommand GetTnsNamesCommand { get; set; }
        public string Tns
        {
            get { return _Tns; }
            set
            {
                _Tns = value;
                if (!string.IsNullOrWhiteSpace(_Tns))
                {
                    Connection.DataSource = TnsNamesReader.GetDataSource(_Tns);
                    ParentViewModel.ConnectionException = Connection.DataSource;
                }
            }
        }

        public ObservableCollection<string> TnsNames
        {
            get { return _TnsNames; }
            set
            {
                _TnsNames = value;
                RaisePropertyChanged();
            }
        }
    }
}