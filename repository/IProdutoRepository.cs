using PontoDeVenda.models;

namespace PontoDeVenda.repository
{
    public interface IProdutoRepository
    {
        List<Produto> GetProdutos();
    }
}
