using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.InfraStructure
{
    public static class Strings
    {
        //Modules
        public static string ControlsModule = "ControlsModule";
        public const string EssentialsModule = "EssentialsModule";
        public const string DataModellerModule = "DataModellerModule";

        //Sell Regions
        public const string MenuRegion = "MenuRegion";
        public const string MainRegion = "MainRegion";

        //DataModeller Regions
        public const string DataModellingRegion = "DataModellingRegion";
        public const string DataSourcesRegion = "DataSourcesRegion";
        public const string AddVariablesRegion = "AddVariablesRegion";
        public const string DefineVariablesRegion = "DefineVariablesRegion";
        public const string DataSourceConnectionRegion = "DataSourceConnectionRegion";


        //Unknown Database Enumeration queries
        public const string SqlServerTablesQuery = "Select TABLE_NAME from INFORMATION_SCHEMA.TABLES";
        public const string SqlServerColumnsQuery = "select COLUMN_NAME,DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='{0}'";
        public const string OracleTablesQuery = "SELECT table_name from user_tables";
        public const string OracleColumnsQuery = "select COLUMN_NAME,DATA_TYPE from ALL_TAB_COLUMNS where TABLE_NAME = '{0}'";
    }
}
