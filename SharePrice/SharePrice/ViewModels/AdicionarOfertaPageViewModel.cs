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
using System.Collections.ObjectModel;

namespace SharePrice.ViewModels
{
    public class AdicionarOfertaPageViewModel : BaseViewModel
    {
        //public ObservableCollection<Tipo> Tipos { get; set; }

        public TipoService _tipoService;
        public ProdutoService _produtoService;
       // public AzureService<Oferta> _ofertaService;
        
        private INavigationService _navigationService;
        private readonly IInputAlertDialogService _inputAlertDialogService;

        public DelegateCommand TirarFotoCommand { get; }
        public DelegateCommand SelecionarImagemCommand { get; }

        public DelegateCommand AdicionarProdutoCommand { get; }
        public DelegateCommand AdicionarGeneroCommand { get; }

        public DelegateCommand LimparCommand { get; }
        public DelegateCommand SalvarCommand { get; }

        //declara o campo de imagem da oferta
        public ImageSource imagemOferta;
        public ImageSource ImagemOferta
        {
            get { return this.imagemOferta; }
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

        //declara o entry do produto
        public string produtoEntry;
        public string ProdutoEntry
        {
            get { return this.produtoEntry; }
            set
            {
                if (Equals(value, this.produtoEntry))
                {
                    return;
                }
                this.produtoEntry = value;
                OnPropertyChanged();
            }
        }

        //declara o picker do tipo
        public ObservableCollection<Tipo> tipos;
        public ObservableCollection<Tipo> Tipos
        {
            get { return this.tipos; }
            set
            {
                if (Equals(value, this.tipos))
                {
                    return;
                }
                this.tipos = value;
                OnPropertyChanged();
            }
        }

        //declara o entry do local da oferta
        public string localEntry;
        public string LocalEntry
        {
            get { return this.localEntry; }
            set
            {
                if (Equals(value, this.localEntry))
                {
                    return;
                }
                this.localEntry = value;
                OnPropertyChanged();
            }
        }

        //declara o DatePicker da data da oferta
        public DatePicker dataPicker;
        public DatePicker DataPicker
        {
            get { return this.dataPicker; }
            set
            {
                if (Equals(value, this.dataPicker))
                {
                    return;
                }
                this.dataPicker = value;
                OnPropertyChanged();
            }
        }

        //declara o entry do preço da oferta
        public string precoEntry;
        public string PrecoEntry
        {
            get { return this.precoEntry; }
            set
            {
                if (Equals(value, this.precoEntry))
                {
                    return;
                }
                this.precoEntry = value;
                OnPropertyChanged();
            }
        }


        private bool isBusy;
        public bool IsBusy { get { return isBusy; } set { isBusy = value; OnPropertyChanged(); }}

        public AdicionarOfertaPageViewModel(INavigationService navigationService, IInputAlertDialogService inputAlertDialogService, IDependencyService dependencyService)
        {
            _navigationService = navigationService;
            _inputAlertDialogService = inputAlertDialogService;

            _tipoService = new TipoService();            
            _produtoService = new ProdutoService();
            //_ofertaService = new AzureService<Oferta>();
            
            TirarFotoCommand = new DelegateCommand(ExecuteTirarFotoCommandAsync);
            SelecionarImagemCommand = new DelegateCommand(ExecuteSelecionarImagemCommandAsync);

            AdicionarProdutoCommand = new DelegateCommand(ExecuteAdicionarProdutoCommandAsync);
            AdicionarGeneroCommand = new DelegateCommand(ExecuteAdicionarGeneroCommandAsync);

            LimparCommand = new DelegateCommand(ExecuteLimparCommand);
            SalvarCommand = new DelegateCommand(ExecuteSalvarCommandAsync);

            Tipos = new ObservableCollection<Tipo>();
            LoadTipos();

            IsBusy = false;
            
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

        public async void LoadTipos()
        {
            var result = await _tipoService.GetTipos();

            Tipos.Clear();

            if (result.Count() != 0)
            {
                foreach (var item in result)
                {
                    Tipos.Add(item);
                }
            }

            IsBusy = false;
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
           
        }

        private void ExecuteLimparCommand()
        {
            ProdutoEntry = "";
            LocalEntry = "";
            PrecoEntry = "";            
        }

        private async void ExecuteAdicionarGeneroCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var novoTipo = new Tipo()
            {
                Nome = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(                
                "Adicionar tipo", "Livros, Alimentos", "Salvar", "Cancelar", "Insira um nome para este tipo")
            };

            _tipoService.AddContact(novoTipo);           

            IsBusy = false;
        }

        private async void ExecuteAdicionarProdutoCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var novoProduto = new Produto()
            {
                TipoId = "61f6b175b4424a528ddc7b6f298a12fa",
                Nome = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar Produto", "Impressora, Notebook", "Salvar", "Cancelar", "Insira um nome para este produto")
            };

            _produtoService.AddContact(novoProduto);
            
            IsBusy = false;
        }
    }
}
