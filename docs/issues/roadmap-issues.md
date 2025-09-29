# Phase Issue Drafts

Use these snippets with `gh issue create --title ... --body <(cat <<'EOF' ...)` or copy/paste into GitHub’s UI. Labels are suggestions; adjust to match your repository.

---

## Fase 0 — Project bootstrap

- **Title:** Fase 0 – Project bootstrap og DevX-baseline
- **Labels:** `phase0`, `backend`, `frontend`, `devx`
- **Body:**
  ```markdown
  ## Mål
  Få et minimalt, kjørbart skjelett på plass med grunnleggende DevX og CI.

  ## Oppgaver
  - [X] Monorepo med `src/api` og `src/web` klarer lokalt build (`dotnet build`, `npm run build`).
  - [X] Docker Compose starter API, SQL Server og web; API kjører migreringer automatisk.
  - [X] DevX-verktøy: `.editorconfig`, ESLint/Prettier, dotnet tool manifest (`dotnet-ef`, `dotnet-format`), Serilog-baselogging.
  - [X] GitHub Actions bygger og tester begge prosjekter, inkluderer `npm run format`.
  - [X] API har `GET /health` og web har enkel `/`-side som svarer OK.

  ## Akseptkriterier
  - `docker compose up` gjør API `/health` og web `/` tilgjengelig uten manuell inngripen.
  - CI passerer alle steg uten manuelle tweaks.
  ```

---

## Fase 1 — CRUD-kjerne

- **Title:** Fase 1 – CRUD for prosjekter og tasks
- **Labels:** `phase1`, `backend`, `frontend`
- **Body:**
  ```markdown
  ## Mål
  Levere full CRUD for prosjekter og tasks med solid Vue-dataflyt og validering.

  ## Backend
  - [ ] Entiteter: `Project`, `TaskItem` (1–mange) med EF Core migrasjoner.
  - [ ] Endepunkter: CRUD for `/projects` og `/projects/{id}/tasks` med DTO-mapping.
  - [ ] Paging/sortering + FluentValidation på DTO-er.

  ## Frontend
  - [ ] Ruter `/projects` og `/projects/:id` med liste og detaljside.
  - [ ] Pinia-stores: `useProjectStore` (cache) og `useTaskStore` (scoped til projectId).
  - [ ] Skjemaer med Zod + `@vee-validate`, modaler for create/edit.

  ## Akseptkriterier
  - Opprette/redigere/slette prosjekt og task uten refresh.
  - Inputvalidering i UI og API med forståelige feilmeldinger.
  ```

### Autogenererte issues for fase 1

- **Title:** Fase 1 – EF Core entiteter og migrasjoner
- **Labels:** `phase1`, `backend`, `api`
- **Body:**
  ```markdown
  ## Mål
  Opprette databasedesign og migrasjoner for `Project` og `TaskItem` slik at CRUD-endepunkter har persisteringslag.

  ## Oppgaver
  - [ ] Modellér `Project` og `TaskItem` i EF Core med 1–mange-relasjon.
  - [ ] Legg til `CreatedAt`/`UpdatedAt`-kolonner og relevante `Required`/`MaxLength`-attributter.
  - [ ] Oppdater `DbContext` og konfigurer `TaskItem.ProjectId` som fremmednøkkel.
  - [ ] Generer migrasjon og oppdater `DatabaseInitializer`/`docker-entrypoint`-script ved behov.

  ## Akseptkriterier
  - Migrasjonen kan kjøres mot lokal SQL Server uten feil.
  - Datamodellen støtter cascading slett eller eksplisitt håndtering for tasks.
  ```

- **Title:** Fase 1 – CRUD-endepunkter for prosjekter og tasks
- **Labels:** `phase1`, `backend`, `api`
- **Body:**
  ```markdown
  ## Mål
  Levere REST-endepunkter for prosjekter og tasks med DTO-er og AutoMapper-profiler.

  ## Oppgaver
  - [ ] Implementer `ProjectsController` med `GET/POST/PUT/DELETE` og prosjektspesifikke DTO-er.
  - [ ] Implementer `TasksController` eller nested routes under `ProjectsController` for `TaskItem` CRUD.
  - [ ] Sørg for `ProblemDetails`-svar, logging og meningsfulle statuskoder.
  - [ ] Skriv integrasjonstester for minst `POST` og `DELETE` på begge ressurser.

  ## Akseptkriterier
  - Swagger viser alle endepunkter med korrekte skjema.
  - CRUD-operasjoner fungerer mot database i Docker Compose.
  ```

- **Title:** Fase 1 – Paging, sortering og validering
- **Labels:** `phase1`, `backend`, `api`
- **Body:**
  ```markdown
  ## Mål
  Sikre konsistent validering og listeresponser for prosjekt- og task-API-ene.

  ## Oppgaver
  - [ ] Legg til FluentValidation-regler for prosjekt- og task-DTO-er (navn, beskrivelser, due date-logikk).
  - [ ] Implementer paging + sortering på `GET /projects` og `GET /projects/{id}/tasks`.
  - [ ] Oppdater responsmodeller til å inkludere `totalCount` og `pageSize` der det er relevant.
  - [ ] Utvid integrasjonstester med valideringsfeil og paging-scenarier.

  ## Akseptkriterier
  - Ugyldige payloads returnerer `422` med feilmeldinger fra FluentValidation.
  - Paging og sortering fungerer demonstrert via API-test eller Swagger-eksempel.
  ```

- **Title:** Fase 1 – Prosjektlister og detaljsider i Vue
- **Labels:** `phase1`, `frontend`, `vue`
- **Body:**
  ```markdown
  ## Mål
  Bygge ruter og komponenter for prosjektoversikt og detaljer inkludert oppslag av tasks.

  ## Oppgaver
  - [ ] Opprett ruter for `/projects` (liste) og `/projects/:id` (detalj + tasks).
  - [ ] Lag komponenter for prosjektkort, tomtilstand og lasteskjelett.
  - [ ] Koble data via Pinia-stores (mock eller ekte API) og håndter loading/error-tilstander.
  - [ ] Legg til grunnleggende enhetstester for routed components.

  ## Akseptkriterier
  - Navigasjon mellom liste og detalj fungerer uten full reload.
  - Liste viser tasks for valgt prosjekt med tomtilstand håndtert.
  ```

- **Title:** Fase 1 – Pinia-stores for prosjekter og tasks
- **Labels:** `phase1`, `frontend`, `state-management`
- **Body:**
  ```markdown
  ## Mål
  Etablere robuste Pinia-stores som håndterer caching, feilsituasjoner og optimistic updates.

  ## Oppgaver
  - [ ] Implementer `useProjectStore` med caching, invalidation og pending-state.
  - [ ] Implementer `useTaskStore` scoped til `projectId` med create/update/delete-handlere.
  - [ ] Integrer API-klient som kapsler axios/håndterer auth headers og feil.
  - [ ] Legg til Vitest-enheter for actions og getters.

  ## Akseptkriterier
  - Stores gir konsistente tilstander (loading/error) og multi-tab-sikker caching.
  - Optimistic updates ruller tilbake ved feil med synlig feedback.
  ```

- **Title:** Fase 1 – CRUD-skjemaer og modaler med validasjon
- **Labels:** `phase1`, `frontend`, `ux`
- **Body:**
  ```markdown
  ## Mål
  Lage gjenbrukbare skjemaer og modaler for oppretting/redigering av prosjekter og tasks med helhetlig validering.

  ## Oppgaver
  - [ ] Sett opp Zod-skjemaer og koble dem til `@vee-validate` forms.
  - [ ] Implementer modaler/dialoger for create/edit med spinnere og feilhåndtering.
  - [ ] Sikre inline-validering, disabled state ved pending og reset ved suksess.
  - [ ] Skriv komponenttester (Vitest + Testing Library) for skjema- og modal-logikk.

  ## Akseptkriterier
  - Bruker får tydelige feilmeldinger både fra UI og API (error-banner/toast).
  - Skjemaer støtter keyboard navigation og aria-attributter for tilgjengelighet.
  ```

---

## Fase 2 — Interaktivt UI

- **Title:** Fase 2 – Kanban, filtre og søk
- **Labels:** `phase2`, `frontend`, `backend`
- **Body:**
  ```markdown
  ## Mål
  Et interaktivt UI med drag-and-drop, filtrering og søk, støttet av query-endepunkter.

  ## Backend
  - [ ] Utvid `TaskItem` med `status`, `priority`, `dueDate`, `tags` + relevante indekser.
  - [ ] Implementer `/tasks/search` med støtte for projectId, status, tekst, frist.

  ## Frontend
  - [ ] Kanban-board (f.eks. `@dnd-kit/core` eller `vue3-dnd`) som persisterer statusendringer.
  - [ ] Filterpanel (status, dato, søk, tags) synkronisert mot route query params.
  - [ ] Debounced søk og derived state i Pinia (getters/memoisering).

  ## Akseptkriterier
  - Drag-and-drop oppdaterer status uten merkbar lagg og lagres i backend.
  - Filtre og søk fungerer både i UI og ved reload (URL-drevet state).
  ```
