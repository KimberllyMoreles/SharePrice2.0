using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using SharePrice.Helpers;
using Prism.Navigation;
using SharePrice.Service;
using Prism.Services;

namespace SharePrice.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        LoginService _azureService;

        public Command LoginFacebookCommand { get; }
        public Command LoginGoogleCommand { get; }

        public LoginPageViewModel(INavigationService navigationService, IDependencyService dependencyService)
        {
            _navigationService = navigationService;
            _azureService = dependencyService.Get<LoginService>();

            LoginFacebookCommand = new Command(async () => await ExecuteLoginFacebookCommandAsync());
            LoginGoogleCommand = new Command(async () => await ExecuteLoginGoogleCommandAsync());
        }

        private async Task ExecuteLoginFacebookCommandAsync()
        {
            if (IsBusy || !(await LoginAsync("Facebook")))
                return;
            else
            {
                await _navigationService?.NavigateAsync("MainPage");
            }

        }

        private async Task ExecuteLoginGoogleCommandAsync()
        {
            if (IsBusy || !(await LoginAsync("Google")))
                return;
            else
            {
                await _navigationService?.NavigateAsync("MainPage");
            }

        }

        public Task<bool> LoginAsync(string redeSocial)
        {
            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return _azureService.LoginAsync(redeSocial);
        }
    }
}
