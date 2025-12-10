namespace Projeto_Engenharia.Model
{
    public class Alteracao
{
    public string TipoAlteracao { get; set; }
    public string NomeProduto {get; set; }
    public int Quantidade {get; set; }
    public string Data { get; set; }

    public Alteracao(string tipoAlteracao, string nome, int qnt)
    {
        this.TipoAlteracao = tipoAlteracao;
        this.NomeProduto = nome;
        this.Quantidade = qnt;
        Data = DateTime.Now.ToString("dd/MM/yyyy");
    }
}
        
}