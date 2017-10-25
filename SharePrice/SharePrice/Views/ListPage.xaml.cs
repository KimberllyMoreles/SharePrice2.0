using SharePrice.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SharePrice.Views
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            list.ItemsSource = new List<Oferta>
{
    new Oferta { ProdutoId = "Angelina Figueira", Preco = 15 },
    new Oferta { ProdutoId = "Angelino Neiva", Preco = 20 },
    new Oferta { ProdutoId = "Boaventura Pardo", Preco = 25 },
    new Oferta { ProdutoId = "Cristóvão Peseiro", Preco = 28 },
    new Oferta { ProdutoId = "Osvaldo Costa", Preco = 31 },
    new Oferta { ProdutoId = "Priscila Bento", Preco = 47 },
    new Oferta { ProdutoId = "Sandoval Rocha", Preco = 19 },
    new Oferta { ProdutoId = "Veríssimo Porciúncula", Preco = 34 }
};
        }
    }
}
