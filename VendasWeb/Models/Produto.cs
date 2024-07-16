using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required, MaxLength(128)]
        public string Nome { get; set; }

        public int Estoque { get; set; }

        public double Preco { get; set; }

        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
    }
}
