# Desafio - Sistema Ponto de Venda (PoS - Point of Sale)

O Sistema Ponto de Venda (PoS - Point of Sale) simula um módulo de frente de caixa de supermercado integrado com o respectivo estoque dos produtos, em que baixas são dadas sempre que um item é adicionado ao pedido. Pedidos também podem ser concluídos com os devidos pagamentos.

Seu desafio aqui será construir este sistema seguindo os requisitos, procurando identificar as entidades para construir da melhor forma possível o relacionamento entre os objetos envolvidos.

## Requisitos funcionais
- [ ] A base de produtos deverá ser carregada na inicialização, através de um arquivo `.csv` com os dados fornecidos ao final do card.
- [ ] O menu deve ser infinito, ou seja, o sistema só deve parar quando um comando chave (atalho, tecla, número específico, etc) for digitado pelo operador.
- [ ] Só pode existir 1 pedido sendo lançado por vez pelo operador.
- [ ] O operador pode adicionar itens (produtos) no pedido, através do código do produto.
- [ ] Deve haver baixa no estoque do produto da quantidade do item adicionado no pedido.
- [ ] Deve haver um agrupamento do item no pedido quando o mesmo produto for adicionado 2 ou mais vezes.
- [ ] Não pode haver adição de itens no pedido cuja quantidade ultrapasse a quantidade de estoque atual do produto.
- [ ] Deve haver uma forma de exibição do pedido em tela, mostrando ao operador os itens adicionados, suas quantidades, preços e valores totais (inclusive o valor total do pedido).
- [ ] Deve haver a opção para o operador sinalizar que vai ser feito o pagamento do pedido, informando se vai ser em dinheiro ou cartão.
- [ ] Se o pagamento for em cartão, o pedido poderá ser encerrado imediatamente.
- [ ] Se o pagamento for em dinheiro, o operador deverá informar o valor pago, e o sistema deverá calcular o troco, descrevendo de forma detalhada quais cédulas e moedas o operador deverá entregar ao cliente, levando em consideração a *forma mais otimizada possível*, ou seja, o menor número possível de cédulas e moedas.
  - [ ] Os valores de cédulas a se considerar são: R$ `200`, `100`, `50`, `20`, `10`, `5` e `2`.
  - [ ] Os valores de moedas a se considerar são: R$ `1`, `0,50`, `0,25`, `0,10`, `0,05` e `0,01`.
- [ ] Não pode haver pagamento parcial do pedido (pagamento menor que o valor total).
  > - Variação (opcional): Caso você queira se desafiar mais, ao final poderá ignorar esse requisito e implementar uma forma de pagamentos parciais.
- [ ] Quando um pedido é finalizado, o sistema fica livre para adição de itens em um novo pedido. 

## Requisitos não funcionais
- Não há banco de dados, o sistema deve ser todo "in-memory".

## Dados para o arquivo `.csv`
```csv
codigo,nome,preco,estoque
1,Arroz,2.25,50
2,Feijao,5.50,50
3,Ovos,6,20
4,Leite,2.90,50
5,Massa,3,30
6,Achocolatado,8,10
7,Suco,1,100
8,Pao,2.4,15
9,Batata,2.7,100
10,Tomate,4,75
11,Cebola,1.80,100
12,Pimentao,4.25,20
13,Laranja,6.85,10
14,Morango,5,5
15,Mamao,7.65,5
16,Sabonete,1.95,15
17,Xampu,4.85,15
18,Condicionador,5.10,15
19,Bombom,1,100
20,Caqui,2.50,100
```
> Dica: Copie este bloco e cole em um notepad ou similar, salvando com um nome de preferência e com a extensão `.csv`
