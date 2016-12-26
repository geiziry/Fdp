using DevExpress.Mvvm;
using Fdp.DataAccess.Enums;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class OracleConnection : BindableBase, IDbConnection
    {
        private string _dataSource;
        private string _hostID;
        private string _port;
        private string _sID;

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

        public string DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                if (!string.IsNullOrEmpty(_dataSource))
                {
                    HostID = GetDataSourceDetail("HOST");
                    Port = GetDataSourceDetail("PORT");
                    SID = GetDataSourceDetail("SID") ?? GetDataSourceDetail("SERVICE_NAME");
                }
            }
        }

        public string HostID
        {
            get { return _hostID; }
            set
            {
                _hostID = value;
                RaisePropertyChanged();
            }
        }

        public string Password { get; set; }

        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }

        public string SID
        {
            get { return _sID; }
            set
            {
                _sID = value;
                RaisePropertyChanged();
            }
        }

        public string UserName { get; set; }

        private string GetDataSourceDetail(string parameter)
        {
            string pattern = $@"(?<=\b{parameter}\s?=\s+?)(?i)[0-9aA-Z_\.]*(?=\)?)";
            MatchCollection matchCollection = Regex.Matches(DataSource, pattern);
            if (matchCollection.Count > 0)
                return matchCollection[0].ToString();
            return null;
        }
    }
}