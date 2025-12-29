# Ops-oppsett for ProjectHub

Dette dokumentet forklarer hvordan operasjoner (ops) er satt opp i ProjectHub. Du kan bruke dette som inspirasjon for andre repositories.

## Oversikt

ProjectHub bruker:
- **GitHub Actions** for CI/CD
- **Docker** for containerisering
- **Docker Compose** for lokal utvikling og deployment

## Mappestruktur

```
ops/
└── docker/
    ├── Dockerfile.api      # Dockerfile for .NET API
    ├── Dockerfile.web      # Dockerfile for Vue frontend
    ├── docker-compose.yml  # Orkestrering av tjenester
    └── nginx.conf          # Nginx-konfigurasjon for web

.github/
└── workflows/
    └── ci.yml              # CI/CD workflow
```

## GitHub Actions CI/CD

### Workflow: ci.yml

**Plassering:** `.github/workflows/ci.yml`

**Trigger:** 
- Push til `master`-branchen
- Pull requests mot `master`

**Jobber:**

#### 1. Build & Test API (.NET)
```text
- Setup .NET 8.0.414
- Restore dependencies: dotnet restore
- Build: dotnet build (Release mode)
- Test: dotnet test
- Format check: dotnet format --verify-no-changes
```

#### 2. Build & Test Web (Vue)
```text
- Setup Node 20.19.0
- Install dependencies: npm ci
- Format check: npm run format
- Build: npm run build
```

### Nøkkelprinsipper

1. **Fast feedback**: Bygger og tester både API og web i samme workflow
2. **Code quality**: Verifiserer formatering for både .NET og JavaScript/TypeScript
3. **Minimal config**: Bruker standard actions fra GitHub
4. **Reproduserbarhet**: Spesifikke versjoner av .NET og Node

## Docker-oppsett

### Dockerfile.api (Multi-stage build)

**Stage 1: Build**
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/api ./api
RUN dotnet restore api/ProjectHub.Api/ProjectHub.Api.csproj
RUN dotnet publish api/ProjectHub.Api/ProjectHub.Api.csproj -c Release -o /app
```

**Stage 2: Runtime**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "ProjectHub.Api.dll"]
```

**Fordeler:**
- ✅ Mindre image-størrelse (runtime uten SDK)
- ✅ Sikkerhet (kun nødvendige runtime-filer)
- ✅ Raskere deployment

### Dockerfile.web (Multi-stage build)

**Stage 1: Build**
```dockerfile
FROM node:20-bullseye AS build
WORKDIR /app
COPY src/web/package*.json ./
COPY src/web/ ./
RUN npm ci && npm run build
```

**Stage 2: Nginx**
```dockerfile
FROM nginx:1.27-alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY ops/docker/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
```

**Fordeler:**
- ✅ Statiske filer served av Nginx (rask og effektiv)
- ✅ Minimal image (Alpine Linux)
- ✅ Proxy til API konfigurert i nginx.conf

### docker-compose.yml

Orkestrerer to tjenester:

```text
services:
  api:
    - Build context: Repo root
    - Port: 8080
    - Database: SQL Server (Tailscale network)
    - Auto-restart

  web:
    - Build context: Repo root
    - Port: 5173 (mapped til 80)
    - Depends on: api
    - Auto-restart
```

### Nginx-konfigurasjon

**Plassering:** `ops/docker/nginx.conf`

**Funksjoner:**
1. Serve Vue SPA (Single Page Application)
   - Alle ruter går til `index.html` (client-side routing)
2. Proxy API-requests
   - Requests til `/api/*` proxies til `http://api:8080/`
   - Setter riktige headers (Host, X-Real-IP, X-Forwarded-For)

## Bruk av oppsettet

### Lokal utvikling med Docker Compose

```bash
# Start alle tjenester
cd ops/docker
docker-compose up --build

# API tilgjengelig på: http://localhost:8080
# Web tilgjengelig på: http://localhost:5173
```

### Stopp tjenester

```bash
docker-compose down
```

### Rebuild uten cache

```bash
docker-compose build --no-cache
docker-compose up
```

## Best Practices og tips

### 1. Multi-stage builds
- Bruk alltid multi-stage builds for mindre images
- Separer build-miljø fra runtime-miljø
- Kopier kun nødvendige filer til final stage

### 2. Build context
- Docker Compose kjøres fra `ops/docker/`
- Build context satt til `../../` (repo root)
- Dette tillater tilgang til alle source-filer

### 3. .dockerignore
Legg til `.dockerignore` i repo root for å ekskludere:
```
node_modules/
bin/
obj/
.git/
*.log
```

### 4. Environment variables
- Bruk `.env`-fil for lokale secrets (ikke commit!)
- Bruk `${VARIABLE}` syntax i docker-compose.yml
- Eksempel: `${SA_PASSWORD}` for database-passord

### 5. Networking
- Docker Compose lager automatisk et nettverk
- Tjenester kan kommunisere med tjenestenavn (f.eks. `http://api:8080`)
- Nginx proxyer fra `web` til `api`

### 6. CI/CD
- Test lokalt før push: `npm run format`, `dotnet format`
- CI feiler hvis formatering ikke er OK
- Bruk samme versjoner lokalt som i CI (Node 20.19.0, .NET 8.0.414)

### 7. Port-mapping
```
8080:8080  → API (container:host)
5173:80    → Web (host:container)
```

## Tilpasning til andre prosjekter

### For et nytt .NET + Vue prosjekt:

1. **Kopier disse filene:**
   - `.github/workflows/ci.yml`
   - `ops/docker/Dockerfile.api`
   - `ops/docker/Dockerfile.web`
   - `ops/docker/docker-compose.yml`
   - `ops/docker/nginx.conf`

2. **Tilpass:**
   - Prosjektnavn i Dockerfile.api (ProjectHub.Api → DittProsjekt.Api)
   - Database connection string i docker-compose.yml
   - .NET versjon (hvis nødvendig)
   - Node versjon (hvis nødvendig)
   - Port-nummer (hvis ønskelig)

3. **Test:**
   ```bash
   # Test CI lokalt
   dotnet restore && dotnet build && dotnet test
   npm ci && npm run build
   
   # Test Docker
   cd ops/docker
   docker-compose up --build
   ```

### For andre stack:

**Python + React:**
- Erstatt Dockerfile.api med Python (Flask/FastAPI/Django)
- Behold Dockerfile.web pattern (bytt Vue med React)
- Tilpass CI workflow for Python (pytest, black, mypy)

**Node.js backend:**
- Bruk Node multi-stage build for API
- Samme pattern for web
- Tilpass CI for Node (npm test, eslint)

**Java Spring Boot:**
- Bruk Maven/Gradle multi-stage build
- Samme pattern for web
- Tilpass CI for Java (mvn test, checkstyle)

## Feilsøking

### Docker build feil
```bash
# Sjekk logs
docker-compose logs api
docker-compose logs web

# Rebuild uten cache
docker-compose build --no-cache
```

### Port allerede i bruk
```bash
# Finn prosess som bruker port
lsof -i :8080
lsof -i :5173

# Eller endre port i docker-compose.yml
```

### CI feil på GitHub
- Sjekk at versjoner matcher (Node, .NET)
- Test samme kommandoer lokalt
- Sjekk at alle filer er committet

## Ressurser

- [Docker multi-stage builds](https://docs.docker.com/build/building/multi-stage/)
- [GitHub Actions dokumentasjon](https://docs.github.com/en/actions)
- [Docker Compose dokumentasjon](https://docs.docker.com/compose/)
- [Nginx konfigurasjon](https://nginx.org/en/docs/)

## Oppsummering

Dette oppsettet gir:
- ✅ Automatisk testing og formatering i CI
- ✅ Konsistent miljø lokalt og i produksjon (Docker)
- ✅ Lett å tilpasse for andre prosjekter
- ✅ Best practices for containerisering
- ✅ Fast feedback loop for utviklere

Kopier og tilpass etter behov! 🚀
