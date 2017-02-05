using Fdp.DataAccess.DatabaseSchema;
using Fdp.InfraStructure.Interfaces.DataModellerInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataModeller.Services
{
    public class SqlServerConnectionBuildingService : ISqlServerConnectionBuildingService
    {
        public ObservableCollection<string> GetLocalNetworkServersAsync()
        {
            var Servers = new ObservableCollection<string>();
            DataTable serversTable = SqlDataSourceEnumerator.Instance.GetDataSources();

            foreach (DataRow row in serversTable.Rows)
            {
                string serverName = row[0].ToString();
                try
                {
                    if (!string.IsNullOrWhiteSpace(row[1].ToString()))
                        serverName += "\\" + row[1];
                }
                catch (Exception)
                {
                    throw;
                }
                Servers.Add(serverName);
            }

            return Servers;
        }
        private void GetListOfDataBasesAsync(ObservableCollection<string> Databases, SqlConnection conn)
        {
            DataTable DatabasesTable = conn.GetSchema("Databases");

            foreach (DataRow row in DatabasesTable.Rows)
            {
                string DatabaseName = row["database_name"].ToString();
                Databases.Add(DatabaseName);
            }
        }

        public async Task<ObservableCollection<string>> GetDatabaseListAsync(SqlConnection connection)
        {
            var Databases = new ObservableCollection<string>();
            try
            {
                using (var conn = new SqlConnection(connection.ConnectionString))
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    GetListOfDataBasesAsync(Databases, conn);
                }
            }
            catch (Exception exception)
            {
                var message = exception.InnerException == null ? exception.Message
                               : exception.InnerException.Message;
                throw new ArgumentException(message);
            }
            return Databases;
        }


    }
}
