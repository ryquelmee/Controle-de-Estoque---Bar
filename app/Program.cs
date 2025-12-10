using Projeto_Engenharia.View;
using Projeto_Engenharia.Controller;
using Projeto_Engenharia.Model;
using Projeto_Engenharia.Data;
using Spectre.Console;

string usuario = AnsiConsole.Ask<string>("Usuário: ");
string senha = AnsiConsole.Prompt(
    new TextPrompt<string> ("Senha: ")
    .Secret()
);

while (usuario != "admin" || senha != "12345")
{
    AnsiConsole.WriteLine("Credenciais inválidas!\n");
    usuario = AnsiConsole.Ask<string>("Usuário: ");
    senha = AnsiConsole.Prompt(
    new TextPrompt<string> ("Senha: ")
    .Secret()
);
}

BancoDeDados bdd = new BancoDeDados();
ControladorEspacos conE = new ControladorEspacos(bdd);
ControladorProdutos conP = new ControladorProdutos(bdd, conE);
ControladorGeral conG = new ControladorGeral(bdd);
InterfaceUsuario iu = new InterfaceUsuario(conE, conP, conG);

iu.menuPrincipal();

