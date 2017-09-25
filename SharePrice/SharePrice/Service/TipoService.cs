using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using SharePrice.Models;
using SharePrice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(TipoService))]
namespace SharePrice.Service
{
    public class TipoService
    {
        public static readonly string AppUrl = "http://sharepriceapp.azurewebsites.net";
        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Tipo> _tableTipo;

        const string dbPath = "sharePriceDb";

        public TipoService()
        {
            _client = new MobileServiceClient(AppUrl);

            var store = new MobileServiceSQLiteStore(dbPath);

            store.DefineTable<Tipo>();

            _client.SyncContext.InitializeAsync(store);
            _tableTipo = _client.GetSyncTable<Tipo>();
        }

        public async void AddTipo(Tipo tipo)
        {
            try
            {
                await _tableTipo.InsertAsync(tipo);
            }
            catch (Exception e)
            {
                var erro = e.InnerException;
            }
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _client.SyncContext.PushAsync();

                await _tableTipo.PullAsync("allTipos", _tableTipo.CreateQuery());
            }
            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }

        public async Task<IEnumerable<Tipo>> GetTipos()
        {
            var empty = new Tipo[0];
            try
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await SyncAsync();

                return await _tableTipo.ToEnumerableAsync();
            }
            catch (Exception ex)
            {
                return empty;
            }
        }
    }
}
