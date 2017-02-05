using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Fdp.InfraStructure.Interfaces.DataModellerInterfaces
{
    public interface ISqlServerConnectionBuildingService
    {
        Task<ObservableCollection<string>> GetDatabaseListAsync(SqlConnection connection);
        ObservableCollection<string> GetLocalNetworkServersAsync();
    }
}