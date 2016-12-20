using Fdp.InfraStructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class DataSource
    {
        public DataSource()
        {
        }

        public DataSource(DbCommand Command)
        {
            Tables = new List<Table>();
            SetCommandText(Command);
            GetSchema();
        }

        public static DbCommand DbEnumerationCommand { get; private set; }
        public List<Table> Tables { get; }

        private void SetCommandText(DbCommand command)
        {
            if (command is OracleCommand)
                command.CommandText = Strings.OracleTablesQuery;
            else if (command is SqlCommand)
                command.CommandText = Strings.SqlServerTablesQuery;
            DbEnumerationCommand = command;
        }

        public void GetSchema()
        {
            var TableNames = new List<string>();
            using (DbDataReader dataReader = DbEnumerationCommand.ExecuteReader())
            {
                while (dataReader.Read())
                    TableNames.Add(dataReader.GetString(0));
            }

            TableNames.ForEach(x => Tables.Add(new Table(x)));
        }
    }
}
