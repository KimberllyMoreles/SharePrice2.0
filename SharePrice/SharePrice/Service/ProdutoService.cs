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
    public class ProdutoService
    {
        
        public static MobileServiceClient _client = new MobileServiceClient(@"http://sharepricecross.azurewebsites.net");
        private IMobileServiceSyncTable<Produto> _tableProduto;

        const string dbPath = "SharePriceDB";

        public ProdutoService()
        {
            //_client = new MobileServiceClient(ApplicationURL);

            var store = new MobileServiceSQLiteStore(dbPath);

            store.DefineTable<Produto>();

            _client.SyncContext.InitializeAsync(store);
            _tableProduto = _client.GetSyncTable<Produto>();
        }

        public async void AddProduto(Produto produto)
        {
            await _tableProduto.InsertAsync(produto);
        }

        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _client.SyncContext.PushAsync();
                await _tableProduto.PullAsync("allProdutos", _tableProduto.CreateQuery());
            }
            catch (MobileServicePushFailedException pushEx)
            {
                if (pushEx.PushResult != null)
                    syncErrors = pushEx.PushResult.Errors;
            }
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var empty = new Produto[0];


            try
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await SyncAsync();

                return await _tableProduto.ToEnumerableAsync();
            }
            catch (Exception ex)
            {
                return empty;
            }
        }
    }
}
