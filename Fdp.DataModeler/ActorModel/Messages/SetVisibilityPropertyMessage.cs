namespace Fdp.DataModeller.ActorModel.Messages
{
    public class SetVisibilityPropertyMessage
    {
        public SetVisibilityPropertyMessage(string visibilityProperty)
        {
            VisibilityProperty = visibilityProperty;
        }

        public string VisibilityProperty { get; private set; }
    }
}