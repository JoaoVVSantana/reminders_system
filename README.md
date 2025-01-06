# Documentação para Executar o Sistema

Esta documentação cobre os passos necessários para configurar, executar e testar o sistema de Gerenciamento de Lembretes.

## Requisitos de Software Necessários

- .NET Core SDK: Versão 6.0 ou superior.
- Node.js: Versão 16 ou superior.
- Gerenciador de Pacotes: NPM ou Yarn.
- Banco de Dados: SQLite (pré-configurado no projeto).

## Frameworks Utilizados

- Backend: ASP.NET Core para a API.
- Entity Framework Core para o banco de dados.
- Frontend: React.js para a interface.
- Axios para comunicação com a API.

## Configuração do Ambiente

1. **Clone o Repositório**
   - `git clone <https://github.com/JoaoVVSantana/reminders_system>`
   
2. **Configuração do Banco de Dados**
   - O projeto utiliza SQLite por padrão.
   - O arquivo do banco será gerado automaticamente no diretório do projeto (`lembretes.db`).

3. **Instalar Dependências do Backend**
   - Acesse o diretório do backend: `cd reminders_system/SistemaLembretes/SistemaLembretes.Server`
   - Execute o comando: `dotnet restore`

4. **Instalar Dependências do Frontend**
   - Acesse o diretório do frontend: `cd reminders_system/SistemaLembretes/SistemaLembretes.Client`
   - Execute o comando: `npm install`

5. **Execução do Backend**
   - Acesse o diretório do backend: `cd reminders_system/SistemaLembretes/SistemaLembretes.Server`
   - Execute o servidor: `dotnet run`
   - O backend estará disponível em: `http://localhost:5000`

6. **Execução do Frontend**
   - Acesse o diretório do frontend: `cd reminders_system/SistemaLembretes/SistemaLembretes.Client`
   - Execute o servidor de desenvolvimento: `npm run dev`
   - A aplicação estará disponível em: `http://localhost:3000`

7. **Testes Automatizados (Backend)**
   - No diretório do backend, execute: `dotnet test`

8. **Testes Automatizados (Frontend)**
   - No diretório do frontend, execute: `npm run test`

## Endpoints da API

- **GET /api/lembrete/todos**: Lista todos os lembretes.
- **GET /api/lembrete/{id}/obterLembrete**: Retorna um lembrete pelo ID.
- **POST /api/lembrete/criarLembrete**: Cria um novo lembrete.
- **DELETE /api/lembrete/{id}/apagarLembrete**: Exclui um lembrete.

## Premissas e Decisões de Projeto

### Estrutura do Backend

Utilizei o padrão arquitetural MVC (Model-View-Controller), com separação adicional de responsabilidades em camadas de serviços, repositórios e validações, visando facilitar a divisão dos componentes e navegação no projeto. Foi dividido em:

- **Configs**: Configuração do framework de entidade Microsoft Entity Framework Core SQLite, com o `DatabaseContext` para gerenciar a conexão com o banco de dados SQLite.
- **Controllers**: Gerencia as rotas da API. O `LembreteController` expõe endpoints RESTful para as operações de criação, leitura, atualização, exclusão e marcação de lembretes como concluídos.
- **Data**: Camada de repositórios que abstrai o acesso do banco de dados, usando o Entity Framework Core. Inclui o `RepositorioLembretes`, que realiza operações de CRUD no banco.
- **Models**: Armazena os modelos, com a classe `Lembrete` definindo os atributos do objeto.
- **Services**: Implementa a lógica no `GerenciadorLembretes`, coordenando as operações entre os repositórios e os controladores, além de validações específicas e manipulação de dados.
- **Tests**: Contém testes automatizados para validar a integridade dos modelos e validações utilizando o framework de testes `XUnit`.

Essa estrutura proporciona menor acoplamento e maior escalabilidade, alinhando-se com boas práticas de desenvolvimento.

### Estrutura do Frontend

Utilizei o framework React.js para a construção do frontend, seguindo a abordagem de uma Single-Page Application (SPA). As principais características incluem:

- Separação de componentes para maior organização e reaproveitamento de código.
- Validação de campos obrigatórios antes de enviar dados para a API.
- Utilização do `Axios` para comunicação com o backend.

