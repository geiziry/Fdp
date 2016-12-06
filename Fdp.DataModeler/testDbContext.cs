using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration;

namespace Fdp.DataModeler
{
    [DbConfigurationType(typeof(OracleDbConfiguration))]
    public class testDbContext:DbContext
    {
        public DbSet<Well> Wells { get; set; }

        public testDbContext() : base(new OracleConnection(String.Format("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})));User Id={3};Password={4};", "localhost", "1521", "orcl", "NK_Phase2", "seawater")),true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("NK_PHASE2");
            var configurationRegistrar = modelBuilder.Configurations;
            new GeneralEntitiesConfiguration(configurationRegistrar);
        }
    }

    internal class GeneralEntitiesConfiguration
    {

        public GeneralEntitiesConfiguration(ConfigurationRegistrar configurationRegistrar)
        {
             configurationRegistrar.Add(new YourEntityConfiguration());
        }
    }

    internal class YourEntityConfiguration : EntityTypeConfiguration<Well>
    {
        public YourEntityConfiguration()
        {
            ToTable("WMASTER");
            Property(entity => entity.Id).HasColumnName("FINDER_S");
            Property(entity => entity.WellName).HasColumnName("UWI");
        }
    }

    public class OracleDbConfiguration : DbConfiguration
    {
        public OracleDbConfiguration()
        {
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
            SetProviderFactory("Oracle.ManagedDataAccess.Client", OracleClientFactory.Instance);
        }
    }

    

}
