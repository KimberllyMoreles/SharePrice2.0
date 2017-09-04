using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using SharePrice.Events;

namespace SharePrice.ViewModels
{
    public class AdicionarOfertaPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private readonly IInputAlertDialogService _inputAlertDialogService;

        public DelegateCommand AdicionarProdutoCommand { get; }
        public DelegateCommand AdicionarGeneroCommand { get; }

        public Command LimparCommand { get; }
        public Command SalvarCommand { get; }

        public AdicionarOfertaPageViewModel(INavigationService navigationService, IInputAlertDialogService inputAlertDialogService)
        {
            _navigationService = navigationService;
            _inputAlertDialogService = inputAlertDialogService;
            
            AdicionarProdutoCommand = new DelegateCommand(ExecuteAdicionarProdutoCommandAsync);
            AdicionarGeneroCommand = new DelegateCommand(ExecuteAdicionarGeneroCommandAsync);

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

        private async void ExecuteAdicionarGeneroCommandAsync()
        {
            var novoGenero = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar tipo", "Livros, Alimentos", "Salvar", "Cancelar", "Insira um nome para este tipo");            
        }

        private async void ExecuteAdicionarProdutoCommandAsync()
        {
            var novoProduto = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar produto", "Impressora HP, Notebook Acer", "Salvar", "Cancelar", "Insira um nome para este produto");
        }
    }
}
