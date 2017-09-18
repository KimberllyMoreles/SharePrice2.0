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
        [JsonProperty("IdTipo")]
        public int Id { get; set; }

        [JsonProperty("NomeTipo")]
        public string Nome { get; set; }

        private string Version;
    }
}
