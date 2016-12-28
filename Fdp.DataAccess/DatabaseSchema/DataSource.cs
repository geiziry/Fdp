using Fdp.DataAccess.Enums;
using Fdp.InfraStructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class DataSource
    {
        public DataSource(IDbConnection Connection)
        {
            InstantiateCommand(Connection);
            Tables = new List<Table>();
        }

        public static DbCommand DbEnumerationCommand { get; private set; }

        public List<Table> Tables { get; }

        public async Task<DataSource> GetSchemaAsync()
        {
            var TableNames = new List<string>();
            using (DbDataReader dataReader = await DbEnumerationCommand.ExecuteReaderAsync().ConfigureAwait(false))
            {
                while (dataReader.Read())
                    TableNames.Add(dataReader.GetString(0));
            }

            TableNames.ForEach(x => Tables.Add(new Table(x)));
            return this;
        }

        private void InstantiateCommand(IDbConnection connection)
        {
            switch (connection.databaseType)
            {
                case DatabaseType.Oracle:
                    DbEnumerationCommand = new OracleCommand
                    {
                        Connection = (connection as FdpOracleConnection).Conn,
                        CommandType = CommandType.Text,
                        CommandText = Strings.OracleTablesQuery
                    };
                    break;

                case DatabaseType.SqlServer:
                    DbEnumerationCommand = new SqlCommand
                    {
                        Connection = (connection as FdpSqlConnection).Conn,
                        CommandType = CommandType.Text,
                        CommandText = Strings.SqlServerTablesQuery
                    };
                    break;

                default:
                    break;
            }
        }
    }
}