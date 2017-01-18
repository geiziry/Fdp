using System.Collections.ObjectModel;

namespace Fdp.DataModeller.ActorModel.Messages
{
    public class SetTnsNamesMessage
    {
        public SetTnsNamesMessage(ObservableCollection<string> tnsNames)
        {
            TnsNames = tnsNames;
        }

        public ObservableCollection<string> TnsNames { get; private set; }
    }
}