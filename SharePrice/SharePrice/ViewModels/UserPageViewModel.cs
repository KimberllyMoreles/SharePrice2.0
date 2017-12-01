using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SharePrice.Helpers;
using SharePrice.Models;
using SharePrice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SharePrice.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        //declara o campo de imagem da oferta
        public ImageSource imagemPerfil;
        public ImageSource ImagemPerfil
        {
            get { return this.imagemPerfil; }
            set
            {
                if (Equals(value, this.imagemPerfil))
                {
                    return;
                }
                this.imagemPerfil = value;
                OnPropertyChanged();
            }
        }
        
        //declara o entry do local da oferta
        public string nomePerfil;
        public string NomePerfil
        {
            get { return this.nomePerfil; }
            set
            {
                if (Equals(value, this.nomePerfil))
                {
                    return;
                }
                this.nomePerfil = value;
                OnPropertyChanged();
            }
        }

        public UserPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ImagemPerfil = Settings.UserImage;
            NomePerfil = Settings.UserName;
        }
    }
}
