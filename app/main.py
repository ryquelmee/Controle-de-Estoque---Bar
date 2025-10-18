import questionary, os

def limpar_tela():
    os.system('cls' if os.name == 'nt' else 'clear')

usuario = ""
senha = ""
while usuario != "admin" or senha != "admin123":
    usuario = input("Usuário: ")
    senha = input("Senha: ")
    limpar_tela()

class Menu:
    def __init__(self):
        pass



    def mostrar_menu(self):
        print("╔══════════════════════════════════════╗")
        print("║" + "STOCK CENTER SYSTEM".center(38) + "║")
        print("╚══════════════════════════════════════╝\n")
        print("╔══════════════════════════════════════╗")
        print("║" + "MENU PRINCIPAL".center(38) + "║") #o .center centralize a string com base na quantidade de casas colocada como argumento
        print("║" + " ".center(38) + "║")
        print("║" + "1 - Operações de estoque 🔧".center(37) + "║")
        print("║" + "2 - Estrutura de estoque ⚙️".center(39) + "║")
        print("║" + "3 - Inventário Atual 📊".center(37) + "║")
        print("║" + "4 - Gerar relatório".center(38) + "║")
        print("║" + "5 - Encerrar sessão 🚪".center(37) + "║")
        print("╚══════════════════════════════════════╝\n")


    def selecionar_acao(self, acao):
        if acao == '1':
            pass
        elif acao == '2':
            pass
        elif acao == '3':
            pass
        elif acao == '4':
            pass
        elif acao == '5':
            pass


while True:
    menu_principal = Menu()
    limpar_tela()
    menu_principal.mostrar_menu()

    acao = questionary.select('Escolha uma opção:', choices=[
            '1',
            '2',
            '3',
            '4',
            '5'
        ]).ask()

    menu_principal.selecionar_acao(acao)