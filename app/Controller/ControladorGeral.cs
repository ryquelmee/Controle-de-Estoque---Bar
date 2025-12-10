using Projeto_Engenharia.Data;
using Projeto_Engenharia.Model;
using Spectre.Console;

namespace Projeto_Engenharia.Controller
{
    class ControladorGeral 
    {
        private BancoDeDados bdd;

        public ControladorGeral(BancoDeDados bdd)
        {
            this.bdd = bdd;
        }

        public List<Espaco> mostrarEstoque()
        {
            return bdd.ListaEspacos;
        }

        public Relatorio gerarRelatorio(string dtInicial, string dtFinal)
        {
            return this.bdd.gerarRelatorio(dtInicial, dtFinal);
        }
    }
}