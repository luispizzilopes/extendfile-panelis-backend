# extendfile-panelis-backend

## Configuração Local

As credenciais locais são gerenciadas via **User Secrets** do .NET e nunca vão para o repositório.

Inicializar o User Secrets (dentro da pasta do projeto de apresentação):
```bash
cd ExtendFile.Panelis.Presentation
dotnet user-secrets init
```

Adicionar a connection string:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=seu_banco;Username=seu_usuario;Password=sua_senha"
```

## Migrations

Gerar uma nova migration:
```bash
dotnet ef migrations add InitialMigration --project ExtendFile.Panelis.Infrastructure --startup-project ExtendFile.Panelis.Presentation
```

Atualizar o banco de dados:
```bash
dotnet ef database update --project ExtendFile.Panelis.Infrastructure --startup-project ExtendFile.Panelis.Presentation
```