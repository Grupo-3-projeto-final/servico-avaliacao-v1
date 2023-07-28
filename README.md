# Serviço Avaliação


## Descrição

O Serviço de Avaliação é uma aplicação desenvolvida em .NET Framework 4.5 que consulta uma fila SQS Aws e calcular médias e verificar a aprovação no curso. O projeto segue o padrão DDD (Domain-Driven Design) para organizar a estrutura e separar as responsabilidades.

## Funcionalidades

- Cálculo da média das notas de um aluno em um curso específica.
- Verificação da aprovação do aluno em um curso, com base na média das notas.

## Tecnologias Utilizadas

- .NET Framework 4.5: Plataforma para o desenvolvimento do serviço.
- AWS SQS: Serviço de filas para processamento de mensagens.
- ASP.NET Web API: Framework para a criação de APIs web no .NET Framework.

## Configuração do Ambiente

Antes de executar o projeto, siga os passos abaixo para configurar o ambiente de desenvolvimento:

1. Verifique se você tem o .NET Framework 4.5 instalado em sua máquina. Caso não tenha, você pode baixá-lo em: [https://www.microsoft.com/net/download](https://www.microsoft.com/net/download)

2. Configure a AWS e crie uma fila SQS para receber as mensagens do serviço.

## Instalação e Execução

Siga os passos abaixo para executar o projeto localmente:

1. Clone este repositório para sua máquina:

```bash
git clone https://github.com/Grupo-3-projeto-final/servico-avaliacao.git

