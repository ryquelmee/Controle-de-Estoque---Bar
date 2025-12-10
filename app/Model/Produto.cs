namespace Projeto_Engenharia.Model
{
    public class Produto {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public string Caminho { get; set; }
        public string Validade { get; set; }
      
        public Produto(string nome, int quantidade, decimal precoCompra, decimal precoVenda, string validade, string caminho)
        {
            this.Nome = nome;
            this.Quantidade = quantidade;
            this.PrecoCompra = precoCompra;
            this.PrecoVenda = precoVenda;
            this.Caminho = caminho;
            this.Validade = validade;
        }  
    };      
}