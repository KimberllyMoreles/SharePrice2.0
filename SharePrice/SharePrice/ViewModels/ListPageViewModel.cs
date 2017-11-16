using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SharePrice.Models;
using SharePrice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharePrice.ViewModels
{
    public class ListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        private OfertaService _ofertaService;
        private ProdutoService _produtoService;

        public ObservableCollection<Oferta> Ofertas { get; set; }
        public ObservableCollection<Oferta> OfertasGraph { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<ProdutoOfertas> ProdutoOfertas { get; set; }

        public DelegateCommand RefreshCommand { get; set; }

        public ICommand OpenGraphCommand { get; set; }


        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _ofertaService = new OfertaService();
            _produtoService = new ProdutoService();

            Ofertas = new ObservableCollection<Oferta>();
            OfertasGraph = new ObservableCollection<Oferta>();
            Produtos = new ObservableCollection<Produto>();
            ProdutoOfertas = new ObservableCollection<ProdutoOfertas>();

            RefreshCommand = new DelegateCommand(CarregarOfertasAsync);

            OpenGraphCommand = new Command<ProdutoOfertas>(OpenGraphAsync);

            Sincroniza();

            CarregarOfertasAsync();
        }

        private async void Sincroniza()
        {
            await _ofertaService.SyncAsync();
            await _produtoService.SyncAsync();

            SyncOfertas();
            SyncProdutos();
        }

        public async void SyncOfertas()
        {
            var resultOferta = await _ofertaService.GetOfertas();

            Ofertas.Clear();

            foreach (var item in resultOferta)
            {
                Ofertas.Add(item);
            }
        }

        public async void SyncProdutos()
        {
            var resultProduto = await _produtoService.GetProdutos();

            Produtos.Clear();

            foreach (var item in resultProduto)
            {
                Produtos.Add(item);
            }
        }

        public void CarregarOfertasAsync()
        {
            Sincroniza();

            var query = from oferta in Ofertas
                        join produto in Produtos on oferta.ProdutoId equals produto.Id
                        select new { ProdutoNome = produto.NomeP, Preco = oferta.Preco, Data = oferta.DataInicio, IdProduto = produto.Id };

            foreach (var produtoOferta in query)
            {
                ProdutoOfertas item = new ProdutoOfertas
                {
                    IdProduto = produtoOferta.IdProduto,
                    Produto = produtoOferta.ProdutoNome,
                    Preco = produtoOferta.Preco,
                    DataInicio = produtoOferta.Data
                };

                ProdutoOfertas.Add(item);
            }

            IsBusy = false;
        }

        private async void OpenGraphAsync(ProdutoOfertas produtoOferta)
        {
            var query = from oferta in Ofertas
                        where oferta.ProdutoId == produtoOferta.IdProduto
                        select oferta;

            foreach (var item in query)
            {
                OfertasGraph.Add(item);
            }

            var navigationParams = new NavigationParameters();
            navigationParams.Add("ofertas", OfertasGraph);
            navigationParams.Add("produto", produtoOferta.Produto);
            await _navigationService?.NavigateAsync("GraphPage", navigationParams);
        }
    }
}
