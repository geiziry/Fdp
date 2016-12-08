using Fdp.InfraStructure;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Fdp.DataModeler.DatabaseSchema
{
    public class Schema
    {

        public Schema()
        {

        }
        public Schema(DbCommand Command)
        {
            Tables = new List<Table>();
            SetCommandText(Command);
            GetTables();
        }

        public static DbCommand Command { get; private set; }
        public List<Table> Tables { get; private set; }

        private void SetCommandText(DbCommand command)
        {
            if (command is OracleCommand)
                command.CommandText = Strings.OracleTablesQuery;
            else if (command is SqlCommand)
                command.CommandText = Strings.SqlServerTablesQuery;
            Command = command;
        }


        public void GetTables()
        {
            List<string> TableNames = new List<string>();
            using (DbDataReader dataReader = Command.ExecuteReader())
            {
                while (dataReader.Read())
                    TableNames.Add(dataReader.GetString(0));
            }

            TableNames.ForEach(x => Tables.Add(new Table(x)));
        }

    }



}
