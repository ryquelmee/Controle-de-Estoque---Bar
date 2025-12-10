using System.Data.Common;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Projeto_Engenharia.Model;
using Spectre.Console;
using Spectre.Console.Rendering;
namespace Projeto_Engenharia.Data
{
    class BancoDeDados
    {
        public List<Espaco> ListaEspacos { get; set; } = new List<Espaco>();
        public List<Alteracao> ListaAlteracoes { get; set; } = new List<Alteracao>();

        public BancoDeDados()
        {
            this.ListaEspacos.Add(new Espaco("estoque", "estoque/"));
        }

        public string adicionarEspaco(string nome, string nomeEspacoPai)
        {
            string caminho = "";
            foreach (Espaco espaco in ListaEspacos)
            {
                if (espaco.Nome == nomeEspacoPai)
                {
                    caminho = espaco.Caminho + nome + "/";
                }
            }
                
                Espaco novoEspaco = new Espaco(nome, caminho);
                this.ListaEspacos.Add(novoEspaco);
                
            return "Espaço criado com sucesso!";
            }
            
        public Espaco? obtemEspaco(string nome)
        {
            foreach (Espaco espaco in this.ListaEspacos)
            {
                if (espaco.Nome == nome)
                {
                    return espaco;
                }
            }
            return null;
        }

        public string editarEspaco(string nomeAtual, string novoNome)
        {
            Espaco espacoEditado = this.obtemEspaco(nomeAtual);
            espacoEditado.Nome = novoNome; 
            espacoEditado.Caminho = espacoEditado.Caminho.Replace(nomeAtual, novoNome);

            foreach(Produto produto in espacoEditado.produtos)
            {
                produto.Caminho = espacoEditado.Caminho;
            }

            foreach (Espaco espaco in this.ListaEspacos) {
                if (espaco.Caminho.Contains(nomeAtual)) {
                    espaco.Caminho = espaco.Caminho.Replace(nomeAtual, novoNome);
                }
            }
            return "Espaço editado com sucesso!";
        }

        public string removerEspaco(string nome)
        {
            foreach (Espaco espaco in ListaEspacos)
            {
                if (espaco.Nome == nome)
                {
                    foreach(Produto produto in espaco.produtos)
                    {
                        Alteracao novaAlteracao = new Alteracao("Remover", produto.Nome, produto.Quantidade);
                        ListaAlteracoes.Add(novaAlteracao);
                    }
                }
            }
            this.ListaEspacos.RemoveAll(espaco => espaco.Caminho.Contains(nome));
            return "Espaco Removido com sucesso!";
        }

        

        public string adicionarProduto(string nome, int quantidade, decimal precoCompra, decimal precoVenda, string validade, Espaco espacoPai)
        {      
            string caminho = espacoPai.Caminho;
            Produto novoProduto = new Produto(nome, quantidade, precoCompra, precoVenda, validade, caminho);
            espacoPai.produtos.Add(novoProduto);

            Alteracao novaAlteracao = new Alteracao("Adicionar", novoProduto.Nome, novoProduto.Quantidade);
            this.guardaAlteracao(novaAlteracao);
            
            return "Produto criado com sucesso!";
        } 

        public List<Produto> buscaProduto(string nome)
        {
            List<Produto> produtosComNomeDesejado = new List<Produto>();

            foreach (Espaco espaco in this.ListaEspacos)
            {
                foreach(Produto produto in espaco.produtos)
                {
                    if (produto.Nome == nome)
                    {
                        produtosComNomeDesejado.Add(produto);
                    }
                }
            }
            return produtosComNomeDesejado;

        }

        public string editarProduto(Produto produto, string novo_nome, int nova_quantidade, decimal novo_precoCompra, decimal novo_precoVenda, string nova_validade)
        {
            produto.Nome = novo_nome;
            produto.Quantidade = nova_quantidade;
            produto.PrecoCompra = novo_precoCompra;
            produto.PrecoVenda = novo_precoVenda;
            produto.Validade = nova_validade;
            return "Produto editado com sucesso!";
        }

        public string removerProduto(Produto produtoRemover, int quantidade)
        {
            if (produtoRemover.Quantidade < quantidade)
            {
                return "Não há produtos suficientes pra remover";
            } else if (produtoRemover.Quantidade > quantidade)
            {
                produtoRemover.Quantidade = produtoRemover.Quantidade - quantidade;
                Alteracao novaAlteracao = new Alteracao("Remover", produtoRemover.Nome, quantidade);
                this.guardaAlteracao(novaAlteracao);
                return "Produtos removido com sucesso";
            } else {
            foreach (Espaco espaco in this.ListaEspacos)
            {
                espaco.produtos.RemoveAll(produto => produto == produtoRemover);
            }
            Alteracao novaAlteracao = new Alteracao("Remover", produtoRemover.Nome, quantidade);
            this.guardaAlteracao(novaAlteracao);

            return "Produto removido com sucesso!";
        }}

        public void guardaAlteracao(Alteracao alteracao)
        {
            this.ListaAlteracoes.Add(alteracao);
        }

        public Relatorio gerarRelatorio(string strdtInicial, string strdtFinal)
        {
            List<Alteracao> Alteracoes = new List<Alteracao>();
            DateTime dtInicial = DateTime.ParseExact(strdtInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtFinal = DateTime.ParseExact(strdtFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            foreach (Alteracao alteracao in ListaAlteracoes) {
                DateTime data_alteracao = DateTime.ParseExact(alteracao.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (data_alteracao <= dtFinal && data_alteracao >= dtInicial)
                {
                    Alteracoes.Add(alteracao);
                }
            }
            return new Relatorio(strdtInicial, strdtFinal, Alteracoes);
        }
    }
}