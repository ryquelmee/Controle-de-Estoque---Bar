namespace Projeto_Engenharia.Model
{
    public class Relatorio
{
    public string dataInicial { get; set; }

    public string dataFinal {get; set; }

    public List<Alteracao> ListaAlteracoes { get; set; } = new List<Alteracao>();

    public Relatorio(string dtInicial, string dtFinal, List<Alteracao> alteracoes)
        {
            this.dataInicial = dtInicial;
            this.dataFinal = dtFinal;
            this.ListaAlteracoes = alteracoes;
        }
}

        
}