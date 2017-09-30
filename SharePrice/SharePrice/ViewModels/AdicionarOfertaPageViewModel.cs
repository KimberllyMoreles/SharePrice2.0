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
        private TipoService _azureServiceTipo;
        
        private INavigationService _navigationService;
        private readonly IInputAlertDialogService _inputAlertDialogService;

        public DelegateCommand TirarFotoCommand { get; }
        public DelegateCommand SelecionarImagemCommand { get; }

        public DelegateCommand AdicionarProdutoCommand { get; }
        public DelegateCommand AdicionarGeneroCommand { get; }

        public DelegateCommand LimparCommand { get; }
        public DelegateCommand SalvarCommand { get; }

        public ImageSource imagemOferta;
        public ImageSource ImagemOferta
        {
            get
            {
                return this.imagemOferta;
            }
            set
            {
                if (Equals(value, this.imagemOferta))
                {
                    return;
                }
                this.imagemOferta = value;
                OnPropertyChanged();
            }
        }

       /* private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }*/

        public AdicionarOfertaPageViewModel(INavigationService navigationService, IInputAlertDialogService inputAlertDialogService, IDependencyService dependencyService)
        {
            _navigationService = navigationService;
            _inputAlertDialogService = inputAlertDialogService;
            _azureServiceTipo = dependencyService.Get<TipoService>();
            
            TirarFotoCommand = new DelegateCommand(ExecuteTirarFotoCommandAsync);
            SelecionarImagemCommand = new DelegateCommand(ExecuteSelecionarImagemCommandAsync);

            AdicionarProdutoCommand = new DelegateCommand(ExecuteAdicionarProdutoCommandAsync);
            AdicionarGeneroCommand = new DelegateCommand(ExecuteAdicionarGeneroCommandAsync);

            LimparCommand = new DelegateCommand(ExecuteLimparCommandAsync);
            SalvarCommand = new DelegateCommand(ExecuteSalvarCommandAsync);

            //IsBusy = false;
            
        }

        private async void ExecuteSelecionarImagemCommandAsync()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var foto = await CrossMedia.Current.PickPhotoAsync();
                ImagemOferta = ImageSource.FromStream(() =>
                {
                    var stream = foto.GetStream();
                    foto.Dispose();
                    return stream;
                });
            }

        }

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

                ImagemOferta = ImageSource.FromStream(() =>
                {
                    var stream = foto.GetStream();
                    foto.Dispose();
                    return stream;
                });
             }
        }

        private async void ExecuteSalvarCommandAsync()
        {
            throw new NotImplementedException();
        }

        private async void ExecuteLimparCommandAsync()
        {
            var tipos = await _azureServiceTipo.GetTipos();
            var num = tipos.Count();
            var aaa = tipos.ToList();
        }

        private async void ExecuteAdicionarGeneroCommandAsync()
        {
            var novoTipo = new Tipo()
            {
                Nome = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(                
                "Adicionar tipo", "Livros, Alimentos", "Salvar", "Cancelar", "Insira um nome para este tipo")
            };
            _azureServiceTipo.AddTipo(novoTipo);
        }

        private async void ExecuteAdicionarProdutoCommandAsync()
        {
            var novoProduto = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar produto", "Impressora HP, Notebook Acer", "Salvar", "Cancelar", "Insira um nome para este produto");
        }
    }
}
