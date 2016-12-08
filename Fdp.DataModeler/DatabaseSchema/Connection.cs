using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataModeler.DatabaseSchema
{
    public class Connection
    {
        public string ConnectionString { get; set; }
        public DatabaseType databaseType { get; set; }

        public string Host { get; set; }
        public string Port { get; set; }
        public string SID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public enum DatabaseType
    {
        Oracle,
        SqlServer
    }

    public class SqlServerConnection
    {

        public List<string> GetNetworkServers()
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
            SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder();
            connection.DataSource = server;
            connection.IntegratedSecurity = true;
            string ConnectionString = connection.ToString();

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
