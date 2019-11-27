using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models
{
    public class PaisViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string BandeiraUrl { get; set; }
        public virtual ICollection<Estado> Estados { get; set; }
    }
}