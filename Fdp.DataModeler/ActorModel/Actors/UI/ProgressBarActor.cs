﻿using Akka.Actor;
using Fdp.DataModeller.ActorModel.Messages;
using Fdp.DataModeller.ViewModels;
using System.Reflection;

namespace Fdp.DataModeller.ActorModel.Actors.UI
{
    public class ProgressBarActor : ReceiveActor
    {
        private readonly ConnectionBaseViewModel _viewModel;
        private PropertyInfo _visibilityProperty;

        public ProgressBarActor(ConnectionBaseViewModel viewModel)
        {
            _viewModel = viewModel;
            Show();
        }

        private void Hide()
        {
            Receive<SetVisibilityPropertyMessage>(message =>
            {
                _visibilityProperty = _viewModel.GetType().GetProperty(message.VisibilityProperty);
                _viewModel.UpdateProgressBarVisibility(_visibilityProperty, false);
                Become(Show);
            });
        }

        private void Show()
        {
            Receive<SetVisibilityPropertyMessage>(message =>
            {
                _visibilityProperty = _viewModel.GetType().GetProperty(message.VisibilityProperty);
                _viewModel.UpdateProgressBarVisibility(_visibilityProperty, true);
                Become(Hide);
            });
        }
    }
}