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

A Aplicação oferece suporte ao Swagger e seus métodos possuem descrição.

Um Usuário padrão da aplicação foi configurado para controler de acesso e criação de perfil de assessores
  
  - user: "financialportfolio@xpfinancialportfolio.com"
  - senha: Root12345!

Ações de Perfil são todas feitas através dos endpoints de "Profiles"
Os usuários registrados podem adquirir um perfil de Assessor ou Cliente, sendo que somente o Admin pode registar um perfil de Assessor e Assessores e Admins podem registrar um perfil de Cliente.
Clientes podem consultar suas próprias ações e Assessores podem consultar as ações de todos seus clientes.

Produtos podem ser registrados por um Admin ou Assessor.

O Serviço de Email é disparado todos os dias as 09:00, enviando através de SMTP emails para todos os assessores cadastrados sobre os produtos que expiram naquele mesmo dia.
As configurações de SMTP podem ser acessadas no arquivo appsettings.json no projeto Xp.FinancialPortfolioManager.API
