using System.Data.Common;
using System.Runtime.InteropServices.Swift;
using Projeto_Engenharia.Data;
using Projeto_Engenharia.Model;

namespace Projeto_Engenharia.Controller
{
    class ControladorProdutos
    {
         private BancoDeDados bdd;
         private ControladorEspacos conE;


        public ControladorProdutos(BancoDeDados bdd, ControladorEspacos conE)
        {
            this.bdd = bdd;
            this.conE = conE;
        }
        public string criarProduto(string nome, int quantidade, decimal precoCompra, decimal precoVenda,  string validade, string nomeEspacoPai)
        {
            Espaco? espacoPai = this.conE.buscarEspaco(nomeEspacoPai);

            if (espacoPai is null)
            {
                return "Espaço não encontrado!";
            } else
            {
                return this.bdd.adicionarProduto(nome, quantidade, precoCompra, precoVenda, validade, espacoPai);

            }
        }

        public List<Produto> buscarProduto(string nome)
        {
            List<Produto> produtosComNomeDesejado = this.bdd.buscaProduto(nome);
            return produtosComNomeDesejado;
        }

        public string editarProduto(Produto produto, string nome, int quantidade, decimal pc, decimal pv, string validade)
        {
            return bdd.editarProduto(produto, nome, quantidade, pc, pv, validade);
        }

        public string removerProduto(Produto produto, int quantidade)
        {
            return bdd.removerProduto(produto, quantidade);
        }
    }

     
}