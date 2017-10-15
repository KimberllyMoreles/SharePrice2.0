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
    public class OfertaService
    {

        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Oferta> _table;
        const string dbPath = "ofertaDb";
        private const string serviceUri = "http://sharepricecross.azurewebsites.net";

        public OfertaService()
        {
            _client = new MobileServiceClient(serviceUri);
            var store = new MobileServiceSQLiteStore(dbPath);
            store.DefineTable<Oferta>();

            _client.SyncContext.InitializeAsync(store);
            _table = _client.GetSyncTable<Oferta>();
        }

        public async Task<IEnumerable<Oferta>> GetOfertas()
        {
            var empty = new Oferta[0];
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

        public async void AddContact(Oferta oferta)
        {
            await _table.InsertAsync(oferta);
            await SyncAsync();
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _client.SyncContext.PushAsync();
                await _table.PullAsync("allOfertas", _table.CreateQuery());                
            }

            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }


        public async Task CleanData()
        {
            await _table.PurgeAsync("allOfertas", _table.CreateQuery(), new System.Threading.CancellationToken());
        }
    }
}
