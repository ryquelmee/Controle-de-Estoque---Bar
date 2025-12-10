
using System.ComponentModel;
using Microsoft.VisualBasic;
using Spectre.Console;
using Projeto_Engenharia.Controller;
using Projeto_Engenharia.Model;
using System.Globalization;

namespace Projeto_Engenharia.View
{
    class InterfaceUsuario
    {
        private int opcao;
        private ControladorEspacos conE;
        private ControladorProdutos conP;
        private ControladorGeral conG;

        public InterfaceUsuario(ControladorEspacos conE, ControladorProdutos conP, ControladorGeral conG)
        {
            this.conE = conE;
            this.conP = conP;
            this.conG = conG;
        }

        public void menuPrincipal()
        {
            while(true)
            {
                AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("STOCKCENTER")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.Write(new Rule("[yellow]Selecione uma opcao[/]").Centered());
            AnsiConsole.WriteLine();


            var menuP = new Panel(
                "[bold cyan]Use as setas ↑ ↓ e ENTER para selecionar[/]");
            menuP.Border = BoxBorder.Rounded;
            menuP.Padding = new Padding(1, 1);
            menuP.BorderStyle = new Style(Color.Grey);
            AnsiConsole.Write(menuP);
            AnsiConsole.WriteLine();

            this.opcao = AnsiConsole.Prompt(
                new SelectionPrompt<(int codigo, string descricao)>()
                    .Title("[bold green]O que deseja fazer?[/]")
                    .HighlightStyle(new Style(Color.Yellow))
                    .PageSize(10)
                    .MoreChoicesText("[grey](Mais opções para baixo)[/]")
                    .UseConverter(x => $"[white]{x.descricao}[/]")
                    .AddChoices(new (int codigo, string descricao)[]
                    {
                        (1, " Gerenciar espaços"),
                        (2, " Gerenciar produtos"),
                        (3, " Mostrar estoque"),
                        (4, " Gerar relatório"),
                        (5, " Sair")
                    })
            ).codigo;

            AnsiConsole.Clear();


                //encaminhar pra opcao
                switch (this.opcao)
                {
                    case 1:
                        this.menuEspacos();
                        break;
                    case 2:
                        this.menuProdutos();
                        break;
                    case 3:
                        List<Espaco> ListaEspacos = this.conG.mostrarEstoque();

                        AnsiConsole.Clear();
                        AnsiConsole.Write(new FigletText("Estoque").Color(Color.Green));
                        {
                            foreach (Espaco espaco in ListaEspacos)
                            {
                                AnsiConsole.Write(new Rule($"[bold yellow]{espaco.Nome}[/]").LeftJustified());
                                AnsiConsole.MarkupLine($"[dim]Caminho: {espaco.Caminho}[/]");

                                if (espaco.produtos.Count > 0)
                                {
                                    var tabela = new Table();
                                    tabela.Border(TableBorder.Rounded);

                                    tabela.AddColumn("Produto");
                                    tabela.AddColumn("Quantidade");
                                    tabela.AddColumn("Preço Compra"); 
                                    tabela.AddColumn("Preço Venda");
                                    tabela.AddColumn("Validade");
                                    tabela.AddColumn("Caminho");

                                    foreach (Produto p in espaco.produtos)
                                    {
                                        tabela.AddRow(
                                            p.Nome,
                                            p.Quantidade < 5 ? $"[red]{p.Quantidade}[/]" : p.Quantidade.ToString(),
                                            $"R$ {p.PrecoCompra:F2}",
                                            $"R$ {p.PrecoVenda:F2}",
                                            p.Validade,
                                            p.Caminho 
                                );
                            }
                                    AnsiConsole.Write(tabela);
                                }
                                else
                                {
                                    AnsiConsole.MarkupLine("[grey]  * Nenhum produto neste espaço *[/]");
                                }
                                
                                AnsiConsole.WriteLine(); 
                            }
                        }

                        AnsiConsole.WriteLine();
                        AnsiConsole.Write(new Rule());
                        AnsiConsole.MarkupLine("[bold yellow]Pressione ENTER para voltar ao menu...[/]");
                        Console.ReadLine(); 

                        break;
                    case 4:
                        string data_inicial = AnsiConsole.Prompt(
                            new TextPrompt<string>("Validade inicial (dd/mm/aaaa): ")
                            .Validate(input =>
                            {
                                if (!DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                {
                                    return ValidationResult.Error("Data Inválida! Use o formato dd/mm/aaaa");
                                }
                                return ValidationResult.Success();
                            }));

                        DateTime dtInicial = DateTime.ParseExact(data_inicial, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        string data_final = AnsiConsole.Prompt(
                            new TextPrompt<string>("Validade final (dd/mm/aaaa): ")
                            .Validate(input =>
                            {
                                if (!DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtFinal))
                                {
                                    return ValidationResult.Error("Data Inválida! Use o formato dd/mm/aaaa");
                                }

                                if (dtFinal < dtInicial)
                                {
                                    return ValidationResult.Error("A data final não pode ser anterior à data inicial!");
                                }
                                return ValidationResult.Success();
                            }));

                        Relatorio relatorio = conG.gerarRelatorio(data_inicial, data_final);
                        var table = new Table();


                        table.Title($"Relatório: [yellow]{relatorio.dataInicial:dd/MM/yyyy}[/] a [yellow]{relatorio.dataFinal:dd/MM/yyyy}[/]");
                        table.Border(TableBorder.Rounded);

                        table.AddColumn("Data");
                        table.AddColumn(new TableColumn("Tipo").Centered()); 
                        table.AddColumn("Nome");
                        table.AddColumn("Quantidade");

                        foreach (var alteracao in relatorio.ListaAlteracoes)
                        {
                            string cor = alteracao.TipoAlteracao == "Adicionar" ? "green" : "red";

                            table.AddRow(
                                alteracao.Data, 
                                $"[{cor}]{alteracao.TipoAlteracao}[/]",                                
                                alteracao.NomeProduto,
                                alteracao.Quantidade.ToString() 
                            );
                        }
                        AnsiConsole.Write(table);

                        AnsiConsole.WriteLine();
                        AnsiConsole.Write(new Rule());
                        AnsiConsole.MarkupLine("[bold yellow]Pressione ENTER para voltar ao menu...[/]");
                        Console.ReadLine(); 
                        break;
                    case 5:
                        AnsiConsole.WriteLine("Saindo...");
                        Environment.Exit(0);
                        break;
                }
            }
        }
                public void menuEspacos()
                {
                    while (true)
                    {
                        AnsiConsole.Clear();

                        AnsiConsole.Write(
                            new FigletText("ESPAÇOS")
                                .Centered()
                                .Color(Color.Yellow)); 

                        AnsiConsole.Write(new Rule("[yellow]Gerenciar Espaços de Armazenamento[/]").Centered());
                        AnsiConsole.WriteLine();

                        var menuP = new Panel(
                            "[bold cyan]Use as setas ↑ ↓ e ENTER para selecionar[/]");
                        menuP.Border = BoxBorder.Rounded;
                        menuP.Padding = new Padding(1, 1);
                        menuP.BorderStyle = new Style(Color.Grey);
                        AnsiConsole.Write(menuP);
                        AnsiConsole.WriteLine();

                        this.opcao = AnsiConsole.Prompt(
                            new SelectionPrompt<(int codigo, string descricao)>()
                                .Title("[bold yellow]O que deseja fazer com os espaços?[/]")
                                .HighlightStyle(new Style(Color.Green)) 
                                .PageSize(10)
                                .MoreChoicesText("[grey](Mais opções para baixo)[/]")
                                .UseConverter(x => $"[white]{x.descricao}[/]") 
                                .AddChoices(new (int codigo, string descricao)[]
                                {
                                    (1, "Adicionar espaço"),
                                    (2, "Editar espaço"),
                                    (3, "Remover espaço"),
                                    (4, "Voltar ao Menu Principal")
                                    })
                            ).codigo;
            
                            switch (this.opcao)
                            {
                                case 1:
                                    string nome = AnsiConsole.Ask<string>("Nome do espaço: ");
                                    string espacoPai = AnsiConsole.Ask<string>("Você quer adicionar dentro de qual espaço?: ");
                                    AnsiConsole.WriteLine(conE.criarEspaco(nome, espacoPai));     
                                    break;
                                case 2:
                                    string nomeAtual = AnsiConsole.Ask<string>("Nome do espaço: ");
                                    string novoNome = AnsiConsole.Ask<string>("Nome novo do espaco: ");
                                    AnsiConsole.WriteLine(conE.editarEspaco(nomeAtual, novoNome));
                                    break;


                                case 3:
                                    string nomeRemover = AnsiConsole.Ask<string>("Nome do espaço a remover: ");
                                    AnsiConsole.WriteLine(conE.removerEspaco(nomeRemover));
                                    break;
                                case 4:
                                    return;
                            }
                    }
                }
        
        public void menuProdutos()
        {
            while (true)
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(
                    new FigletText("PRODUTOS")
                        .Centered()
                        .Color(Color.Blue)); 

                AnsiConsole.Write(new Rule("[blue]Gerenciar Itens do Estoque[/]").Centered());
                AnsiConsole.WriteLine();

                var menuP = new Panel(
                    "[bold cyan]Use as setas ↑ ↓ e ENTER para selecionar[/]");
                menuP.Border = BoxBorder.Rounded;
                menuP.Padding = new Padding(1, 1);
                menuP.BorderStyle = new Style(Color.Grey);
                AnsiConsole.Write(menuP);
                AnsiConsole.WriteLine();

                this.opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int codigo, string descricao)>()
                        .Title("[bold blue]O que deseja fazer com os produtos?[/]")
                        .HighlightStyle(new Style(Color.Cyan1))
                        .PageSize(10)
                        .MoreChoicesText("[grey](Mais opções para baixo)[/]")
                        .UseConverter(x => $"[white]{x.descricao}[/]") 
                        .AddChoices(new (int codigo, string descricao)[]
                        {
                            (1, "Adicionar produto"),
                            (2, "Buscar produto"),
                            (3, "Editar produto"),
                            (4, "Remover produto"),
                            (5, "Voltar ao Menu Principal")
                        })
                ).codigo;

                
                var tabela = new Table();
                //encaminhar pra opcao
                switch (this.opcao)
                {
                    case 1:
                        string nome = "";
                        while (string.IsNullOrWhiteSpace(nome))
                        {
                            nome = AnsiConsole.Ask<string>("Nome do produto: ");
                        }

                        
                        int quantidade = 0;
                        while (quantidade <= 0)
                        {
                            quantidade = AnsiConsole.Ask<int>("Quantidade: ");
                        }
                        
                        decimal pc = AnsiConsole.Prompt(
                            new TextPrompt<decimal>("Preço de compra do produto: ")
                            .ValidationErrorMessage("[red]Por favor, digite um número válido usando ponto (ex: 10.50)[/]")
                            .Validate(valor => valor >= 0 ? ValidationResult.Success() : ValidationResult.Error("O preço não pode ser negativo!"))
                        );

                        decimal pv = AnsiConsole.Prompt(
                            new TextPrompt<decimal>("Preço de venda do produto: ")
                            .ValidationErrorMessage("[red]Por favor, digite um número válido (ex: 20.00)[/]")
                            .Validate(valor => valor >= 0 ? ValidationResult.Success() : ValidationResult.Error("O preço não pode ser negativo!"))
                        );

                        string validade = AnsiConsole.Prompt(
                            new TextPrompt<string>("Validade do produto (dd/mm/aaaa): ")
                            .Validate(input =>
                            {
                                if (!DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                {
                                    return ValidationResult.Error("Data Inválida! Use o formato dd/mm/aaaa");
                                }
                                return ValidationResult.Success();
                        }));

                        string espacoPai = AnsiConsole.Ask<string>("Nome do espaço pai: ");

                        this.conP.criarProduto(nome, quantidade, pc, pv, validade, espacoPai);
                        break;

                    case 2:
                        nome = AnsiConsole.Ask<string>("Nome do produto que deseja procurar: ");
                        List<Produto> produtos = this.conP.buscarProduto(nome);

                        if (produtos.Count == 0)
                        {
                            AnsiConsole.WriteLine("Não foi encontrado o produto desejado!");
                        } else
                        {
                            tabela = new Table();

                            tabela.AddColumn("Nome");
                            tabela.AddColumn("Quantidade");
                            tabela.AddColumn("P. Compra");
                            tabela.AddColumn("P. Venda");
                            tabela.AddColumn("Validade");
                            tabela.AddColumn("Caminho");

                            foreach (var produto in produtos)
                            {
                                tabela.AddRow(
                                    produto.Nome,
                                    produto.Quantidade.ToString(),
                                    $"{produto.PrecoCompra:C2}",
                                    $"{produto.PrecoVenda:C2}",
                                    produto.Validade,
                                    produto.Caminho
                                );
                            }

                            AnsiConsole.Write(tabela);
                            AnsiConsole.WriteLine();
                            AnsiConsole.Prompt(
                            new ConfirmationPrompt("Resultados exibidos. Pressione [yellow]ENTER[/] para voltar ao menu de Produtos.")
                            .ShowChoices(false)
                            .ShowDefaultValue(false));
                        }
                        break;

                    case 3:
                        nome = AnsiConsole.Ask<string>("Nome do produto a editar: ");
                        produtos = this.conP.buscarProduto(nome);
                        int counter = 1;
                        tabela = new Table();
                        tabela.AddColumn("Opção");
                        tabela.AddColumn("Nome");
                        tabela.AddColumn("Quantidade");
                        tabela.AddColumn("P. Compra");
                        tabela.AddColumn("P. Venda");
                        tabela.AddColumn("Validade");
                        tabela.AddColumn("Caminho");

                        foreach (var produto in produtos)
                        {
                            tabela.AddRow(
                                counter.ToString(),
                                produto.Nome,
                                produto.Quantidade.ToString(),
                                $"{produto.PrecoCompra:C2}",
                                $"{produto.PrecoVenda:C2}",
                                produto.Validade,
                                produto.Caminho
                            );
                            counter++;
                        }
                        AnsiConsole.Write(tabela);
                        int indiceProduto = AnsiConsole.Ask<int>("Selecione o número do produto que deseja editar: ");
                        
                        if (indiceProduto - 1 > produtos.Count || indiceProduto < 1)
                        {
                            AnsiConsole.WriteLine("Índice inválido!");
                        } else
                        {
                            Produto produtoEditar = produtos[indiceProduto - 1]; 
                            string novoNome = "";
                            while (string.IsNullOrWhiteSpace(novoNome))
                            {
                                novoNome = AnsiConsole.Ask<string>("Nome do produto: ");
                            }

                            int nova_quantidade = 0;
                            while (nova_quantidade <= 0)
                            {
                                nova_quantidade = AnsiConsole.Ask<int>("Quantidade: ");
                            }
                            
                            decimal novoPc = AnsiConsole.Prompt(
                                new TextPrompt<decimal>("Preço de compra do produto: ")
                                .ValidationErrorMessage("[red]Por favor, digite um número válido usando ponto (ex: 10.50)[/]")
                                .Validate(valor => valor >= 0 ? ValidationResult.Success() : ValidationResult.Error("O preço não pode ser negativo!"))
                            );

                            decimal novoPv = AnsiConsole.Prompt(
                                new TextPrompt<decimal>("Preço de venda do produto: ")
                                .ValidationErrorMessage("[red]Por favor, digite um número válido (ex: 20.00)[/]")
                                .Validate(valor => valor >= 0 ? ValidationResult.Success() : ValidationResult.Error("O preço não pode ser negativo!"))
                            );

                            string novaValidade = AnsiConsole.Prompt(
                                new TextPrompt<string>("Validade do produto (dd/mm/aaaa): ")
                                .Validate(input =>
                                {
                                    if (!DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                                    {
                                        return ValidationResult.Error("Data Inválida! Use o formato dd/mm/aaaa");
                                    }
                                    return ValidationResult.Success();
                            }));
                            AnsiConsole.WriteLine(this.conP.editarProduto(produtoEditar, novoNome, nova_quantidade, novoPc, novoPv, novaValidade));
                            }

                        break;

                    case 4:
                        nome = AnsiConsole.Ask<string>("Nome do produto que deseja remover: ");
                        produtos = this.conP.buscarProduto(nome);

                        counter = 1;
                        tabela = new Table();
                        tabela.AddColumn("Opção");
                        tabela.AddColumn("Nome");
                        tabela.AddColumn("Quantidade");
                        tabela.AddColumn("P. Compra");
                        tabela.AddColumn("P. Venda");
                        tabela.AddColumn("Validade");
                        tabela.AddColumn("Caminho");

                        foreach (var produto in produtos)
                        {
                            tabela.AddRow(
                                counter.ToString(),
                                produto.Nome,
                                produto.Quantidade.ToString(),
                                $"{produto.PrecoCompra:C2}",
                                $"{produto.PrecoVenda:C2}",
                                produto.Validade,
                                produto.Caminho                               
                            );
                            counter++;
                        }
                        AnsiConsole.Write(tabela);
                        indiceProduto = AnsiConsole.Ask<int>("Selecione o número do produto que deseja remover: ");
                        
                         if (indiceProduto - 1 > produtos.Count)
                        {
                            AnsiConsole.WriteLine("Índice inválido!");
                        } else
                        {
                           Produto produtoRemover =  produtos[indiceProduto - 1];
                           int quantidade_remover = AnsiConsole.Ask<int>("Quantidade que deseja remover: ");
                           AnsiConsole.WriteLine(this.conP.removerProduto(produtoRemover, quantidade_remover));   
                        }
                        break;
                    case 5:
                        return;
                }
            }
        }
    }   
}