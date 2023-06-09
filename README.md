# Desafio WLABS - C# e Mongodb

## Tópicos

:small_blue_diamond: [Descrição do Projeto](#descrição-do-projeto)

:small_blue_diamond: [Tecnologias Utilizadas](#tecnologias-utilizadas)

:small_blue_diamond: [Funcionalidades](#Funcionalidades)

:small_blue_diamond: [Configuração](#Configuração)

:small_blue_diamond: [Endpoints](#Endpoints)

:small_blue_diamond: [Desenvolvedores](#desenvolvedor(es))

## Descrição do Projeto

Este é o projeto de uma API desenvolvida para o desafio da WLABS. A API é responsável por buscar informações de endereço com base em um CEP fornecido.

## Teconologias utilizadas

- .NET Core 7
- MongoDB
- HttpClient

## Funcionalidades

A API possui as seguintes funcionalidades:

- Realizar a busca de informações de endereço com base em um CEP.
- Utilizar múltiplas APIs de consulta de CEP para obter as informações necessárias.
  - [ViaCep](https://viacep.com.br/)
  - [ApiCEP](https://apicep.com/api-de-consulta/)   
  - [AwesomeAPI](https://docs.awesomeapi.com.br/api-cep)
- Registrar logs dos erros ocorridos.

## Configuração

Para executar a aplicação, siga as instruções abaixo:

- Certifique-se de ter o .NET Core SDK instalado em sua máquina.
- Clone este repositório para o seu ambiente local.
- Abra o projeto em uma IDE de sua preferência.
- Verifique as configurações do banco de dados MongoDB no arquivo appsettings.json e ajuste conforme necessário.
- Execute o projeto para iniciar a API.

## Endpoints

A API possui o seguinte endpoint:

- GET /api/endereco/get?cep={cep} 

Realiza a busca de informações de endereço com base no CEP fornecido.

## Desenvolvedor(es)

| [<img src="https://avatars.githubusercontent.com/u/58979991?v=4" width=115><br><sub>Gabriel Carvalho</sub>](https://github.com/GabCarvaS) |
| :---------------------------------------------------------------------------------------------------------------------------------------: |
