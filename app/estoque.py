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
        fila = deque([self.raiz])

        while fila:
            espaco_atual = fila.popleft()

            print(espaco_atual.caminho)

            for subespaco in espaco_atual.subespacos:
                fila.append(subespaco)


    def adicionar_espaco(self, nome):
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
estoque = Estoque(Espaco("Raiz", ""))
        