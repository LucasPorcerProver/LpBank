# Para rodar o projeto siga os seguintes steps:

# Criar Servidor: 
Primeiramente com o Docker Desktop instalado, via command Line acesse a pasta DevOps -> Postgres e execute o comando: docker-compose up -d

Apos a finalizacao do comando, ira ser criado um banco de dados POSTGRESQL que sera utilizado pela API. 

# Criar Base de Dados e Schema: 
Neste servidor, devera ser criado 2 banco de dados com os respectivos nomes: accounts e identity, e em cada um verificar ja esta criado o schema chamado public. 

# Executando a API:
Apos a criacao correta da base de dados, validar a connection string se esta de acordo com os hosts do seu docker local. 

# Criacao de dados:
As Migrations ja estao configuradas para logo que a API iniciar, ja inserir dados no banco de dados, e criar a estrutura de tabelas necessarias, nao sendo necessario atuacao nesta etapa. 

# Collection para testes:
A collection para testes esta disponibilizada na pasta Postman podendo ser importada

# Dados de OAuth:
A API esta configurada para ter seguranca OAuth, necessitando logar no controller Authentication.
Dados para login estao disponibilizados na collection na pasta: Postman.

# Estrutura:
A estrutura de API, esta com uma padrao que geralmente utilizo nos meus projetos pessoais, logicamente isto nao se aplica a todos os projetos que trabalho e posso facilmente me adaptar a arquitetura ja definida anteriormente.

# Testes Unitarios:
Um ponto importante a ressaltar, os testes de unidade estao desenvolvidos de 2 formas para avalizacao, realizando mock direto no teste, ou realizando o mock em uma classe externa.