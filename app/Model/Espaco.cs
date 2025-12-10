namespace Projeto_Engenharia.Model
{
    public class Espaco
{
    public string Nome { get; set; }
    public string Caminho { get; set; }

    public List<Produto> produtos = new List<Produto>();


    public Espaco(string nome, string caminho)
        {
            this.Nome = nome;
            this.Caminho = caminho;
        }

}
        
}