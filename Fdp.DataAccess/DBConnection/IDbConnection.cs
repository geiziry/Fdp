using Fdp.DataAccess.Enums;

namespace Fdp.DataAccess.DatabaseSchema
{
    public interface IDbConnection
    {
        string ConnectionString { get; }
        DatabaseType databaseType { get;}
    }
}
