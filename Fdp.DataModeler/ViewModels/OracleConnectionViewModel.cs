using DevExpress.Mvvm;
using Fdp.DataAccess.DatabaseSchema;
using Fdp.DataAccess.DBConnection;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fdp.DataModeller.ViewModels
{
    public class OracleConnectionViewModel : ConnectionBaseViewModel
    {
        private FdpOracleConnection _Connection;
        private Visibility _IsGettingUsers = Visibility.Collapsed;
        private string _Tns;
        private ObservableCollection<string> _TnsNames;
        private ObservableCollection<string> _usersList;
        private TNSNamesReader TnsNamesReader;

        public OracleConnectionViewModel()
        {
            TnsNamesReader = new TNSNamesReader();
            Connection = new FdpOracleConnection();
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

            GetOracleUsersCommand = new DelegateCommand(() => ManageProgress(async o => await GetOracleUsers(), nameof(IsGettingUsers)));
        }

        public FdpOracleConnection Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand GetOracleUsersCommand { get; set; }

        public DelegateCommand GetTnsFileCommand { get; set; }

        public DelegateCommand GetTnsNamesCommand { get; set; }

        public Visibility IsGettingUsers
        {
            get { return _IsGettingUsers; }
            set
            {
                _IsGettingUsers = value;
                RaisePropertyChanged();
            }
        }

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

        public ObservableCollection<string> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                RaisePropertyChanged();
            }
        }

        private async Task GetOracleUsers()
        {
            UsersList = new ObservableCollection<string>();
            using (var conn = Connection.Conn)
            {
                await conn.OpenAsync().ConfigureAwait(false);
                await Task.Run(() => System.Threading.Thread.Sleep(5000));
                var Cmd = new OracleCommand { Connection = conn, CommandType = CommandType.Text,
                    CommandText = "select Username from all_users" };
                using (var dataReader = await Cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (dataReader.Read())
                        UsersList.Add(dataReader.GetString(0));
                }
            }
        }
    }
}