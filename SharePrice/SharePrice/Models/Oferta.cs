using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePrice.Models
{
    [DataTable("Oferta")]
    public class Oferta
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("usuarioId")]
        public string UsuarioId { get; set; }

        [JsonProperty("produtoId")]
        public string ProdutoId { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("dataInicio")]
        public DateTime DataInicio { get; set; }

        [JsonProperty("dataFim")]
        public DateTime DataFim { get; set; }

        [JsonProperty("destaque")]
        public bool Destaque { get; set; }

        [JsonProperty("local")]
        public string Local { get; set; }

    }
}
