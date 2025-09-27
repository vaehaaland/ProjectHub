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
  - [ ] API har `GET /health` og web har enkel `/`-side som svarer OK.

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

