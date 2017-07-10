using Prism.Commands;
using Prism.Mvvm;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using SharePrice.Events;

namespace SharePrice.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        IEventAggregator _ea { get; }
        public LoginPageViewModel(IEventAggregator eventAggregator)
        {
            _ea = eventAggregator;
            Title = "Event Initialized";
        }

        public override void OnNavigatingTo(Prism.Navigation.NavigationParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine($"{Title} OnNavigatingTo");
            _ea.GetEvent<InitializeTabbedChildrenEvent>().Publish(parameters);
        }
        
    }
}
