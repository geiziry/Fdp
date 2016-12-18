using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdp.InfraStructure.Interfaces
{
    public interface IFdpDialogService
    {
        UICommand ShowDialog(string Title, Type ViewType, IEnumerable<UICommand> dialogCommands);
    }
}
