using PontoDeVenda.models;
using PontoDeVenda.repository;
using PontoDeVenda.service;
using System.Threading.Channels;

string caminhoBaseDeDados = @"..\..\..\database";

IProdutoRepository produtoRepository = new ProdutoRepository(caminhoBaseDeDados);
ProdutoService produtoService = new ProdutoService(produtoRepository);

List<Produto> produtos = produtoService.GetProdutos();

Thread.Sleep(1000);
Console.Clear();

var logo = @"
█▀ █ █▀ ▀█▀ █▀▀ █▀▄▀█ ▄▀█   █▀█ █▀▄ █░█
▄█ █ ▄█ ░█░ ██▄ █░▀░█ █▀█   █▀▀ █▄▀ ▀▄▀";

Pedido pedido = new Pedido();
bool continuar = true;
while (continuar)
{
    Console.Clear();

    Console.WriteLine(logo);

    Console.WriteLine("\n[ CONTROLES ]");
    Console.WriteLine("[ + ] Adicionar novo item ao pedido");
    Console.WriteLine("[ Tab ] - Ver produtos em estoque");
    Console.WriteLine("\nPressione Esc para sair...");

    Console.Write("\nEscolha uma opção: ");
    ConsoleKey opcao = Console.ReadKey().Key;

    switch (opcao)
    {
        case ConsoleKey.Add:
        case ConsoleKey.OemPlus:
            if (pedido.Finalizado)
            {
                pedido = new Pedido();
            }
            pedido.AdicionarItens(produtos);
            break;
        case ConsoleKey.Tab:
            ExibirProdutosEmEstoque();
            break;
        case ConsoleKey.Escape:
            continuar = false;
            break;
        default:
            break;
    }
}

void ExibirProdutosEmEstoque()
{
    Console.WriteLine("\n\nProdutos em estoque: \n");
    foreach (Produto produto in produtos)
    {
        if (produto.Estoque > 0)
        {
            Console.WriteLine($"Código: {produto.Codigo} | Nome: {produto.Nome} | Preço: {produto.Preco} | Estoque: {produto.Estoque}");
        }
    }

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey(true);
    Console.Clear();
}