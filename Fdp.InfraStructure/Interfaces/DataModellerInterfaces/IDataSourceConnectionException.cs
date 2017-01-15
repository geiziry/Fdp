using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.InfraStructure.Interfaces.DataModellerInterfaces
{
    public interface IDataSourceConnectionException
    {
        string ConnectionException { get; set; }
        string TextToAppend { get; set; }
    }
}
