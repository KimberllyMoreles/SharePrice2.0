using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace SharePrice.ViewModels
{
    public class AdicionarOfertaPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public Command AdicionarProdutoCommand { get; }
        public Command AdicionarGeneroCommand { get; }
        public Command LimparCommand { get; }
        public Command SalvarCommand { get; }

        public AdicionarOfertaPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AdicionarProdutoCommand = new Command(async () => await ExecuteAdicionarProdutoCommandAsync());
            AdicionarGeneroCommand = new Command(async () => await ExecuteAdicionarGeneroCommandAsync());
            LimparCommand = new Command(async () => await ExecuteLimparCommandAsync());
            SalvarCommand = new Command(async () => await ExecuteSalvarCommandAsync());
        }

        private Task ExecuteSalvarCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteLimparCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteAdicionarGeneroCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteAdicionarProdutoCommandAsync()
        {
            throw new NotImplementedException();
        }
    }
}
