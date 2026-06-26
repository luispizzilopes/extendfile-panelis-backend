# Panelis — Backend

API REST do sistema **Panelis**, uma plataforma de gestão e monitoramento de alimentação de gatos em ambientes controlados (prédios e boxes). Permite o registro de testes de consumo alimentar, análise por gato e geração de relatórios.

---

## Sumário

- [Arquitetura](#arquitetura)
- [Design Patterns](#design-patterns)
- [Stack Tecnológica](#stack-tecnológica)
- [Estrutura de Módulos](#estrutura-de-módulos)
- [Variáveis de Ambiente](#variáveis-de-ambiente)
- [Como Rodar Localmente](#como-rodar-localmente)
- [Migrations](#migrations)
- [Convenções de Código](#convenções-de-código)

---

## Arquitetura

O projeto segue **Clean Architecture** com separação estrita entre camadas. A solução é composta por 9 projetos:

```
extendfile-panelis.sln
├── ExtendFile.Panelis.Domain           # Entidades, Value Objects, Aggregates, interfaces de repositório
├── ExtendFile.Panelis.Application      # Casos de uso, Commands, Queries, Handlers, Validadores, DTOs
├── ExtendFile.Panelis.Infrastructure   # EF Core, repositórios, serviços externos, migrations, scripts SQL
├── ExtendFile.Panelis.Presentation     # Controllers, Middlewares, Swagger, configuração do host
├── ExtendFile.Panelis.CrossCutting     # IoC / Dependency Injection centralizado
├── ExtendFile.Panelis.BuildingBlocks   # Abstrações base: AggregateRoot, Entity, ValueObject, Pagination
├── ExtendFile.Panelis.BackgroundJobs   # Jobs em background (Hangfire/hosted services)
├── ExtendFile.Panelis.Schedulers       # Agendamentos recorrentes
└── ExtendFile.Panelis.Tests            # Testes automatizados
```

### Responsabilidades por camada

| Camada | Responsabilidade |
|---|---|
| **Domain** | Regras de negócio puras, entidades ricas, value objects, enums de domínio, exceções de domínio |
| **Application** | Orquestração via Use Cases, Commands e Queries (CQRS), validação de entrada, DTOs de request/response |
| **Infrastructure** | Implementação de repositórios, contexto EF Core, serviços de e-mail, JWT, logging, configurações EF |
| **Presentation** | Controllers REST, pipeline HTTP, Swagger, middleware de erros globais, versionamento de API |
| **CrossCutting** | Registro de dependências (DI) organizado por módulo para manter `Program.cs` limpo |
| **BuildingBlocks** | Primitivas reutilizáveis: `AggregateRoot`, `Entity`, `ValueObject`, `PaginedResult`, `PaginationParams` |

---

## Design Patterns

### CQRS com MediatR

Todas as operações são modeladas como Commands (mutações) ou Queries (leituras), processadas por Handlers via MediatR:

```
Controller
  └── envia Command/Query via IMediator.Send()
        └── Handler (IRequestHandler<TRequest, TResponse>)
              └── UseCase (lógica de aplicação)
                    └── Repository (acesso a dados)
```

### Use Cases

Cada operação de negócio tem seu próprio `UseCase` isolado, chamado pelo `Handler`. Isso separa a lógica de negócio do mecanismo de entrega (MediatR) e facilita testes.

```
Application/Modules/Cat/
├── Commands/CreateCat/
│   ├── CreateCatCommand.cs         # Parâmetros da operação
│   ├── CreateCatCommandHandler.cs  # Delega para o UseCase
│   └── CreateCatCommandValidator.cs # FluentValidation
├── Requests/CreateCat/
│   └── CreateCatRequest.cs         # DTO de entrada do Controller
├── Responses/
│   └── CatDto.cs                   # DTO de saída
└── UseCases/CreateCat/
    └── CreateCatUseCase.cs         # Lógica de negócio
```

### Repository Pattern + Unit of Work

Os repositórios são definidos como interfaces no `Domain` e implementados na `Infrastructure`. O `UnitOfWork` centraliza o commit das transações:

```csharp
// Domain
public interface IUnitOfWork {
    ICatRepository CatRepository { get; }
    IHouseRepository HouseRepository { get; }
    // ...
    Task CommitAsync(CancellationToken cancellationToken);
}
```

### FluentValidation

Cada Command/Query possui um `*Validator` com regras de validação declarativas. O `ValidationBehavior` (MediatR Pipeline Behavior) intercepta automaticamente antes do Handler:

```
Request → ValidationBehavior → Handler → UseCase
```

### ErrorOr

Os Use Cases retornam `ErrorOr<T>` em vez de lançar exceções para fluxos esperados. O `ActionResultExtension` converte os erros para HTTP responses adequados:

```csharp
// UseCase retorna
return Error.NotFound("Cat.NotFound", "Gato não encontrado");

// Controller converte automaticamente
return result.ToActionResult(this); // → 404 Not Found
```

### Middleware Global de Erros

`ErrorMiddleware` captura exceções não tratadas e retorna resposta JSON padronizada. Em ambiente de desenvolvimento expõe a mensagem original; em produção retorna mensagem genérica.

---

## Stack Tecnológica

| Tecnologia | Versão | Uso |
|---|---|---|
| .NET | 8.0 | Runtime e SDK |
| ASP.NET Core | 8.0 | Framework web |
| Entity Framework Core | 8.0.24 | ORM |
| PostgreSQL + Npgsql | 8.0.11 | Banco de dados |
| ASP.NET Core Identity | 8.0.24 | Autenticação e gerenciamento de usuários |
| JWT Bearer | 8.0.24 | Autenticação stateless via tokens |
| MediatR | 14.0.0 | Mediador para CQRS |
| FluentValidation | 12.1.1 | Validação de requests |
| ErrorOr | 2.0.1 | Resultados tipados sem exceções |
| Serilog | 4.3.1 | Logging estruturado (console + arquivo) |
| Swagger / Swashbuckle | 6.6.2 | Documentação da API |
| API Versioning | 5.1.0 | Versionamento via URL (`/api/v1/`) |
| MailKit | 4.17.0 | Envio de e-mails transacionais |
| Brevo (Sendinblue) | — | Provedor de e-mail em produção |
| Newtonsoft.Json | 13.0.4 | Serialização em middlewares |

---

## Estrutura de Módulos

Cada domínio de negócio (Cat, House, User, Test, Report, Setting, Dashboard) segue a mesma estrutura dentro de `Application/Modules/<Dominio>/`:

```
Modules/<Dominio>/
├── Commands/<Operacao>/
│   ├── <Operacao>Command.cs
│   ├── <Operacao>CommandHandler.cs
│   └── <Operacao>CommandValidator.cs
├── Queries/<Operacao>/
│   ├── <Operacao>Query.cs
│   ├── <Operacao>QueryHandler.cs
│   └── <Operacao>QueryValidator.cs
├── Requests/<Operacao>/
│   └── <Operacao>Request.cs
├── Responses/
│   └── <Dominio>Dto.cs
└── UseCases/<Operacao>/
    └── <Operacao>UseCase.cs
```

---

## Variáveis de Ambiente

Copie `.env.example` para `.env` e preencha os valores. Em desenvolvimento local, prefira **User Secrets** do .NET.

| Variável | Descrição | Exemplo |
|---|---|---|
| `CONNECTION_STRING` | String de conexão PostgreSQL | `Host=localhost;Port=5432;Database=panelis;Username=postgres;Password=` |
| `EMAIL_HOST` | Servidor SMTP | `smtp.gmail.com` |
| `EMAIL_PORT` | Porta SMTP | `587` |
| `EMAIL_USERNAME` | Usuário do e-mail | `seu@email.com` |
| `EMAIL_PASSWORD` | Senha do e-mail | — |
| `EMAIL_FROM_EMAIL` | E-mail remetente | `noreply@panelis.com` |
| `EMAIL_FROM_NAME` | Nome do remetente | `Panelis` |

---

## Como Rodar Localmente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [PostgreSQL 14+](https://www.postgresql.org/)
- [dotnet-ef](https://learn.microsoft.com/ef/core/cli/dotnet) — instale com:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Setup

```bash
# 1. Clone o repositório
git clone https://github.com/luispizzilopes/extendfile-panelis-backend.git
cd extendfile-panelis-backend

# 2. Configure as credenciais via User Secrets (recomendado para dev)
cd ExtendFile.Panelis.Presentation
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=panelis;Username=postgres;Password=sua_senha"

# 3. Aplique as migrations
dotnet ef database update \
  --project ExtendFile.Panelis.Infrastructure \
  --startup-project ExtendFile.Panelis.Presentation

# 4. Inicie a API
dotnet run --project ExtendFile.Panelis.Presentation
```

A API estará disponível em `https://localhost:5001`. O Swagger pode ser acessado em `/swagger`.

---

## Migrations

```bash
# Criar nova migration
dotnet ef migrations add <NomeDaMigration> \
  --project ExtendFile.Panelis.Infrastructure \
  --startup-project ExtendFile.Panelis.Presentation

# Aplicar migrations pendentes
dotnet ef database update \
  --project ExtendFile.Panelis.Infrastructure \
  --startup-project ExtendFile.Panelis.Presentation
```

Scripts SQL para produção ficam em `ExtendFile.Panelis.Infrastructure/Scripts/`, numerados sequencialmente (ex: `001_criacao_view_dashboard.sql`).

---

## Convenções de Código

### Nomenclatura

- **Classes**: PascalCase — `CreateCatUseCase`, `CatRepository`
- **Interfaces**: prefixo `I` — `ICatRepository`, `IUnitOfWork`
- **Métodos assíncronos**: sufixo `Async` — `GetByIdAsync`, `ExecuteAsync`
- **Commands**: sufixo `Command` — `CreateCatCommand`
- **Queries**: sufixo `Query` — `GetCatsQuery`
- **Handlers**: sufixo `Handler` — `CreateCatCommandHandler`
- **Use Cases**: sufixo `UseCase` — `CreateCatUseCase`
- **Validators**: sufixo `Validator` — `CreateCatCommandValidator`

### Fluxo de uma nova feature

1. Criar entidade/value object no **Domain** (se necessário)
2. Definir interface de repositório no **Domain**
3. Implementar repositório na **Infrastructure** + configuração EF (`Configuration/<Dominio>/`)
4. Criar `Request`, `Command/Query`, `Validator`, `Handler`, `UseCase` e `Response` na **Application**
5. Criar Controller na **Presentation**
6. Registrar dependências no **CrossCutting**
7. Gerar migration (se houve mudança no modelo)

### Padrão de commits

```
<tipo>: <descrição curta>

Tipos: feat | fix | refactor | docs | test | chore
Exemplo: feat: adiciona endpoint de relatório de consumo por gato
```
