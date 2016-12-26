using Fdp.DataAccess.Enums;
using Oracle.ManagedDataAccess.Client;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class OracleConnection : IDbConnection
    {
        public string HostID { get; set; }
        public string Port { get; set; }
        public string SID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string DataSource { get; set; }

        public string ConnectionString
        {
            get
            {
                var connection = new OracleConnectionStringBuilder();
                if (string.IsNullOrWhiteSpace(DataSource))
                    connection.DataSource = $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={HostID})(PORT={Port}))(CONNECT_DATA=(SID={SID})))";
                else
                    connection.DataSource = DataSource;
                connection.UserID = UserName;
                connection.Password = Password;
                return connection.ToString();
            }
        }

        public DatabaseType databaseType => DatabaseType.Oracle;


    }
}
