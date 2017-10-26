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
using Xamarin.Forms;

namespace SharePrice.ViewModels
{
    public class ListPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private OfertaService _ofertaService;
        public ObservableCollection<Oferta> Ofertas { get; set; }
        public DelegateCommand RefreshCommand { get; set; }


        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _ofertaService = new OfertaService();

            Ofertas = new ObservableCollection<Oferta>();
            RefreshCommand = new DelegateCommand(CarregarOfertasAsync);

            CarregarOfertasAsync();
        }

        public async void CarregarOfertasAsync()
        {
            var result = await _ofertaService.GetOfertas();

            Ofertas.Clear();

            foreach (var item in result)
            {
                Ofertas.Add(item);
            }
            IsBusy = false;
        }
    }
}
