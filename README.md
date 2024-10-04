API de Controle de Recrutamento
API backend para gerenciamento de um sistema de recrutamento, com funcionalidades voltadas para o controle de candidatos e vagas, além do acompanhamento de inscrições e processo seletivo.

Funcionalidades Principais
Gerenciamento de Candidatos: criação, edição, listagem e exclusão de perfis.
Controle de Vagas: CRUD completo de vagas, com informações relevantes para o processo de seleção.
Inscrição e Acompanhamento: funcionalidades para inscrição de candidatos e monitoramento do status de suas candidaturas.
Autenticação e Autorização: segurança com JWT para diferenciar acessos entre administradores e candidatos.


Configuração e Execução
Clone o repositório e configure as variáveis de ambiente conforme o necessário (conexão com banco de dados, chave JWT, etc).
No terminal, navegue até a pasta do projeto e execute:
bash
Copiar código
dotnet run
A API estará acessível em http://localhost:5000.
