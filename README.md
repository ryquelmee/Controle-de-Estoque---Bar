# StockCenter 🍸

![Status](https://img.shields.io/badge/Status-Prot%C3%B3tipo_Acad%C3%AAmico-yellow)

O StockCenter é um protótipo de sistema para gerenciamento e controle de estoque, focado em facilitar a rotina e a organização de um bar.

Este projeto foi desenvolvido para a disciplina de Engenharia de Software do curso de Análise e Desenvolvimento de Sistemas (ADS).

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Conceitos Aplicados](#-conceitos-aplicados)
- [Tecnologias](#-tecnologias)
- [Como Executar](#%E2%96%B6%EF%B8%8F-como-executar)
- [Autores](#-autores)

## Sobre o Projeto

O objetivo principal do **StockCenter** é oferecer uma forma intuitiva de gerenciar os produtos de um bar. Muitas vezes, os produtos não ficam em um único local, mas sim distribuídos em "espaços" como o bar principal, o estoque seco, a geladeira de cervejas, etc.

Para resolver isso, o sistema foi modelado utilizando uma estrutura de **Árvore Genérica**. Isso permite que o usuário crie espaços e sub-espaços de forma hierárquica (ex: `Estoque` -> `Prateleira A` -> `Caixa 1`), e então associe os produtos a cada um desses locais.

## ✨ Funcionalidades

-   **Gerenciamento de Espaços:** Crie, edite e remova locais de armazenamento de forma hierárquica.
-   **Gerenciamento de Produtos:** Adicione, edite e remova produtos (com nome, quantidade, etc.) em seus respectivos espaços.
-   **Alerta de Estoque Baixo:** O sistema avisa quando um produto específico atinge uma quantidade mínima pré-definida.
-   **Geração de Relatório:** Gera um relatório simples do estado atual de todo o estoque.

## 🔧 Conceitos Aplicados

Como parte dos requisitos acadêmicos, o projeto foca na aplicação prática de conceitos de engenharia de software:

-   **Programação Orientada a Objetos (POO):** Toda a estrutura do sistema é baseada em classes e objetos (como `Produto`, `Espaco`, `Arvore`) para representar as entidades do problema.
-   **Estrutura de Dados (Árvore Genérica):** Utilizada como a principal estrutura para organizar os espaços e os produtos, permitindo flexibilidade no cadastro.

## 🚀 Tecnologias

-   **Python 3.x**
    *(O projeto utiliza apenas bibliotecas padrão do Python)*

## ▶️ Como Executar

Como é um protótipo, o projeto é executado diretamente pelo terminal. Não há necessidade de instalar dependências.

1.  Clone o repositório:
    ```bash
    git clone [https://github.com/SEU-USUARIO/StockCenter.git](https://github.com/SEU-USUARIO/StockCenter.git)
    cd StockCenter
    ```

2.  Execute o arquivo principal:
    ```bash
    python main.py
    ```
    *(**Nota:** Substitua `main.py` pelo nome real do seu arquivo de entrada, como `app.py` ou `run.py`).*

## 👥 Autores

Este projeto foi desenvolvido por:

-   Lucas Henrique
-   Luiz Gustavo
-   Roger Quaresma
-   Ryquelme Rodrigues
