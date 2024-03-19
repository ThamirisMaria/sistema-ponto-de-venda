using PontoDeVenda.models;
using System.Globalization;

namespace PontoDeVenda.repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string caminhoBaseDeDados;
        private readonly StreamReader leitorProdutos;
        public ProdutoRepository(string caminhoBaseDeDados)
        {
            this.caminhoBaseDeDados = caminhoBaseDeDados;
            this.leitorProdutos = new StreamReader(File.OpenRead($@"{caminhoBaseDeDados}\produtos.csv"));
        }

        public List<Produto> GetProdutos()
        {
            Console.WriteLine("Carregando produtos...");
            string? linha = leitorProdutos.ReadLine();
            List<Produto> produtos = new List<Produto>();
            Produto produto;
            while ((linha = leitorProdutos.ReadLine()!) != null)
            {
                var valores = linha.Split(",");
                produto = new Produto();
                produto.Codigo = int.Parse(valores[0]);
                produto.Nome = valores[1];
                NumberFormatInfo format = new NumberFormatInfo();
                format.NumberDecimalSeparator = ".";
                produto.Preco = decimal.Parse(valores[2], format);
                produto.Estoque = int.Parse(valores[3]);
                produtos.Add(produto);
            }

            return produtos;
        }
    }
}
