using Fdp.DataAccess.DBConnection;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fdp.DataModeller.Services
{
    public class OracleConnectionBuildingService : IOracleConnectionBuildingService
    {
        private TNSNamesReader TnsNamesReader;

        public OracleConnectionBuildingService()
        {
            TnsNamesReader = new TNSNamesReader();
        }

        public string GetDataSource(string _Tns) => TnsNamesReader.GetDataSource(_Tns);

        public async Task<ObservableCollection<string>> GetOracleUsers(OracleConnection connection)
        {
            var UsersList = new ObservableCollection<string>();
            using (connection)
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var Cmd = new OracleCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "select Username from all_users"
                };
                using (var dataReader = await Cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (dataReader.Read())
                        UsersList.Add(dataReader.GetString(0));
                }
            }
            return UsersList;
        }

        public ObservableCollection<string> GetTnsNames()
        {
            var Tns = TnsNamesReader.GetOracleHomes();
            TnsNamesReader.SetTnsFileText(Tns.FirstOrDefault(), true);
            return new ObservableCollection<string>(TnsNamesReader.LoadTnsNames());
        }

        public ObservableCollection<string> GetTnsNamesFromFile()
        {
            FileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                TnsNamesReader.SetTnsFileText(dialog.FileName);
                return new ObservableCollection<string>(TnsNamesReader.LoadTnsNames());
            }
            return null;
        }
    }
}