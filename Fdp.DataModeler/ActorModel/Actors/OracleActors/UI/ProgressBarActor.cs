using Akka.Actor;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.DataModeller.ViewModels;
using System.Reflection;

namespace Fdp.DataModeller.ActorModel.Actors.OracleActors.UI
{
    public class ProgressBarActor : ReceiveActor
    {
        private readonly OracleConnectionViewModel _viewModel;
        private PropertyInfo _visibilityProperty;

        public ProgressBarActor(OracleConnectionViewModel viewModel)
        {
            _viewModel = viewModel;

            Receive<SetVisibilityPropertyMessage>(message => 
                    SetVisibilityProperty(message.VisibilityProperty));
        }

        private void SetVisibilityProperty(string visibilityProperty)
        {
            _visibilityProperty = _viewModel.GetType().GetProperty(visibilityProperty);
        }

        private void Hide()
        {
            _viewModel.UpdateProgressBarVisibility(_visibilityProperty, false);
            Become(Show);
        }

        private void Show()
        {
            _viewModel.UpdateProgressBarVisibility(_visibilityProperty, true);
            Become(Hide);
        }
    }
}