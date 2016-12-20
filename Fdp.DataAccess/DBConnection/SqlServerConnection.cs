using Fdp.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class SqlServerConnection : IDbConnection
    {
        public string DataSource { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string InitialCatalog { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                var connection = new SqlConnectionStringBuilder();
                connection.DataSource = DataSource;
                connection.IntegratedSecurity = IntegratedSecurity;
                if (!string.IsNullOrWhiteSpace(InitialCatalog))
                    connection.InitialCatalog = InitialCatalog;
                if (!IntegratedSecurity)
                {
                    connection.UserID = UserName;
                    connection.Password = Password;
                }

                return connection.ToString();
            }
        }

        public event EventHandler<string> ExceptionRaised;
        public string Exception { get; set; }
        public DatabaseType databaseType => DatabaseType.SqlServer;


        public async Task<List<string>> GetLocalNetworkServersAsync()
        {
            var Servers = new List<string>();
            DataTable serversTable = await Task.Run(() => SqlDataSourceEnumerator.Instance.GetDataSources()).ConfigureAwait(false);

            foreach (DataRow row in serversTable.Rows)
            {
                string serverName = row[0].ToString();
                try
                {
                    if (!string.IsNullOrWhiteSpace(row[1].ToString()))
                        serverName += "\\" + row[1].ToString();
                }
                catch (Exception)
                {
                    throw;
                }
                Servers.Add(serverName);
            }

            return Servers;
        }

        public async Task<List<string>> GetDatabaseListAsync()
        {
            var Databases = new List<string>();
            using (var conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    await conn.OpenAsync().ConfigureAwait(false);
                    DataTable DatabasesTable = await Task.Run(() => conn.GetSchema("Databases")).ConfigureAwait(false);

                    foreach (DataRow row in DatabasesTable.Rows)
                    {
                        string DatabaseName = row["database_name"].ToString();
                        Databases.Add(DatabaseName);
                    }
                }
                catch (Exception ex)
                {
                    OnExceptionRaised(ex.Message);
                }
            }
            return Databases;
        }

        protected virtual void OnExceptionRaised(string message)
        {
            (ExceptionRaised as EventHandler<string>)?.Invoke(this, message);
        }
    }
}
