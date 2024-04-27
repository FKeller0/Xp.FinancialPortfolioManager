# Xp.FinancialPortfolioManager

Repositório criado para o Teste Técnico - XP Inc.

Passos para executar:

- Clonar o repositório
- VSCode:
  - No diretório de API, executar o comando "dotnet ef database update" para gerar o banco de dados SQLITE
  - dotnet run    
- VisualStudio:
  - Update-Database com o projeto de Infraestrutura selecionado para gerar o banco de dados SQLITE
  - Executar(F5)

A Aplicação oferece suporte ao Swagger e seus métodos possuem uma breve descrição.

Um Usuário padrão da aplicação foi configurado para controle de acesso e criação de perfil de assessores
  
  - user: "financialportfolio@xpfinancialportfolio.com"
  - senha: Root12345!

Ações de Perfil são todas feitas através dos endpoints de "Profiles"

Os usuários registrados podem adquirir um perfil de Assessor ou Cliente, sendo que somente o Admin pode registar um perfil de Assessor e Assessores e Admins podem registrar um perfil de Cliente.

Clientes podem consultar suas próprias ações e Assessores podem consultar as ações de todos seus clientes.

Produtos podem ser registrados por um Admin ou Assessor.

O Serviço de Email foi configurado para disparar todos os dias as 09:00, enviando através de SMTP emails para todos os assessores cadastrados sobre os produtos que expiram naquele mesmo dia.
As configurações de SMTP podem ser acessadas no arquivo appsettings.json no projeto Xp.FinancialPortfolioManager.API
A configuração de periodicidade pode ser alterada no arquivo Program.cs, a biblioteca utilizada para o Scheduler é bem flexível.

Libs utilizadas:
- MediatR => padrão de mediador utilizado com padrão CQRS para envios de comandos e queries, também permite adição de Behaviors para Pipelines;
- Coravel => Simplicidade na gestão de Scheduler;
- ErrorOr => Permite simplificar retornos de maneira elegante atribuindo um Erro ou Resultado ao retorno;
- MailKit => Biblioteca de envio de emails;
- FluentValidations => Facilita a validação de dados recebidos de maneira fluente;
- BCrypt => criptografia de senhas;
- EFCore => Facilidade na gestão de acesso ao banco de Dados, permitindo também modularidade na implementação do Banco(Neste caso o banco utilizado foi o SQLITE);
