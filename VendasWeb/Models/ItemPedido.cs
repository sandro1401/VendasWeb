using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VendasWeb.Models
{
    [Table("ItemPedido")]
    public class ItemPedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "O campo Pedido é obrigatório.")]
        public int IdPedido { get; set; }
        [ForeignKey(nameof(IdPedido))]
        public Pedido Pedido { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "O campo Produto é obrigatório.")]
        public int IdProduto { get; set; }
        [ForeignKey(nameof(IdProduto))]
        public Produto Produto { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "O campo Valor Unitário é obrigatório.")]
        public double ValorUnitario { get; set; }

        

        [NotMapped]
        public double ValorItem { get => this.Quantidade * this.ValorUnitario; }
    }
}
