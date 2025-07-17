# TPCadastroUsuario

Este projeto consiste em uma API ASP.NET Core 8 (backend), um frontend em Angular 19 servido pelo Nginx, e um banco de dados SQL Server, todos orquestrados via Docker Compose.

---

## Pré-requisitos

- **Docker** (v20+)
- **Docker Compose** (v2+)
- Git (para clonar o repositório)

---

## Passo a passo para execução

1. **Clone o repositório**

   ```bash
   git clone https://github.com/seu-usuario/TPCadastroUsuario.git
   cd TPCadastroUsuario
   ```
2. **Executar as dependências do Angular**

Na raiz do "tpcadastro-usuario", execute:

   ```npm install 
   ```

3. **Build e subida dos containers**

   Na raiz do projeto, execute:

   ```bash
   docker-compose build
   docker-compose up -d
   ```

   Isso irá:

   - Baixar/buildar as imagens de cada serviço
   - Criar a rede interna `tpcadastrousuario_default`
   - Subir os containers de **db**, **initdb**, **backend** e **frontend**

4. **Criar o banco de dados e tabelas**

   Após o passo anterior, rode o init‑container para aplicar o script de migração:

   ```bash
   docker-compose run --rm initdb
   ```

   Esse comando monta o `backend/migra.sql` dentro de um container com `sqlcmd` e executa a criação do banco e das tabelas.

5. **Verifique os serviços**

   ```bash
   docker-compose ps
   ```

   Você deve ver algo assim:

   | Nome                         | Serviço  | Portas                 |
   | ---------------------------- | -------- | ---------------------- |
   | sqlserver                    | db       | 0.0.0.0:1433->1433/tcp |
   | tpcadastrousuario-backend-1  | backend  | 0.0.0.0:5000->8080/tcp |
   | tpcadastrousuario-frontend-1 | frontend | 0.0.0.0:4200->80/tcp   |
     
6. **Acesse a aplicação**

   - **Swagger da API**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   - **Frontend Angular**: [http://localhost:4200/](http://localhost:4200/)
---

## Comandos úteis

- **Parar e remover containers, rede e volumes**

  ```bash
  docker-compose down -v
  ```

- **Ver logs de um serviço**

  ```bash
  docker-compose logs -f backend
  ```

- **Reconstruir apenas um serviço**

  ```bash
  docker-compose up -d --build frontend
  ```

---

## Estrutura do Docker Compose

- **db**: container SQL Server (`mcr.microsoft.com/mssql/server:2022-latest`)
- **initdb**: roda `migra.sql` via `mssql-tools` para criar banco e tabelas
- **backend**: Web API ASP.NET Core, exposta na porta interna 8080
- **frontend**: build do Angular servido por Nginx na porta interna 80

---

## Testes Automatizados

### Backend (.NET)

Os testes do backend são executados automaticamente durante o **build** da imagem Docker. No `backend/Dockerfile`, após copiar todo o código, há o comando:

```dockerfile
RUN dotnet test \
  src/Tests/Application.Tests/TPCadastroUsuario.Application.Tests.csproj \
  --no-restore --no-build -c Release \
  --logger "console;verbosity=detailed"
```

Dessa forma, sempre que você executar:

```bash
docker-compose build backend
```

é garantido que todos os testes passem antes de gerar a imagem. Se algum teste falhar, o build será interrompido.

Para rodar os testes de forma isolada, sem rebuildar o backend completo, adicione este serviço no `docker-compose.yml`:

```yaml
services:
  backend-tests:
    image: mcr.microsoft.com/dotnet/sdk:8.0
    volumes:
      - ./backend:/app
    working_dir: /app/src/Tests/Application.Tests
    command: ["dotnet", "test", "--no-restore", "--no-build", "-c", "Release", "--logger:console;verbosity=detailed"]
```

E então execute:

```bash
docker-compose run --rm backend-tests
```

---

## Personalizações Automatizados

- **Executar testes do backend** via container:

  ```bash
  docker run --rm \
    -v "$(pwd)/backend/src/Tests":/src \
    -w "/src/Application.Tests" \
    mcr.microsoft.com/dotnet/sdk:8.0 \
    dotnet test
  ```

## Personalizações

- Para alterar a porta do backend no host, modifique a seção `ports` no `docker-compose.yml`:

  ```yaml
  backend:
    ports:
      - "<HOST_PORT>:8080"
  ```

- Se precisar ajustar variáveis de ambiente (ex.: connection string), edite o bloco `environment` do serviço `backend`.

---

Pronto! Agora qualquer pessoa que clone este repositório poderá subir todos os serviços com Docker e já ter sua base de dados criada automaticamente.

