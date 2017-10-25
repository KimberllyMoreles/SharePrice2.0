using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePrice.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public UserPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
