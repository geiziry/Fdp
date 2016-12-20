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

        public string ConnectionString
        {
            get
            {
                var connection = new OracleConnectionStringBuilder();
                connection.DataSource = $"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={HostID})(PORT={Port}))(CONNECT_DATA=(SID={SID})))";
                connection.UserID = UserName;
                connection.Password = Password;
                return connection.ToString();
            }
        }

        public DatabaseType databaseType => DatabaseType.Oracle;
    }
}
