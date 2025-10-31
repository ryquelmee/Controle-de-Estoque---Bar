from collections import deque
import questionary




class Espaco:
    def __init__(self, nome, caminho = None):
        self.nome = nome
        self.caminho = caminho
        self.subespacos = []

class Produto:
    def __init__(self, nome, vc, vv, validade, caminho = None):
        self.codigo = None
        self.nome = nome
        self.validade = validade
        self.pc = vc
        self.pv = vv
        self.caminho = caminho


class Estoque:
    def __init__(self, raiz):
        self.raiz = raiz

    def percorrer_estoque(self):
        fila = []
        fila = deque([self.raiz])
        while fila:
            espaco_atual = fila.popleft()
            print(espaco_atual.caminho)
            for subespaco in espaco_atual.subespacos:
                fila.append(subespaco)


    def adicionar_espaco(self, nome):
        fila = []
        fila = deque([self.raiz])
        while fila:
            
            espaco_atual = fila.popleft()
            subespacos_atuais = [subespaco for subespaco in espaco_atual.subespacos]

            espaco_escolhido = questionary.select("Selecione o espaço: ", choices=["Local atual"] + [subespaco.nome for subespaco in subespacos_atuais]).ask()
            caminho = (espaco_atual.caminho or "") + "/" + nome
            if espaco_escolhido == "Local atual":
                espaco_atual.subespacos.append(Espaco(nome, caminho))
                return
            else:
                for subespaco in espaco_atual.subespacos:
                    if subespaco.nome == espaco_escolhido:
                        fila.append(subespaco)
                        caminho = subespaco.caminho
                        break

    
    def buscar_espaco(self, nome, buscar=False, buscar_com_caminho=False):
        fila = []
        espacos_com_nome_buscado = []
        fila = deque([self.raiz])
            
        if buscar_com_caminho:
            espaco_atual = self.raiz

            for espaco in nome:
                for subespaco_atual in espaco_atual.subespacos:
                    if subespaco_atual.nome == espaco:
                        espaco_atual = subespaco_atual
                        break
            
            return espaco_atual
        else:
            while fila:
                espaco_atual = fila.popleft()
                if espaco_atual.nome == nome:
                    espacos_com_nome_buscado.append(espaco_atual)
                for subespaco in espaco_atual.subespacos:
                    fila.append(subespaco)

            if buscar:
                for espaco in espacos_com_nome_buscado:
                    print(espaco.caminho)
                return

            else: 
                return espacos_com_nome_buscado

    def remover_espaco(self, nome):
        espacos_com_nome_buscado = self.buscar_espaco(nome)
        if not espacos_com_nome_buscado:
            print("Espaço não encontrado!")
            return
        espaco_escolhido = questionary.select("Selecione o espaço a remover: ", choices=[espaco.caminho for espaco in espacos_com_nome_buscado]).ask()
        nome_do_caminho_a_remover = espaco_escolhido.split("/")[-1]
        espaco_escolhido = espaco_escolhido.split("/")[:-1]

        espaco_pai_do_escolhido = self.buscar_espaco(espaco_escolhido, buscar_com_caminho=True)
        
        for subespaco in espaco_pai_do_escolhido.subespacos:
            if subespaco.nome == nome_do_caminho_a_remover:
                espaco_pai_do_escolhido.subespacos.remove(subespaco)
                break
        return f"Espaço {nome_do_caminho_a_remover} removido com sucesso"

    
    def editar_espaco(self, nome):
        espacos_com_nome_buscado = self.buscar_espaco(nome)
        if not espacos_com_nome_buscado:
            print("Espaço não encontrado!")
            return
        espaco_escolhido = questionary.select("Selecione o espaço a editar: ", choices=[espaco.caminho for espaco in espacos_com_nome_buscado]).ask()
        nome_do_caminho_a_editar = espaco_escolhido.split("/")[-1]
        espaco_escolhido = espaco_escolhido.split("/")[:-1]

        espaco_pai_do_escolhido = self.buscar_espaco(espaco_escolhido, buscar_com_caminho=True)

        for subespaco in espaco_pai_do_escolhido.subespacos:
            if subespaco.nome == nome_do_caminho_a_editar:
                novo_nome = ""
                while not novo_nome:
                    novo_nome = str(input("Digíte o novo nome do espaço(não pode ser vazio!): "))
                subespaco.nome = novo_nome
                caminho_antigo = subespaco.caminho.split("/")[:-1]
                subespaco.caminho = ""
                for espaco in caminho_antigo:
                    subespaco.caminho += f"{espaco}/"
                
                subespaco.caminho += f"{novo_nome}"
                break
        print("Espaço editado com sucesso!")


estoque = Estoque(Espaco("Raiz", ""))
        