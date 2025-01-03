# Estrutura do Backend

Utilizei o padrão arquitetural MVC (Model-View-Controller), com separação adicional de responsabilidades em camadas de serviços, repositórios e validações. Dividido em:
- Configs: Configuração do framework de entidade Microsoft Entity Framework Core SQLite, com o DatabaseContext para gerenciar a conexão com o banco de dados SQLite.
- Controllers: Gerencia as rotas da API. Lá o LembreteController  expõe endpoints RESTful para as operações de criação, leitura, atualização, exclusão e marcação de lembretes como concluídos.
- Data: Camada de repositórios que abstrai o acesso do banco de dados, usando o Entity Framework Core. Têm o RepositorioLembretes, que realiza operações de CRUD no banco.
- Models: Armazena os modelos, nesse caso temos apenas o Lembrete, que define os atributos do objeto.
- Services: Implementa a lógica  no GerenciadorLembretes, coordenando as operações entre os repositórios e os controladores. Também possui validações específicas e manipulação de dados.
- Tests: Onde ocorrem testes automatizados para validar a integridade dos modelos e validações  Utiliza o framework de testes XUnit.

Nessa estrutura, o código tem menor acoplamento e maior escalabilidade.
