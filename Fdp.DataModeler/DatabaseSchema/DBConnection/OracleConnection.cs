using Fdp.DataModeler.Enums;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataModeler.DatabaseSchema
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
                OracleConnectionStringBuilder connection = new OracleConnectionStringBuilder();
                connection.DataSource = string.Format("(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})))", HostID, Port, SID);
                connection.UserID = UserName;
                connection.Password = Password;
                return connection.ToString();
            }
        }

        public DatabaseType databaseType
        {
            get
            {
                return DatabaseType.Oracle;
            }
        }
    }
}
