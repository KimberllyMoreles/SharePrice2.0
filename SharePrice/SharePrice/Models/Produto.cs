using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SharePrice.Models
{
    [DataTable("Produto")]
    public class Produto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("tipoId")]
        public string TipoId { get; set; }

        [Version]
        private string Versao;
    }
}
