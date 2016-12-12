﻿using Fdp.DataAccess.Enums;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.DataAccess.DatabaseSchema
{
    public interface IDbConnection
    {
        string ConnectionString { get; }
        DatabaseType databaseType { get;}
    }



}
