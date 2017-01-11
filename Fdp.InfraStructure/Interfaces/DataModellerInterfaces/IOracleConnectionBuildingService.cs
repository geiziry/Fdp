using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Fdp.InfraStructure.Interfaces.DataModellerInterfaces
{
    public interface IOracleConnectionBuildingService
    {
        string GetDataSource(string _Tns);

        Task<ObservableCollection<string>> GetOracleUsers(OracleConnection connection);

        ObservableCollection<string> GetTnsNames();

        ObservableCollection<string> GetTnsNamesFromFile();
    }
}