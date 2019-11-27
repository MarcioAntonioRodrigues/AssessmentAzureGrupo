using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string BandeiraUrl { get; set; }
        public virtual ICollection<Estado> Estados { get; set; }
    }
}
