﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using SharePrice.Events;

namespace SharePrice.ViewModels
{
    public class InitialPageViewModel : BaseViewModel
    {
        IEventAggregator _ea { get; }

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { SetProperty(ref _titulo, value); }
        }

        public InitialPageViewModel(IEventAggregator eventAggregator)
        {
            _ea = eventAggregator;
        }

        public override void OnNavigatingTo(Prism.Navigation.NavigationParameters parameters)
        {
            
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Publish(parameters);

            /*if (parameters.ContainsKey("parametro"))
                this.Titulo = parameters["parametro"].ToString();*/
        }
    }
}
