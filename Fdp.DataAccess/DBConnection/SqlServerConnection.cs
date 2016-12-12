using Fdp.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class SqlServerConnection : IDbConnection
    {
        public string DataSource { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder();
                connection.DataSource = DataSource;
                connection.IntegratedSecurity = IntegratedSecurity;
                if (!IntegratedSecurity)
                {
                    connection.UserID = UserName;
                    connection.Password = Password;
                }

                return connection.ToString();
            }
        }

        public DatabaseType databaseType
        {
            get
            {
                return DatabaseType.SqlServer;
            }

        }

        public List<string> GetLocalNetworkServers()
        {
            List<string> Servers = new List<string>();
            DataTable serversTable = SqlDataSourceEnumerator.Instance.GetDataSources();

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

        public List<string> GetDatabaseList(string server)
        {
            List<string> Databases = new List<string>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            DataTable DatabasesTable = conn.GetSchema("Databases");
            conn.Close();

            foreach (DataRow row in DatabasesTable.Rows)
            {
                string DatabaseName = row["database_name"].ToString();
                Databases.Add(DatabaseName);
            }
            return Databases;

        }

    }
}
