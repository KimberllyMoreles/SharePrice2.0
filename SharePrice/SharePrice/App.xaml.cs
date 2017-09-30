using DryIoc;
using Prism.DryIoc;
using SharePrice.Events;
using SharePrice.Service;
using SharePrice.Views;
using Xamarin.Forms;

namespace SharePrice
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<InformationPage>();
            Container.RegisterTypeForNavigation<InitialPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<AdicionarOfertaPage>();

            Container.Register<IInputAlertDialogService, InputAlertDialogService>();
            Container.RegisterTypeForNavigation<ListPage>();
            Container.RegisterTypeForNavigation<UserPage>();
            Container.RegisterTypeForNavigation<NotificationPage>();
        }
    }
}
