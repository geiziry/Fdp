using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Fdp.InfraStructure;

namespace Fdp.DataAccess.DatabaseSchema
{
    public class Table
    {

        public Table(string TableName)
        {
            this.TableName = TableName;
            Columns = new List<Column>();
            SetCommandText();
            GetColumns();
        }

        private void SetCommandText()
        {
            if (DataSource.DbEnumerationCommand is OracleCommand)
                DataSource.DbEnumerationCommand.CommandText = string.Format(Strings.OracleColumnsQuery, TableName);
            else if (DataSource.DbEnumerationCommand is SqlCommand)
                DataSource.DbEnumerationCommand.CommandText = string.Format(Strings.SqlServerColumnsQuery, TableName);
        }

        public string TableName { get; set; }
        public List<Column> Columns { get; set; }

        private void GetColumns()
        {
            using (DbDataReader dataReader = DataSource.DbEnumerationCommand.ExecuteReader())
            {
                while (dataReader.Read())
                    Columns.Add(new Column { ColumnName = dataReader.GetString(0), ColumnType = dataReader.GetString(1) });
            }
        }


    }
}
