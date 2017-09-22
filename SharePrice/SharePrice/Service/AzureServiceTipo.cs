using SharePrice;
using SharePrice.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using SharePrice.Service;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using SharePrice.Models;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(AzureServiceTipo))]
namespace SharePrice.Service
{
    public class AzureServiceTipo
    {
        public static readonly string AppUrl = "http://sharepriceapp.azurewebsites.net";
        private IMobileServiceClient _client;
        private IMobileServiceSyncTable<Tipo> _tableTipo;

        const string dbPath = "sharePriceDb";

        public AzureServiceTipo()
        {
            _client = new MobileServiceClient(AppUrl);

            var store = new MobileServiceSQLiteStore(dbPath);

            store.DefineTable<Tipo>();

            _client.SyncContext.InitializeAsync(store);
            _tableTipo = _client.GetSyncTable<Tipo>();
        }

        public async void AddTipo(Tipo tipo)
        {
           /* try
            {*/
                await _tableTipo.InsertAsync(tipo);
            /*}
            catch (Exception e)
            {
                var erro = e.InnerException;
            }*/
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
