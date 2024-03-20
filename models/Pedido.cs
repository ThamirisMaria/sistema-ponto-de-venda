using PontoDeVenda.repository;
using PontoDeVenda.service;

namespace PontoDeVenda.models
{
    public class Pedido
    {
        private static int idAtual = 1;
        public int Id { get; private set; }
        public List<ItemPedido> Itens { get; }
        public MetodosDePagamento MetodoDePagamento { get; set; }

        public DateTime Data = DateTime.Now;

        public bool Finalizado { get; private set; }
        public decimal ValorTotal => Itens.Sum(item => item.ValorTotal);

        public Pedido()
        {
            Id = idAtual;
            Itens = [];

            idAtual++;
        }

        public void AdicionarItem(int codigoProduto, int quantidade, List<Produto> produtos)
        {
            Produto produto = produtos.Find(p => p.Codigo == codigoProduto)!;
            if (produto != null)
            {
                ItemPedido itemExistente = Itens.Find(i => i.Produto.Codigo == codigoProduto)!;
                if (quantidade > 0)
                {
                    if (produto.Estoque >= quantidade)
                    {
                        if (itemExistente != null)
                        {
                            Console.WriteLine($"Item Existente: {itemExistente.Produto.Nome}");
                            itemExistente.Quantidade += quantidade;
                        }
                        else
                        {
                            Itens.Add(new ItemPedido
                            {
                                Produto = produto,
                                Quantidade = quantidade
                            });
                        }

                        produto.Estoque -= quantidade;
                    }
                    else
                    {
                        Console.WriteLine($"Estoque insuficiente: Apenas {produto.Estoque} unidade(s) de {produto.Nome} em estoque.");
                    }
                }else
                    Console.WriteLine("Quantidade inválida!");
                }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        public void AdicionarItens(List<Produto> produtos)
        {
            int codigoProduto = 0;
            int quantidadeProduto = 0;
            bool continuar = true;


            do
            {
                Console.Clear();
                if (Itens.Count <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("\nIniciando novo pedido...");
                    Thread.Sleep(1000);
                };
                Console.WriteLine("\n---- NOVO ITEM ----\n");
                Console.Write("Insira o código do produto: ");
                codigoProduto = int.Parse(Console.ReadLine()!);
                Console.Write("Insira a quantidade: ");
                quantidadeProduto = int.Parse(Console.ReadLine()!);
                AdicionarItem(codigoProduto, quantidadeProduto, produtos);

                if (Itens.Count > 0)
                {
                    ExibirPedido();
                }

                Console.WriteLine("\n[ CONTROLES ]");
                Console.WriteLine("[ Enter ] - Continuar");
                Console.WriteLine("[ Insert ] - Ir para pagamento");
                Console.WriteLine("[ Esc ] - Voltar ao início");

                ConsoleKey opcao = Console.ReadKey().Key;
                switch (opcao)
                {
                    case ConsoleKey.Enter:
                        continue;
                    case ConsoleKey.Insert:
                        Console.Clear();
                        ExecutarPagamento();
                        continuar = false;
                        break;
                    case ConsoleKey.Escape:
                        continuar = false;
                        break;
                    default:
                        continue;
                }
                Console.Clear();
            } while (continuar);
        }

        public void ExibirPedido()
        {
            Console.WriteLine($"\n-------------- Detalhes do Pedido Nº {Id} --------------\n");
            Console.WriteLine(Data);
            Console.WriteLine($"\nItens do pedido: \n");

            int codigoWidth = Math.Max(7, Itens.Max(i => i.Produto.Codigo.ToString().Length) + 2);
            int nomeWidth = Math.Max(15, Itens.Max(i => i.Produto.Nome.Length) + 2);
            int quantidadeWidth = Math.Max(10, Itens.Max(i => i.Quantidade.ToString().Length) + 2);
            int precoUnitarioWidth = Math.Max(15, Itens.Max(i => i.Produto.Preco.ToString("C").Length) + 2);
            int valorTotalItemWidth = Math.Max(15, Itens.Max(i => i.ValorTotal.ToString("C").Length) + 2);

            Console.WriteLine(new string('-', codigoWidth + nomeWidth + quantidadeWidth + precoUnitarioWidth + valorTotalItemWidth + 16));
            Console.WriteLine($"| Código | Nome             | Quantidade | Preço Unitário | Total            |");
            Console.WriteLine(new string('-', codigoWidth + nomeWidth + quantidadeWidth + precoUnitarioWidth + valorTotalItemWidth + 16));

            foreach (var item in Itens)
            {
                Console.WriteLine($"| {item.Produto.Codigo.ToString().PadRight(codigoWidth)} | {item.Produto.Nome.PadRight(nomeWidth)} | {item.Quantidade.ToString().PadRight(quantidadeWidth)} | {item.Produto.Preco.ToString("C").PadRight(precoUnitarioWidth)} | {item.ValorTotal.ToString("C").PadRight(valorTotalItemWidth)} |");
            }

            Console.WriteLine(new string('-', codigoWidth + nomeWidth + quantidadeWidth + precoUnitarioWidth + valorTotalItemWidth + 16));
            Console.WriteLine($"Valor Total do Pedido: {ValorTotal:C}");
            Console.WriteLine(new string('-', codigoWidth + nomeWidth + quantidadeWidth + precoUnitarioWidth + valorTotalItemWidth + 16));
        }

        internal void ExecutarPagamento()
        {
            MetodosDePagamento metodoDePagamento;
            Console.WriteLine("Selecione a forma de pagamento: ");
            Console.WriteLine("[ 1 ] - Dinheiro");
            Console.WriteLine("[ 2 ] - Cartão");
            metodoDePagamento = Console.ReadLine() == "1" ? MetodosDePagamento.Dinheiro : MetodosDePagamento.Cartao;

            if(metodoDePagamento == MetodosDePagamento.Dinheiro)
            {
                decimal valorPago = 0;
                Console.WriteLine($"\nValor total a pagar: {ValorTotal}");
                Console.Write($"\nInsira o valor pago: ");
                valorPago = Convert.ToDecimal(Console.ReadLine());

                if(valorPago > 0)
                {
                    if(valorPago > ValorTotal)
                    {
                        ExibirTroco(valorPago);
                    }else if (valorPago < ValorTotal)
                    {
                        Console.Clear();
                        Console.WriteLine("\nValor pago não pode ser menor que o valor total do pedido.\n");
                        ExecutarPagamento();
                    }
                    FinalizarPedido();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nValor pago precisa ser maior que 0.\n");
                    ExecutarPagamento();
                }
            }
            else if(metodoDePagamento == MetodosDePagamento.Cartao)
            {
                FinalizarPedido();
            }
        }

        private void ExibirTroco(decimal valorPago)
        {
            int[] cedulas = { 200, 100, 50, 20, 10, 5, 2 };
            decimal[] moedas = { 1, 0.5m, 0.25m, 0.1m, 0.05m, 0.01m };
            Dictionary<string, int> troco = new Dictionary<string, int>();

            decimal trocoRestante = valorPago - ValorTotal;

            foreach (int cedula in cedulas)
            {
                int quantidade = (int)(trocoRestante / cedula);
                if (quantidade > 0)
                {
                    troco.Add($"R$ {cedula:0.00}", quantidade);
                    trocoRestante -= quantidade * cedula;
                }
            }

            foreach (decimal moeda in moedas)
            {
                int quantidade = (int)(trocoRestante / moeda);
                if (quantidade > 0)
                {
                    troco.Add($"R$ {moeda:0.00}", quantidade);
                    trocoRestante -= quantidade * moeda;
                }
            }

            Console.WriteLine("\nTroco a ser devolvido ao cliente:\n");
            foreach (var item in troco)
            {
                Console.WriteLine($"{item.Key} \n Quantidade: {item.Value}");
            }

            Console.WriteLine("\nPressione qualquer tecla para finalizar...");
            Console.ReadKey(true);
            Console.Clear();
        }

        private void FinalizarPedido()
        {
            Finalizado = true;
            Console.WriteLine("Finalizando pedido...");
            Thread.Sleep(1000);
        }
    }
}
