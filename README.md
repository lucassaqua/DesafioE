# DesafioE

Este foi um desafio de código que recebi durante um processo seletivo. Deixarei documentado o desafio proposto e a seguir, abordagem que segui para conclui-lo.


# -----------------        Desafio        ------------------- 

Sumário
Sumário.............................................................................................................................................................................2
1. Objetivo.....................................................................................................................................................................4
1.1. Desenvolver um webcrawler. ...........................................................................................................................4
2. Passos........................................................................................................................................................................4
2.1. Itens...................................................................................................................................................................4
2.1.1. Acessar o site "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc". ...............................4
2.1.2. Extrair os campos "IP Adress", "Port", "Country" e "Protocol". de todas as linhas, de todas as páginas
disponíveis na execução. ..............................................................................................................................................4
2.1.3. Necessário salvar o resultado da extração em arquivo json, que deverá ser salvo na máquina. ................4
2.1.4. Necessário salvar em banco de dados a data início execução, data termino execução, quantidade de
páginas, quantidade linhas extraídas em todas as páginas e arquivo json gerado. .....................................................4
2.1.5. Necessário print (arquivo .html) de cada página..........................................................................................4
2.1.6. Necessário que o webcrawler seja multithread, com máximo de 3 execuções simultâneas.......................4


# ------------------      Solução        -------------------- 

1 - Criei um projeto modelo, um CRUD genérico com arquitetura MVC e conexão a um banco de dados SQL Server.

2 - No site de proxys, abri a guia do desenvolvedor no navegador e, na aba Network, localizei a requisição HTTP feita ao servidor, após eu marcar a opção de país como Alemanha e apertar o botão "MOSTRAR LISTA DE SERVIDORES PROXY GRATUITOS" no site.
    Dessa maneira, consegui a URL da requisição ( https://fineproxy.org/wp-content/themes/fineproxyorg/proxy-list.php?0.2683192644552712&country_codes%5B%5D=DE ), que pode ser vista na aba Headers e, também o array com os dados que populam a grid do site, que pode ser visto na aba Response.
    Como o site não permite o consumo da api deles de forma direta, usando a URL, eu criei um documento .json localmente chamado dataEntrada.json ( está localizado na pasta Data ), colei o array de dados que peguei no navegador dele e consumi os dados deste documento.
    Com estas informações em mãos, eu adaptei o meu projeto, criando um endpoint GetRegisters que lê o arquivo dataEntrada.json, e então cria localmente um arquivo dataSaida.json com as informações que foram solicitadas no desafio. Além disso, é feito um registro no banco de dados do conteudo de dataSaida.json e das demais informações solicitadas e também, criados os arquivos .html das páginas.

    Importante: O caminho para os arquivos dataEntrada.json, dataSaida.json e .html precisam ser definidos nos locais indicados na Controller e no Service.
    Eu criei a pasta Data para facilitar a visualisação da criação nos arquivos, mas você pode criá-los em outra pasta, se preferir.
    Também é necessário definir a ServerConnection, no appsettings.json.

4 - Um adendo:
Sabendo que por padrão a grid do site exibe 20 itens do array por página, eu defini a quantidade de páginas a ser salva no banco de dados, como o número total de intens do array dividido por 20, se o resto desta divisão for zero. Senão, será o número de intens divido por 20, mais um.


# - Arquitetura do projeto e dependencias 
Para este projeto, usei o padrão de arquitetura MVC, aplicando conceitos de DDD. 

Criei uma pasta Controller, onde além do Endpoint que le os dados e executa o desafio ( o POST ), eu criei um get; um getById; um put; e um Delete.

Eu incluí no projeto a pasta Data, que incluí o arquivo dataEntrada.json com os dados dos proxys e a pasta Paginas HTML, que setei para receber os 
arquivos .html gerados após a execução do projeto, por padrão. Mas se quiser, você pode excluir esta pasta e setar nos locais indicados na controller e no service, o caminho para o json com os dados a serem lidos, o caminho onde deve ser salvos os arquivos .html e o arquivo json gerado pela solução.

Para este projeto, usei a versão ASP .NET Core 5.0 e as seguintes dependências:
- Microsoft.EntityFrameworkCore Version="5.0.3"
   Que usei ferramenta ORM.

- Microsoft.EntityFrameworkCore.Design" Version="5.0.3"
  Que usei para ter acesso a recursos do Scaffold.

- Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.3"
  Que usei para fazer a conexão entre o projeto e o sql server.

- Microsoft.EntityFrameworkCore.Tools" Version="5.0.3"
  Para executar os comandos de criação da migration e atuaização do banco de dados.

-AutoMapper.Extensions.Microsoft.DependencyInjection Version="12.0.1"
  Que usei para fazer o mapemento entre a entidade de proxy e o model.

Usei também o SQL Server e SQL Server Management Studio v17 para visualizar o banco de dados e fazer consultas.


