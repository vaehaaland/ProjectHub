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

