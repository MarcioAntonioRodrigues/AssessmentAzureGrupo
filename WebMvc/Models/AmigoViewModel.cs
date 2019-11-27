using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvc.Models
{
    public class AmigoViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Foto")]
        public string PictureUrl { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Sobrenome")]
        public string SobreNome { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Telefone { get; set; }
        [Required]
        [Display(Name = "Data de Aniversário")]
        public string Aniversario { get; set; }
        [Required]
        [Display(Name = "País de origem")]
        public string NomePais { get; set; }
        [Required]
        [Display(Name = "Estado de origem")]
        public string NomeEstado { get; set; }
    }
}