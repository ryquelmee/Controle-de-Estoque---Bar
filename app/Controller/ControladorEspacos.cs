using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Projeto_Engenharia.Model;
using Projeto_Engenharia.Data;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;


namespace Projeto_Engenharia.Controller
{
    class ControladorEspacos
    {
        private BancoDeDados bdd;
        
        public ControladorEspacos(BancoDeDados bdd)
        {
            this.bdd = bdd;
        }

        public string criarEspaco(string nome, string nomeEspacoPai)
        {
            Espaco espaco = bdd.obtemEspaco(nome);
            if (espaco is null)
            {
                Espaco espacoPai = bdd.obtemEspaco(nomeEspacoPai);
                if (espacoPai is not null)
                {
                    bdd.adicionarEspaco(nome, nomeEspacoPai);
                    return "Espaço criado com sucesso!";
                } else
                {
                    return "Não existe espaço pai com esse nome";
                }
            } else
            {
                return "Nome do espaço já existe!";
            }
            
        }
        public Espaco? buscarEspaco(string nome)
        {
            return bdd.obtemEspaco(nome);
            
        }
        public string editarEspaco(string nomeAtual, string novoNome )
        {
            Espaco espaco = this.buscarEspaco(nomeAtual);
            Espaco espaco_editado = this.buscarEspaco(novoNome);
            if (espaco is null || espaco_editado is not null)
            {
                return "Espaço inexistente ou espaço pai não existente!";
            } else
            {
                return bdd.editarEspaco(nomeAtual, novoNome);
            }
        }

        public string removerEspaco(string nome)
        {
            Espaco espaco = this.buscarEspaco(nome);
            if (espaco is null)
            {
                return "Não existe espaço com esse nome!";
            } else
            {
                return bdd.removerEspaco(nome);
            }
        }

    }
}