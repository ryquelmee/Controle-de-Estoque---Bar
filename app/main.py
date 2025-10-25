import questionary, os
from estoque import estoque

def limpar_tela():
    os.system('cls' if os.name == 'nt' else 'clear')


usuario = ""
senha = ""
while usuario != "admin" or senha != "admin123":
    usuario = input("Usuário: ")
    senha = input("Senha: ")
    limpar_tela()



def menu_principal():
    limpar_tela()
    while True:
        print("╔══════════════════════════════════════╗")
        print("║" + "STOCK CENTER SYSTEM".center(38) + "║")
        print("╚══════════════════════════════════════╝\n")
        print("╔══════════════════════════════════════╗")
        print("║" + "MENU PRINCIPAL".center(38) + "║") #o .center centralize a string com base na quantidade de casas colocada como argumento
        print("║" + " ".center(38) + "║")
        print("║" + "1 - Gerenciar estoque 🔧".center(37) + "║")
        print("║" + "2 - Configurar estoque ⚙️".center(39) + "║")
        print("║" + "3 - Inventário Atual 📊".center(37) + "║")
        print("║" + "4 - Gerar relatório".center(38) + "║")
        print("║" + "5 - Encerrar sessão 🚪".center(37) + "║")
        print("╚══════════════════════════════════════╝\n")

        acao = questionary.select('Escolha uma opção:', choices=[
            '1',
            '2',
            '3',
            '4',
            '5'
        ]).ask()

        if acao == '1':
            menu_gerenciar()
        elif acao == '2':
            menu_configurar()
        elif acao == '3':
            estoque.percorrer_estoque()
        elif acao == '4':
            pass
        elif acao == '5':
            exit()

def menu_gerenciar():
    limpar_tela()
    while True:
        
        print("╔══════════════════════════════════════╗")
        print("║" + "OPERACOES DE ESTOQUE 🔧".center(37) + "║")
        print("╚══════════════════════════════════════╝\n")
        print("╔══════════════════════════════════════╗")
        print("║" + "1 - Adicionar produto ➕".center(37) + "║") #adicionar
        print("║" + "2 - Remover produto ❌".center(37) + "║") #remover
        print("║" + "3 - Buscar produto 🔍".center(37) + "║") #buscar
        print("║" + "4 - Atualizar produto ✏️".center(39) + "║") #editar
        print("║" + "5 - Voltar 🔙".center(37) + "║") #sair
        print("╚══════════════════════════════════════╝\n")

        acao = questionary.select('Escolha uma opção:', choices=[
            '1',
            '2',
            '3',
            '4',
            '5'
        ]).ask()

        if acao == '1':
            pass
        elif acao == '2':
            pass
        elif acao == '3':
            pass
        elif acao == '4':
            pass
        elif acao == '5':
            return

def menu_configurar():
    limpar_tela()
    while True: 
        print("╔══════════════════════════════════════╗")
        print("║" + "CONFIGURAR ESTOQUE ⚙️".center(39) + "║")
        print("╚══════════════════════════════════════╝\n")
        print("╔══════════════════════════════════════╗")
        print("║" + "1 - Criar espaço🏠".center(37) + "║") #adicionar
        print("║" + "2 - Editar espaço🛠".center(38) + "║") #editar
        print("║" + "3 - Excluir espaço❌".center(37) + "║") #excluir
        print("║" + "4 - Buscar espaço🔎".center(37) + "║") #buscar
        print("║" + "5 - Voltar 🔙".center(37) + "║") #sair
        print("╚══════════════════════════════════════╝\n")
    
        acao = questionary.select('Escolha uma opção:', choices=[
            '1',
            '2',
            '3',
            '4',
            '5'
        ]).ask()
        
        if acao == '1':
            nome_espaco = input("Digite o nome do espaço a ser criado: ")
            if not nome_espaco.strip():
                print("Nome inválido!")
            else:
                estoque.adicionar_espaco(nome_espaco)

        elif acao == '2':
            pass
        elif acao == '3':
            pass
        elif acao == '4':
            pass
        elif acao == '5':
            return
        
menu_principal()