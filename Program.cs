using PontoDeVenda.models;
using PontoDeVenda.repository;
using PontoDeVenda.service;

string caminhoBaseDeDados = @"..\..\..\database";

IProdutoRepository produtoRepository = new ProdutoRepository(caminhoBaseDeDados);
ProdutoService produtoService = new ProdutoService(produtoRepository);

List<Produto> produtos = produtoService.GetProdutos();

Thread.Sleep(1000);

var logo = @"

█▀ █ █▀ ▀█▀ █▀▀ █▀▄▀█ ▄▀█   █▀█ █▀▄ █░█
▄█ █ ▄█ ░█░ ██▄ █░▀░█ █▀█   █▀▀ █▄▀ ▀▄▀";

Console.WriteLine(logo);
