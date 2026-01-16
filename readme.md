# Guia de Execução do Projeto

Este projeto contém uma API .NET 9 usando Clean Architecture, JWT para autenticação e PostgreSQL via Docker.

## 1. Como executar o projeto localmente?

```bash
# Criar diretório para o projeto (opcional)
mkdir meu-projeto
cd meu-projeto

# Iniciar o git no diretório.
git init

# Clonar o repositório
git clone https://github.com/maylodossantos/technical-test.git
```
Importante: Para facilitar o desenvolvimento local, as variáveis de ambiente já estão pré-configuradas com valores padrão. Não é necessário configurar manualmente.

## 2. Execução da Aplicação

```bash
# Subir containers no Docker.
docker-compose up --build

# Verificar status dos containers
docker-compose ps
```

# Execução de Testes

## 1. Testes Unitários.

```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura detalhada
dotnet test --logger "console;verbosity=detailed"

# Executar testes de um projeto específico
dotnet test tests/UnitTests/
```

## 2. Testando Endpoints via Swagger

Acesse: http://localhost:8080/swagger/index.html

Os testes devem iniciar com a criação de um usuário, que não requer autenticação. Em seguida, realize o login para obter os tokens de acesso.

### Como configurar a autenticação após autenticado?

Clique no botão "Authorize" (cadeado) no topo da página

No modal que aparecer, insira: "Bearer token"

Clique em "Authorize" e depois em "Close"

Todos os endpoints protegidos agora podem ser testados
