# Demo Exchange

Projeto destinado a criar um serviço para expor endpoints e realizar cotações de moeda estrangeira para o Real, contemplando os segmentos Varejo, Private e Personnalite.

## Imagens Docker
Para o projeto foram usadas as seguintes imagens:

Para o banco de dados, foi utilizado o MariaDB

`docker run --name mariadb -e MYSQL_ROOT_PASSWORD=admin -d -p 3306:3306 mariadb`

**Comando acima irá expor a porta 3306 para utilizar instancias do MariaDB**
**Recomandado utilizar o client DBeaver para administração do banco dedados https://dbeaver.io/**

Para o mecanismo de cache, foi utilizado Memcached

`docker run -p 11211:11211 -d memcached`

## Rodar as aplicaçãos com Docker Compose
A solução conta com a opção de rodar todos os serviços utilizando docker-compose:

Para iniciar todos os serviços você deve executar os seguintes comandos: 

`docker-compose build --force-rm --pull`

Comando que irá realizar o pull de todas as imagens relacionados ao serviço.

`docker-compose up -d`

Comando que irá iniciar todos os serviços com as mesmas portas do comandos para o docker descritos acima.
Obs: Você pode executar apenas esse comando, que o mesmo também irá fazer o pull das imagens caso não sejam localizadas na máquina.

**WARNING**

Antes de consultar os endpoinst do serviço será necessário executar o script de criação de banco dados que está na pasta demo-exchange/src/Demo.Exchange.Api/Infra/Repositories/Scripts.

## Packages
As aplicações fazem uso dos seguintes packages

https://www.nuget.org/packages/EnyimMemcachedCore/ : Bliblioteca que abstrai utilização do Memcached

https://www.nuget.org/packages/MySqlConnector/ : Bliblioteca que abstrai para utilização do MariDB

## Fontes de referência
https://www.twitch.tv/phatboyg

Canal do Twitch do criador do Masstransit

https://jimmybogard.com/

Criador dos pacotes MediatR e AutoMapper além de ótimas apresensetações no NDC.

https://www.stevejgordon.co.uk/

Blog voltado a performance de aplicações ASP.NET Core

https://dotnet.microsoft.com/learn/dotnet/architecture-guides

Ótimos exemplos de como criar a realizar deploy de aplicação com docker e desenvolvimento de microsserviços

https://d0.awsstatic.com/whitepapers/performance-at-scale-with-amazon-elasticache.pdf

Documetanção da Amazon para uso de estratégias de cache

https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md#3-introduction

Guidelines para criação de aplicações REST.
