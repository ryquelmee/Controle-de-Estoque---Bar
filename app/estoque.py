from collections import deque
import questionary

class Espaco:
    def __init__(self, nome, caminho = None):
        self.nome = nome
        self.caminho = caminho
        self.subespacos = []


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

    
    def buscar_espaco(self, nome, buscar=False):
        fila = []
        espacos_com_nome_buscado = []
        fila = deque([self.raiz])
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
        espaco_escolhido = questionary.select("Selecione o espaço a remover: ", choices=[espaco.caminho for espaco in espacos_com_nome_buscado]).ask()
        espaco_escolhido.split("/")
        print(espaco_escolhido)
        return



estoque = Estoque(Espaco("Raiz", ""))
        