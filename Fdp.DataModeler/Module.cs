using Fdp.DataModeler.DatabaseSchema;
using Oracle.ManagedDataAccess.Client;
using Prism.Modularity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Fdp.DataModeller
{
    public class Module : IModule
    {
        public void Initialize()
        {
            //using (var context = new testDbContext())
            //{
            //    var wells = context.Wells.ToList();
            //    var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            //    var tables = metadata.GetItemCollection(DataSpace.SSpace)
            //        .GetItems<EntityContainer>()//;
            //        .Where(s => !s.MetadataProperties.Contains("Type")
            //        || s.MetadataProperties["Type"].ToString() == "Tables");


            //    //foreach (var table in tables)
            //    //{
            //    //    var tableName = table.MetadataProperties.Contains("Table")
            //    //        && table.MetadataProperties["Table"].Value != null
            //    //      ? table.MetadataProperties["Table"].Value.ToString()
            //    //      : table.Name;

            //    //    var tableSchema = table.MetadataProperties["Schema"].Value.ToString();

            //    //    Console.WriteLine(tableSchema + "." + tableName);
            //    //}
            //    //var tables = objectContext.MetadataWorkspace.GetItems(DataSpace.SSpace)
            //    //    .Select(t => t).ToList();

            //}

            //using (var conn = new Oracle.ManagedDataAccess.Client.OracleConnection(string.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})));User Id={3};Password={4};", "localhost", "1521", "orcl", "NK_Phase2", "seawater")))
            //{
            //    conn.Open();


            //    OracleCommand cmd = new OracleCommand{Connection = conn,CommandType = CommandType.Text};

            //    DataSource schema = new DataSource(cmd);
            //}

            //using (var conn1 = new SqlConnection("Data Source=MGEIZIRY;Initial Catalog=NK_Phase2;Integrated Security=True"))
            //{
            //    conn1.Open();

            //    SqlCommand cmd = new SqlCommand { Connection = conn1, CommandType = CommandType.Text };
            //    DataSource schema = new DataSource(cmd);

            //}

             // var types=  
                   // Tables.Values.SelectMany(f => f, (p, c) => new { c.ColumnType }).Distinct().ForEach(x=>Debug.WriteLine(x));

               //Debug.WriteLine(types);
            }

            
        }
    }



