using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tp3AzureMarcio.Models
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Aniversario { get; set; }
        public string NomePais { get; set; }
        public string NomeEstado { get; set; }
    }
}