using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using Prism.Events;
using SharePrice.Events;
using SharePrice.Helpers;

namespace SharePrice.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        IEventAggregator _ea { get; }
        private INavigationService _navigationService;

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { SetProperty(ref _titulo, value); }
        }


        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _ea = eventAggregator;
            _navigationService = navigationService;

            /*if (!Settings.IsLoggedIn)
                _navigationService?.NavigateAsync("InitialPage");*/

        }

        public override void OnNavigatingTo(Prism.Navigation.NavigationParameters parameters)
        {

            _ea.GetEvent<InitializeTabbedChildrenEvent>().Publish(parameters);

            /*if (parameters.ContainsKey("parametro"))
                this.Titulo = parameters["parametro"].ToString();*/
        }


    }
}
