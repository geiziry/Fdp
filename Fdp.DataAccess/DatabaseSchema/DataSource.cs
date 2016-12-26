using Fdp.InfraStructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class DataSource
    {
        public DataSource(DbCommand Command)
        {
            Tables = new List<Table>();
            SetCommandText(Command);
        }

        public static DbCommand DbEnumerationCommand { get; private set; }
        public List<Table> Tables { get; }

        public async Task<DataSource> GetSchema()
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

        private void SetCommandText(DbCommand command)
        {
            if (command is OracleCommand)
                command.CommandText = Strings.OracleTablesQuery;
            else if (command is SqlCommand)
                command.CommandText = Strings.SqlServerTablesQuery;
            DbEnumerationCommand = command;
        }
    }
}