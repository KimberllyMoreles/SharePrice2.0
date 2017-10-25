using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SharePrice.Models
{
    [DataTable("Tipo")]
    public class Tipo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nome")]
        public string NomeT { get; set; }
        
        [Version]
        private string Versao;
    }
}
