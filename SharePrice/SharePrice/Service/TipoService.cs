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

namespace SharePrice.Service
{
    public class TipoService
    {

        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Tipo> _table;
        const string dbPath = "tipoDb";
        private const string serviceUri = "http://sharepricecross.azurewebsites.net";

        public TipoService()
        {
            _client = new MobileServiceClient(serviceUri);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Tipo>();

            _client.SyncContext.InitializeAsync(store);
            _table = _client.GetSyncTable<Tipo>();
        }

        public async Task<IEnumerable<Tipo>> GetTipos()
        {
            var empty = new Tipo[0];
            try
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await SyncAsync();

                return await _table.ToEnumerableAsync();
            }
            catch (Exception ex)
            {
                return empty;
            }
        }

        public async void AddContact(Tipo tipo)
        {
            await _table.InsertAsync(tipo);
            await SyncAsync();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync("allTipos", _table.CreateQuery());                
            }

            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }


        public async Task CleanData()
        {
            await _table.PurgeAsync("allTipos", _table.CreateQuery(), new System.Threading.CancellationToken());
        }
    }
}
