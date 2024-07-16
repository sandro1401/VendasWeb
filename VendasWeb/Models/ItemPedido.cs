using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    [Table("ItemPedido")]
    public class ItemPedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int IdPedido { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdProduto { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public double ValorUnitario { get; set; }

       
       

       
        

        [NotMapped]
        public double ValorItem { get => this.Quantidade * this.ValorUnitario; }
    }
}
