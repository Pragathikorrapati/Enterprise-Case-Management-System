# Enterprise Case Management | Full Stack .NET + Angular

A fictional, original portfolio demonstration of enterprise case tracking. **Not code from any employer or client.**

## Features
- Accessible Angular dashboard with case search, status filters, new-case form, and status updates
- ASP.NET Core 8 REST API with EF Core, input validation, Swagger, and seeded demo records
- SQLite for zero-config local development; SQL Server option documented below
- Responsive layout, visible focus indicators, semantic form labels, keyboard-friendly controls, and live status messages

## Stack
C# · .NET 8 · ASP.NET Core Web API · EF Core · Angular 18 · TypeScript · HTML5 · CSS3 · SQLite / SQL Server

## Run locally
**Prerequisites:** .NET 8 SDK and Node.js 20+

Terminal 1:
```bash
cd backend/CaseManagement.Api
dotnet restore
dotnet run --urls http://localhost:5050
```
API: `http://localhost:5050/api/cases` · Swagger: `http://localhost:5050/swagger`

Terminal 2:
```bash
cd frontend
npm install
npm start
```
Open `http://localhost:4200`. Angular proxies `/api` to the backend.

## API
| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/cases` | List cases; optional `status` and `search` query parameters |
| GET | `/api/cases/{id}` | Get one case |
| POST | `/api/cases` | Create a case |
| PATCH | `/api/cases/{id}/status` | Update status |

Example:
```bash
curl -X POST http://localhost:5050/api/cases -H 'Content-Type: application/json' -d '{"title":"Review new filing","assignedTo":"Jordan Lee","priority":"High","description":"Validate supporting documents."}'
```

## SQL Server option
By default this demo uses a local SQLite file. To switch, set `Database:Provider` to `SqlServer`, supply a `ConnectionStrings:SqlServer` value in environment variables or user secrets, and start the API. This demo uses `EnsureCreated` for easy evaluation; production systems should use reviewed EF Core migrations.

## Architecture
```text
Angular standalone UI -> /api proxy -> ASP.NET Core controllers -> EF Core -> SQLite (or SQL Server)
```

## Accessibility notes
Uses native buttons, labeled controls, skip navigation, visible focus states, table headings, descriptive error messages, and `aria-live` feedback. These are accessibility-focused implementation practices, **not a claim of certified WCAG 2.1 AA compliance**; a full audit with automated and manual assistive-technology testing would still be required.

## Security / production considerations
This is a local demonstration without authentication. Before production use, add Entra ID/JWT authentication, role-based authorization, audit logging, pagination, migrations, rate limiting, structured monitoring, and integration tests. Do not deploy publicly as a production case-management system.

## Portfolio talking points
- Separated API controllers, persistence, and typed UI service calls.
- Implemented server-side validation and user-facing error handling.
- Designed responsive case workflow UI and keyboard-friendly controls.
- Built the sample from scratch with fictional records and no confidential client materials.

## Screenshots
Run the frontend and capture your own dashboard screenshot for this section. No screenshot is included because the UI was not browser-rendered in this environment.

## License
MIT — see LICENSE.
