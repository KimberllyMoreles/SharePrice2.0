using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePrice.Models
{
    public class ProdutoOfertas
    {
        public string Produto { get; set; }
        
        public double Preco { get; set; }
        
        public DateTime DataInicio { get; set; }
    }
}
