## 🌟 ASP.NET Core Web API - Desafio Técnico Back-End

![Logo level up](https://avatars.githubusercontent.com/u/83302250?s=200&v=4)


### 📃 Descrição

Esta API RESTful recomenda jogos gratuitos com base nos gostos do usuário, incluindo gêneros, plataforma e memória RAM disponível.

### ✅ Pré-requisitos

- **VSCode** ou outra IDE de sua preferência (Plugin .Net e C#).
- **.NET 8** instalado.
- **SQLite** ou outro banco de dados compatível com Entity Framework.
- **Entity Framework** ORM para interagir com banco de dados usando objetos .NET
- **Pacotes NuGet**
    - *Para ORM:* 
        - Microsoft.EntityFrameworkCore.Sqlite
        - Microsoft.EntityFrameworkCore.Tools
    - *Para requisições HTTP:*
        - Microsoft.AspNet.WebApi.Client
    - *Para documentação Swagger:*
        - Swashbuckle.AspNetCore

---
---

### 🚀 Rodando API Localmente

#### Passo 1: Clonando Repositório para sua máquina local:

    git clone https://github.com/wsawebmaster/GameRecommendationAPI.git
    cd GameRecommendationAPI

#### Passo 2: Iniciando .Net Entity Framework:

    dotnet tool install --global dotnet-ef

#### Passo 3: Instalando Dependências:

    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add package Microsoft.AspNet.WebApi.Client
    dotnet add package Swashbuckle.AspNetCore

#### Passo 4: Executando a aplicação:

    dotnet run

### 🌐 Acessar API

 - [URL da API padrão](http://localhost:5074/)
 - [URL da documentação Swagger](http://localhost:5074/swagger/index.html)

#### Sugestão de comando caso queira desenvolver do absoluto zero para implementar manualmente

    dotnet new webapi --name GameRecommendationAPI -minimal

### 📚 Referências

- [VS Code](https://code.visualstudio.com/) - IDE para desenvolvimento
- [.NET](https://dotnet.microsoft.com/en-us/download) - SDK do .NET
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) - Banco de Dados usado
- [.NET ef tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) - Conjunto de ferramentas
- [NetEntityFramework](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) - ORM que facilita interação com Banco de dados
- [NugetPackages](https://www.nuget.org/packages) - Gerenciador de pacotes oficial para o .NET
- [GitignoreIo](https://www.toptal.com/developers/gitignore) - Criar conteúdo .gitignore


---
---

### 👨‍💻 Expert

<p>
<img 
      align="left" 
      style="margin: 10px; width: 80px; border-radius: 50%;" 
      src="https://avatars.githubusercontent.com/u/52001930?s=400&u=fb999c966c5c652a8357cbede4b1112e79cbfe18&v=4" 
/>
    <p style="padding-top:25px">&nbsp&nbsp&nbsp Wagner Andrade<br>
    &nbsp&nbsp&nbsp
    <a href="https://github.com/wsawebmaster">
    GitHub</a>&nbsp;|&nbsp;
    <a href="https://www.linkedin.com/in/
wsawebmaster">LinkedIn</a>
&nbsp;|&nbsp;
<a href="mailto:wsawebmaster@yahoo.com.br">
    Email</a>
  &nbsp;|&nbsp;
</p>
</p>