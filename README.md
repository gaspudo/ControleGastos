# Controle de Gastos Residenciais

Sistema fullstack de controle de gastos residenciais, desenvolvido como projeto de portfólio com foco em arquitetura em camadas, boas práticas de C#/.NET e fundamentos de React com TypeScript.

O sistema permite cadastrar pessoas de uma residência, registrar suas transações financeiras (receitas e despesas) e consultar um relatório consolidado de totais por pessoa e geral.

## Funcionalidades

- **Cadastro de Pessoas**: criação, listagem e exclusão (com exclusão em cascata das transações associadas).
- **Cadastro de Transações**: criação e listagem, com regra de negócio que impede menores de 18 anos de cadastrar receitas.
- **Consulta de Totais**: relatório com receitas, despesas e saldo por pessoa, além do total geral consolidado.

## Stack

**Backend**
- .NET 10 / C#
- Entity Framework Core (Pomelo, provider MySQL)
- MySQL 8.0
- xUnit (testes unitários)
- Swagger / OpenAPI

**Frontend**
- React 19 + TypeScript
- Vite

## Arquitetura

O backend segue uma arquitetura em camadas inspirada em Clean Architecture:

```
backend/
├── ControleGastos.Domain          # Entidades, regras de negócio, interfaces de repositório
├── ControleGastos.Infrastructure  # EF Core, repositórios, Unit of Work, migrations
├── ControleGastos.Api             # Controllers, DTOs, Services, middleware
└── ControleGastos.Tests           # Testes unitários (xUnit)
```

**Decisões de design relevantes:**

- **Domínio rico**: as entidades `Pessoa` e `Transacao` protegem suas próprias invariantes através dos construtores (ex: `Transacao` recusa a criação de uma receita para menor de idade lançando `RegraDeNegocioException`), em vez de depender de validação externa.
- **Repository Pattern + Unit of Work**: acesso a dados abstraído por interfaces (`IPessoaRepository`, `ITransacaoRepository`, `IRelatorioRepository`), com `IUnitOfWork` coordenando transações atômicas quando uma operação de negócio (como excluir uma pessoa e suas transações) precisa de consistência entre múltiplos repositórios.
- **Exceções de domínio tipadas**: hierarquia de exceções (`AppException` → `RegraDeNegocioException`, `NotFoundException`) capturada por um middleware global que traduz cada tipo para o status HTTP correto (400, 404), sem expor detalhes internos em erros não mapeados (500).
- **DTOs em todas as bordas da API**: entidades de domínio nunca são expostas diretamente pelos endpoints.

O frontend segue uma estrutura simples por responsabilidade:

```
frontend/controle-gastos/src/
├── components/    # Componentes React (um arquivo por componente + CSS correspondente)
├── services/      # Camada de comunicação HTTP com a API
├── types/         # Interfaces TypeScript espelhando os DTOs do backend
└── App.tsx        # Orquestração de estado compartilhado (lifting state up)
```

## Como rodar o projeto

### Backend

```bash
cd backend/ControleGastos.Api
dotnet restore
dotnet ef database update --project ../ControleGastos.Infrastructure --startup-project .
dotnet run
```

A API sobe com Swagger disponível em `/swagger`. É necessário um servidor MySQL local rodando e a connection string configurada em `appsettings.Development.json` (não versionado por conter credenciais):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=db_controlegastos;User=root;Password=SUA_SENHA;"
  }
}
```

### Frontend

```bash
cd frontend/controle-gastos
npm install
npm run dev
```

Crie um arquivo `.env` na raiz do frontend com a URL da API:

```
VITE_API_URL={caminho_da_sua_api)}
```

### Testes

```bash
cd backend/ControleGastos.Tests
dotnet test
```

## Autor

Desenvolvido por [João Pedro Gaspar da Silva](https://github.com/gaspudo).