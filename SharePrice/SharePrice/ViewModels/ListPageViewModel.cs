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

        public Command UsuarioPageCommand { get; }
        public Command AdicionarOfertaPageCommand { get; }
        public Command NotificacaoCommand { get; }

        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            UsuarioPageCommand = new Command(async () => await ExecuteUsuarioPageCommandAsync());
            AdicionarOfertaPageCommand = new Command(async () => await ExecuteAdicionarOfertaPageCommandAsync());
            NotificacaoCommand = new Command(async () => await ExecuteNotificacaoCommandAsync());
        }

        private Task ExecuteNotificacaoCommandAsync()
        {
            throw new NotImplementedException();
        }

        private async Task ExecuteAdicionarOfertaPageCommandAsync()
        {
            await _navigationService?.NavigateAsync("AdicionarOfertaPage");
        }

        private async Task ExecuteUsuarioPageCommandAsync()
        {
            await _navigationService?.NavigateAsync("MainPage");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
