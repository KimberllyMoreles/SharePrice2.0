using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SharePrice.ViewModels
{
    public class ListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }
    }
}
