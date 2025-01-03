# Documentação para Executar o Sistema
Esta documentação cobre os passos necessários para configurar, executar e testar o sistema .
 ## Requisitos de Software Necessários
 - .NET Core SDK: Versão 6.0 ou superior. 
 -  Node.js: Versão 16 ou superior. 
 -  Gerenciador de Pacotes: NPM ou Yarn. 
 - Banco de Dados: SQLite (pré-configurado no projeto). 
 - Navegador Moderno: Para acessar a interface web. Bibliotecas e 
 ## Frameworks Utilizados 
 - Backend:  ASP.NET Core para a API. 
 - Entity Framework Core para o banco de dados. 
  Frontend:  React.js para a interface. 
 -  Axios para comunicação com a API. 
 ## Configuração do Ambiente 
 1. Clone o Repositório . 
 2. Configuração do Banco de Dados - O projeto utiliza SQLite por padrão. - O arquivo do banco será gerado automaticamente no diretório do projeto (lembretes.db). 
 3. Instalar Dependências do Backend: 
	 - cd ... reminders_system\SistemaLembretes\SistemaLembretes.Server 'dotnet restore'
 4.  Instalar Dependências do Frontend:
	 -  cd ... reminders_system\SistemaLembretes\sistemalembretes.client 'npm install'
 5. Execução Backend. Acesse o diretório do backend: cd backend. Execute o servidor: dotnet run . O backend estará na porta: http://localhost:5000
 6.  Frontend . Acesse o diretório do frontend: cd frontend. Execute o servidor de desenvolvimento: npm start. A aplicação estará na porta: http://localhost:3000 
 7. Testes Automatizados Backend. No diretório do backend, execute: dotnet test SistemaLembretes.Server. Para rodar os testes do React, utilize: npm test API 
 8. Endpoints - 
 - GET /api/lembrete - Lista todos os lembretes. 
 - GET /api/lembrete/{id} - Retorna um lembrete pelo ID. 
 - POST /api/lembrete - Cria um novo lembrete. 
 -  PUT /api/lembrete/{id} - Atualiza um lembrete existente. (não implementada)
 -  DELETE /api/lembrete/{id} - Exclui um lembrete. 
 -  PATCH /api/lembrete/{id}/concluir - Marca um lembrete como concluído. (não implementada)
# Premissas e Decisões de Projeto 
## Estrutura do Backend

Utilizei o padrão arquitetural MVC (Model-View-Controller), com separação adicional de responsabilidades em camadas de serviços, repositórios e validações, visando facilitar a divisão dos componentes e navegação no projeto. 
Foi dividido em:

-   Configs: Configuração do framework de entidade Microsoft Entity Framework Core SQLite, com o DatabaseContext para gerenciar a conexão com o banco de dados SQLite.
-   Controllers: Gerencia as rotas da API. Lá o LembreteController expõe endpoints RESTful para as operações de criação, leitura, atualização, exclusão e marcação de lembretes como concluídos.
-   Data: Camada de repositórios que abstrai o acesso do banco de dados, usando o Entity Framework Core. Têm o RepositorioLembretes, que realiza operações de CRUD no banco.
-   Models: Armazena os modelos, nesse caso temos apenas o Lembrete, que define os atributos do objeto.
-   Services: Implementa a lógica no GerenciadorLembretes, coordenando as operações entre os repositórios e os controladores. Também possui validações específicas e manipulação de dados.
-   Tests: Onde ocorrem testes automatizados para validar a integridade dos modelos e validações Utiliza o framework de testes XUnit.

Nessa estrutura, o código tem menor acoplamento e maior escalabilidade. Nem todas as funcionalidades criadas foram implementadas, visando entregar o produto requisitado. 

## Estrutura do Frontend

Utilizei o framework React.js para a construção do frontend, seguindo a abordagem de uma Single-Page Application (SPA). Pela simplicidade do projeto, não foi necessário dividir a estrutura do frontend em componentes reutilizáveis, uma vez que mesmo que sejam implementadas novas funcionalidades no backend, não existe necessidade prática de multi-paginação. 
