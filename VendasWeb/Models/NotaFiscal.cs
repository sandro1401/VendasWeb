using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }

        // Número da nota fiscal
        public string Numero { get; set; }

        // Data de emissão da nota fiscal
        public DateTime DataEmissao { get; set; }

        // Identificador do pedido associado à nota fiscal
        public int PedidoId { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public decimal ValorProdutos { get; set; }


        public decimal ValorTotal { get; set; }

        // Observações adicionais sobre a nota fiscal
        public string Observacoes { get; set; }

        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>(); 
    }
}
