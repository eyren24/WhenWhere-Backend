# WhenWhere – Backend

## Stack Tecnologico
- **Framework:** .NET 9 (C#)
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **API:** RESTful, autenticazione tramite JWT
- **Strumenti:** openapi-cli-generator per client, log applicativi di base

---

## Architettura del Backend
Architettura a livelli:

- **Controllers (API Layer):** espongono endpoint REST protetti tramite JWT
- **Business/Repository Layer:** logica applicativa + accesso al database via EF Core
- **Data Layer:** schema SQL Server relazionale

---

## Modello Dati (Sintesi)
**Utente**
- id, username, email, passwordHash, ruolo

**Agenda**
- id, utenteId, nomeAgenda, descrizione, temaHex, isPrivate, createdAt

**Evento**
- id, agendaId, titolo, descrizione, startDate, endDate, luogo?

**Nota**
- id, agendaId, contenuto, temaHex

**Like**
- id, agendaId, utenteId, createdAt
- vincolo: un like per utente per agenda (chiave composta)

---

## Flusso di Autenticazione JWT
1. L'utente invia credenziali tramite `/auth/login`
2. Il server genera un token JWT contenente info utente
3. Il client include il token nell'header `Authorization: Bearer <token>`
4. I controller validano il token e autorizzano l'accesso

---

## Endpoint Principali
### Autenticazione
- `POST /auth/login`
- `POST /auth/register`

### Agende
- `GET /agende` (private + pubbliche dell'utente loggato)
- `POST /agende`
- `PUT /agende/{id}`
- `DELETE /agende/{id}`
- `GET /agende/pubbliche` (social)

### Eventi
- `POST /eventi`
- `PUT /eventi/{id}`
- `DELETE /eventi/{id}`

### Note
- `POST /note`
- `DELETE /note/{id}`

### Social (Like)
- `POST /like/{agendaId}`
- `DELETE /like/{agendaId}`

### Admin
- `GET /admin/utenti`
- `DELETE /admin/utenti/{id}`
- `GET /admin/stats`

---

## Avvio in Sviluppo
Prerequisiti:
- .NET 9
- SQL Server attivo

Comandi:
```bash
dotnet restore
dotnet run
```

Il backend espone le API su localhost.

---

## Esecuzione & Configurazione
- Configurare connection string in *appsettings.json*
- Migrazioni EF:
```bash
dotnet ef migrations add Init
dotnet ef database update
```

---

## Sviluppi Futuri
- Integrazione Google Calendar API
- Endpoint per allegati media
- Ricerca e filtri avanzati lato server
- Ruoli amministrativi granulari
- Test automatici e validazioni aggiuntive

