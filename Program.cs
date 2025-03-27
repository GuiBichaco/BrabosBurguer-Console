using System;
using System.Collections.Generic;

class Program
{
    // Listas que armazenam os produtos, seus preços e brindes disponíveis
    static List<string> produtos = new List<string> { "X-Burguer", "Refrigerante", "Sorvete" };
    static List<double> precos = new List<double> { 25.00, 8.00, 10.00 };
    static List<string> brindes = new List<string> { "Batata Frita", "Churros", "Milkshake" };

    // Dicionário para armazenar os itens adicionados ao pedido
    static Dictionary<string, int> pedido = new Dictionary<string, int>();

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            // Exibe o menu principal
            Console.WriteLine("\n===== Menu da Lanchonete =====");
            Console.WriteLine("1. Listar produtos disponíveis");
            Console.WriteLine("2. Adicionar produto ao pedido");
            Console.WriteLine("3. Remover produto do pedido");
            Console.WriteLine("4. Visualizar pedido atual");
            Console.WriteLine("5. Finalizar pedido e sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            // Executa a opção escolhida pelo usuário
            switch (opcao)
            {
                case 1:
                    ListarProdutos();
                    break;
                case 2:
                    AdicionarProduto();
                    break;
                case 3:
                    RemoverProduto();
                    break;
                case 4:
                    VisualizarPedido();
                    break;
                case 5:
                    FinalizarPedido();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (opcao != 5);
    }

    // Exibe a lista de produtos disponíveis com preços
    static void ListarProdutos()
    {
        Console.WriteLine("\nProdutos disponíveis:");
        for (int i = 0; i < produtos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {produtos[i]} - R$ {precos[i]:F2}");
        }
    }

    // Permite ao usuário adicionar produtos ao pedido
    static void AdicionarProduto()
    {
        ListarProdutos();
        Console.Write("Digite o número do produto desejado: ");
        int escolha = int.Parse(Console.ReadLine()) - 1;

        if (escolha >= 0 && escolha < produtos.Count)
        {
            Console.Write("Digite a quantidade: ");
            int quantidade = int.Parse(Console.ReadLine());

            // Se o produto já estiver no pedido, soma a quantidade, senão, adiciona
            if (pedido.ContainsKey(produtos[escolha]))
                pedido[produtos[escolha]] += quantidade;
            else
                pedido[produtos[escolha]] = quantidade;

            Console.WriteLine("Produto adicionado ao pedido!");
        }
        else
        {
            Console.WriteLine("Escolha inválida!");
        }
    }

    // Permite ao usuário remover produtos do pedido
    static void RemoverProduto()
    {
        VisualizarPedido();
        Console.Write("Digite o nome do produto para remover: ");
        string nomeProduto = Console.ReadLine();

        if (pedido.ContainsKey(nomeProduto))
        {
            Console.Write("Digite a quantidade a remover: ");
            int quantidade = int.Parse(Console.ReadLine());

            // Se a quantidade informada for menor, reduz apenas o valor, senão remove o item
            if (pedido[nomeProduto] > quantidade)
                pedido[nomeProduto] -= quantidade;
            else
                pedido.Remove(nomeProduto);

            Console.WriteLine("Produto removido do pedido!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado no pedido!");
        }
    }

    // Exibe os produtos no pedido e calcula o total
    static void VisualizarPedido()
    {
        Console.WriteLine("\nSeu pedido atual:");
        if (pedido.Count == 0)
        {
            Console.WriteLine("Nenhum item no pedido.");
            return;
        }

        double total = 0;
        foreach (var item in pedido)
        {
            double precoUnitario = precos[produtos.IndexOf(item.Key)];
            double precoTotal = precoUnitario * item.Value;
            total += precoTotal;
            Console.WriteLine($"{item.Key} - {item.Value}x - R$ {precoTotal:F2}");
        }
        Console.WriteLine($"Total: R$ {total:F2}");
    }

    // Finaliza o pedido e aplica descontos e brindes, se aplicáveis
    static void FinalizarPedido()
    {
        VisualizarPedido();
        double total = 0;
        int totalItens = 0;
        foreach (var item in pedido)
        {
            double precoUnitario = precos[produtos.IndexOf(item.Key)];
            total += precoUnitario * item.Value;
            totalItens += item.Value;
        }

        double desconto = 0;
        string brinde = "";

        // Aplica desconto de 10% para pedidos acima de R$ 100
        if (total > 100)
        {
            desconto = total * 0.10;
            Console.WriteLine("Desconto de 10% aplicado!");
        }
        // Oferece um brinde se o cliente pedir mais de 5 itens
        else if (totalItens > 5)
        {
            Random rand = new Random();
            brinde = brindes[rand.Next(brindes.Count)];
            Console.WriteLine($"Brinde aplicado ao pedido: {brinde}!");
        }

        Console.WriteLine($"Valor final a pagar: R$ {total - desconto:F2}");
        Console.WriteLine("Pedido finalizado. Obrigado pela preferência!");

        // Adicionando uma pausa para evitar que o CMD feche imediatamente
        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}
