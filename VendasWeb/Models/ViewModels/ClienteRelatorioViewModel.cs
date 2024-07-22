namespace VendasWeb.Models.ViewModels
{
    public class RelatorioViewModel
    {
        public IEnumerable<ClienteRelatorioViewModel> Clientes { get; set; }
    }
    public class ClienteRelatorioViewModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public IEnumerable<PedidoRelatorioViewModel> Pedidos { get; set; }
    }

    public class PedidoRelatorioViewModel
    {
        public int IdPedido { get; set; }
        public DateTime? DataPedido { get; set; }
        public double? ValorTotal { get; set; }
        public IEnumerable<ItemPedidoRelatorioViewModel> Itens { get; set; }
    }

    public class ItemPedidoRelatorioViewModel
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
    }


}
