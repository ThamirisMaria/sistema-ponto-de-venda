using PontoDeVenda.models;
using PontoDeVenda.repository;

namespace PontoDeVenda.service
{
    public class ProdutoService
    {
        private readonly IProdutoRepository produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        public List<Produto> GetProdutos()
        {
            return produtoRepository.GetProdutos();
        }
    }
}
