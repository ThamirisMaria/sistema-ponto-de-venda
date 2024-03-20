namespace PontoDeVenda.models
{
    public class ItemPedido
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; } = 0;
        public decimal ValorTotal => Quantidade * Produto.Preco;
    }
}
