using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
