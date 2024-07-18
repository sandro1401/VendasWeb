using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    [Table("Cliente")]
    public class Cliente :Usuario
    {
        [Required, Column(TypeName = "char(14)")]
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        [NotMapped]
        public int Idade
        {
            get => (int)Math.Floor((DateTime.Now - DataNascimento).TotalDays / 365.2425);
        }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>(); 
    }
}
