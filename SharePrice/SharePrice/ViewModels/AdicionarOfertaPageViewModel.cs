using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using SharePrice.Events;
using SharePrice.Models;
using SharePrice.Service;
using Prism.Services;
using Plugin.Media;

namespace SharePrice.ViewModels
{
    public class AdicionarOfertaPageViewModel : BaseViewModel
    {
        private AzureServiceTipo _azureServiceTipo;

        private INavigationService _navigationService;
        private readonly IInputAlertDialogService _inputAlertDialogService;

        public DelegateCommand TirarFotoCommand { get; }
        public DelegateCommand SelecionarImagemCommand { get; }

        public DelegateCommand AdicionarProdutoCommand { get; }
        public DelegateCommand AdicionarGeneroCommand { get; }

        public DelegateCommand LimparCommand { get; }
        public DelegateCommand SalvarCommand { get; }

        public string ImagemOferta;

        public AdicionarOfertaPageViewModel(INavigationService navigationService, IInputAlertDialogService inputAlertDialogService, IDependencyService dependencyService)
        {
            _navigationService = navigationService;
            _inputAlertDialogService = inputAlertDialogService;
            _azureServiceTipo = dependencyService.Get<AzureServiceTipo>();

            TirarFotoCommand = new DelegateCommand(ExecuteTirarFotoCommandAsync);
            SelecionarImagemCommand = new DelegateCommand(ExecuteSelecionarImagemCommandAsync);

            AdicionarProdutoCommand = new DelegateCommand(ExecuteAdicionarProdutoCommandAsync);
            AdicionarGeneroCommand = new DelegateCommand(ExecuteAdicionarGeneroCommandAsync);

            LimparCommand = new DelegateCommand(ExecuteLimparCommandAsync);
            SalvarCommand = new DelegateCommand(ExecuteSalvarCommandAsync);
        }

        private async void ExecuteSelecionarImagemCommandAsync()
        {
            /*if (CrossMedia.Current.IsPickPhotoSupported)
                var photo = await CrossMedia.Current.PickPhotoAsync();
       */ }

        private async void ExecuteTirarFotoCommandAsync()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Pictures",
                    Name = $"{DateTime.UtcNow}.jpg"
                };

                // Take a photo of the business receipt.
                var foto = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                if (foto == null)
                    return;
                
                var resource = ImageSource.FromResource(foto.AlbumPath);
                ImagemOferta = resource.ToString();
            }
        }

        private async void ExecuteSalvarCommandAsync()
        {
            throw new NotImplementedException();
        }

        private async void ExecuteLimparCommandAsync()
        {
            throw new NotImplementedException();
        }

        private async void ExecuteAdicionarGeneroCommandAsync()
        {
            var novoTipo = new Tipo();

            string tipo = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar tipo", "Livros, Alimentos", "Salvar", "Cancelar", "Insira um nome para este tipo");

            novoTipo.Nome = tipo.ToString();

            _azureServiceTipo.AddTipo(novoTipo);
        }

        private async void ExecuteAdicionarProdutoCommandAsync()
        {
            var novoProduto = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar produto", "Impressora HP, Notebook Acer", "Salvar", "Cancelar", "Insira um nome para este produto");
        }
    }
}
