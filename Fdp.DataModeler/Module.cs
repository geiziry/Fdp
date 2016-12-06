using Fdp.DataModeler;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataModeller
{
    public class Module : IModule
    {
        public void Initialize()
        {
            using (var context=new testDbContext())
            {
                var wells = context.Wells.ToList();
                //var metadata= ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
                //var tables = metadata.GetItemCollection(DataSpace.SSpace)
                //    .GetItems<EntityContainer>();
                    //.Where(s => !s.MetadataProperties.Contains("Type")
                    //|| s.MetadataProperties["Type"].ToString() == "Tables");

                //foreach (var table in tables)
                //{
                //    var tableName = table.MetadataProperties.Contains("Table")
                //        && table.MetadataProperties["Table"].Value != null
                //      ? table.MetadataProperties["Table"].Value.ToString()
                //      : table.Name;

                //    var tableSchema = table.MetadataProperties["Schema"].Value.ToString();

                //    Console.WriteLine(tableSchema + "." + tableName);
                //}
                //var tables = objectContext.MetadataWorkspace.GetItems(DataSpace.SSpace)
                //    .Select(t => t).ToList();

            }
        }
    }

}
