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
        public OfertaService _ofertaService;
        
        private INavigationService _navigationService;
        private readonly IInputAlertDialogService _inputAlertDialogService;

        public DelegateCommand TirarFotoCommand { get; }
        public DelegateCommand SelecionarImagemCommand { get; }

        public DelegateCommand AdicionarProdutoCommand { get; }
        public DelegateCommand AdicionarTipoCommand { get; }

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

        //declara o picker do produto
        public ObservableCollection<Produto> produtos;
        public ObservableCollection<Produto> Produtos
        {
            get { return this.produtos; }
            set
            {
                if (Equals(value, this.produtos))
                {
                    return;
                }
                this.produtos = value;
                OnPropertyChanged();
            }
        }

        //declara o entry do preço da oferta
        public int indexProduto;
        public int IndexProduto
        {
            get { return this.indexProduto; }
            set
            {
                if (Equals(value, this.indexProduto))
                {
                    return;
                }
                this.indexProduto = value;
                OnPropertyChanged();
            }
        }

        //declara o picker do tipo;
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

        //declara o entry do preço da oferta
        public int indexTipo;
        public int IndexTipo
        {
            get { return this.indexTipo; }
            set
            {
                if (Equals(value, this.indexTipo))
                {
                    return;
                }
                this.indexTipo = value;
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
        public DateTime dataInicioPicker;
        public DateTime DataInicioPicker
        {
            get { return this.dataInicioPicker; }
            set
            {
                if (Equals(value, this.dataInicioPicker))
                {
                    return;
                }
                this.dataInicioPicker = value;
                OnPropertyChanged();
            }
        }

        //declara o entry do preço da oferta
        public double precoEntry;
        public double PrecoEntry
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

        //declara o entry do preço da oferta
        public bool destaqueSwitch;
        public bool DestaqueSwitch
        {
            get { return this.destaqueSwitch; }
            set
            {
                if (Equals(value, this.destaqueSwitch))
                {
                    return;
                }
                this.destaqueSwitch = value;
                OnPropertyChanged();
            }
        }

        //declara o DatePicker da data da oferta
        public DateTime dataFimPicker;
        public DateTime DataFimPicker
        {
            get { return this.dataFimPicker; }
            set
            {
                if (Equals(value, this.dataFimPicker))
                {
                    return;
                }
                this.dataFimPicker = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public AdicionarOfertaPageViewModel(INavigationService navigationService, IInputAlertDialogService inputAlertDialogService, IDependencyService dependencyService)
        {
            _navigationService = navigationService;
            _inputAlertDialogService = inputAlertDialogService;

            _tipoService = new TipoService();            
            _produtoService = new ProdutoService();
            _ofertaService = new OfertaService();

            TirarFotoCommand = new DelegateCommand(ExecuteTirarFotoCommandAsync);
            SelecionarImagemCommand = new DelegateCommand(ExecuteSelecionarImagemCommandAsync);

            AdicionarProdutoCommand = new DelegateCommand(ExecuteAdicionarProdutoCommandAsync);
            AdicionarTipoCommand = new DelegateCommand(ExecuteAdicionarTipoCommandAsync);

            LimparCommand = new DelegateCommand(ExecuteLimparCommand);
            SalvarCommand = new DelegateCommand(ExecuteSalvarCommandAsync);

            Tipos = new ObservableCollection<Tipo>();
            Produtos = new ObservableCollection<Produto>();

            Sincroniza();
                        
            LoadTipos();
            LoadProdutos();

            DataInicioPicker = DateTime.Today;
            DataFimPicker = DateTime.Today;

            DestaqueSwitch = false;

            IsBusy = false;
            
        }
        private async void Sincroniza()
        {
            await _tipoService.SyncAsync();
            await _produtoService.SyncAsync();
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
            
            foreach (var item in result)
            {
                Tipos.Add(item);
            }

            IsBusy = false;
        }

        public async void LoadProdutos()
        {
            var result = await _produtoService.GetProdutos();

            Produtos.Clear();

            foreach (var item in result)
            {
                Produtos.Add(item);
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
            //Tratar usuário...
            _ofertaService.AddContact(new Oferta()
            {
                ProdutoId = Produtos[IndexProduto].Id,
                Preco = PrecoEntry,
                DataInicio = DataInicioPicker,
                DataFim = DataFimPicker,
                Destaque = DestaqueSwitch,
                Local = LocalEntry
            });
        }

        private void ExecuteLimparCommand()
        {
            LocalEntry = "";
            PrecoEntry = 0;
            DataInicioPicker = DateTime.Today;
            DestaqueSwitch = false;
            DataFimPicker = DateTime.Today;
        }

        private async void ExecuteAdicionarTipoCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var novoTipo = new Tipo()
            {
                NomeT = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(                
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
                TipoId = Tipos[IndexTipo].Id,
                NomeP = await _inputAlertDialogService.OpenCancellableTextInputAlertDialog(
                "Adicionar Produto", "Impressora, Notebook", "Salvar", "Cancelar", "Insira um nome para este produto")
            };

            _produtoService.AddContact(novoProduto);
            
            IsBusy = false;
        }
    }
}
