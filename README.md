# ⚙️ Automation.Engine

Plataforma de **automação robótica** construída em **.NET 8** aplicando **Arquitetura Limpa (Clean Architecture)**.  
O projeto combina:

- 🌐 **Web Crawling** → captura de dados da web.  
- 🤖 **RPA (Robotic Process Automation)** → automação de processos em sistemas/web.  
- 🗄 **Persistência** em **PostgreSQL** via Entity Framework Core.  
- ⏰ **Agendamento de tarefas** com Quartz.NET.  
- 📊 **Logging estruturado** com Serilog.  

---

## 📌 Estrutura da Solução

Automation.Engine.sln
├── Automation.Engine.Domain # Regras de negócio (entidades + interfaces)
├── Automation.Engine.Application # Casos de uso (services, DTOs, jobs)
├── Automation.Engine.Infrastructure # Implementações (EF Core, Crawlers, RPA, Logging)
└── Automation.Engine.Worker # Worker Service (Quartz + execução contínua)

markdown
Copiar código

---

## 📂 Detalhes de cada camada

### 🔹 Domain
- Entidades de negócio (`Quote`, etc).  
- Interfaces de repositórios e serviços (`IQuoteRepository`).  
- Não tem dependência de nada externo.  

### 🔹 Application
- Casos de uso (`QuoteService`).  
- DTOs de entrada/saída.  
- Jobs do Quartz (`QuoteJob`).  
- Depende apenas do **Domain**.  

### 🔹 Infrastructure
- Implementação dos repositórios (`QuoteRepository`).  
- DbContext do EF Core (`AutomationContext`).  
- Crawlers com Selenium/HtmlAgilityPack.  
- Serviços RPA (automatização de formulários).  
- Logging com Serilog.  

### 🔹 Worker
- Ponto de entrada (`Program.cs`).  
- Configuração do **Quartz.NET** (agendamento).  
- Injeção de dependência.  
- Execução contínua como **Worker Service**.  

---

## ⚙️ Tecnologias Usadas

- **.NET 8 Worker Service**  
- **Entity Framework Core + Npgsql** (PostgreSQL)  
- **Quartz.NET** (jobs agendados)  
- **Selenium / HtmlAgilityPack** (crawling e RPA)  
- **Serilog** (logging estruturado)  
- **Docker** (opcional para banco e worker)  

---

## 🚀 Como Rodar o Projeto

### 1. Clonar o repositório
```bash
git clone https://github.com/sua-org/Automation.Engine.git
cd Automation.Engine
2. Configurar Banco de Dados
Subir PostgreSQL com Docker:

bash
Copiar código
docker run --name automation-postgres -e POSTGRES_PASSWORD=123456 -e POSTGRES_DB=automation -p 5432:5432 -d postgres:15
Configurar appsettings.json no Worker:

json
Copiar código
"ConnectionStrings": {
  "Postgres": "Host=localhost;Database=automation;Username=postgres;Password=123456"
}
3. Criar Migrations
bash
Copiar código
cd Automation.Engine.Infrastructure
dotnet ef migrations add InitialCreate -o Persistence/Migrations
dotnet ef database update
4. Rodar o Worker
bash
Copiar código
dotnet run --project Automation.Engine.Worker
🧪 Testando
Rodar testes unitários
bash
Copiar código
dotnet test
Verificar inserts no banco
sql
Copiar código
SELECT * FROM "Quotes";
Logs esperados no console:

csharp
Copiar código
[Quartz] QuoteJob executando...
[Crawler] Capturado texto: "Frase teste 123"
[DB] Quote salvo com sucesso!
[RPA] Formulário preenchido
📊 Fluxo do Sistema
O Quartz.NET dispara um Job (QuoteJob).

O Job chama QuoteService (Application).

O QuoteService usa:

CrawlerService → captura dados da web.

IQuoteRepository → salva no banco (Postgres via EF Core).

RpaService → automatiza ações externas.

Logs são gerados pelo Serilog.

🏗 Extensibilidade
Para adicionar um novo crawler → criar serviço em Infrastructure.Crawlers e expor via Application.

Para adicionar um novo job → criar em Application.Jobs e registrar no Quartz.

Para trocar de banco → alterar apenas Infrastructure (sem mudar Application/Domain).
