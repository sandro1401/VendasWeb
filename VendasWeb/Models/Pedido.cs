using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }

        public DateTime? DataPedido { get; set; }

        public DateTime? DataEntrega { get; set; }

        public double? ValorTotal { get; set; }

        public int IdCliente { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
        public int IdEntrega { get; set; }
        [ForeignKey("EnderecoId")]
        public Endereco EnderecoEntrega { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>(); 
    }
}
