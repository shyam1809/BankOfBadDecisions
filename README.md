# 🏦 Bank of Bad Decisions (ASP.NET Core MVC + SQLite)

A parody banking app that **rewards terrible financial choices** with badges, leaderboards, and roast-style reports.

## ✅ Quick Start (Local)

### Prereqs
- .NET 8 SDK
- VS Code + C# extension
- (Optional) Docker Desktop

### Run
```bash
dotnet restore
dotnet run
```
Open http://localhost:5253 (or the printed URL), then click around.

Database `app.db` is auto-created with demo data.

## 🧪 Trigger Chaos
Go to **Transactions**:
- **💸 Overspend** — adds a random expense
- **⏰ Pay Late Fee** — adds a fee
- **🤑 Take Loan** — inflates your balance (temporarily)

Badges are auto-awarded by rules in `Services/BadgeService.cs`.

## 🐳 Docker

Build & run:
```bash
docker build -t bankofbaddecisions .
docker run -p 8080:8080 bankofbaddecisions
```
Open http://localhost:8080

## 🗃️ Entity Framework (Optional Migrations)
We use `EnsureCreated()` for demo. For production, switch to migrations:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Then replace `EnsureCreated()` with `Database.Migrate()` in `Program.cs`.

## 🔁 Jenkins CI → DockerHub

1. Create a DockerHub repo: `your-dockerhub-username/bankofbaddecisions`.
2. In Jenkins (LTS recommended), add credentials:
   - **ID**: `dockerhub-cred`
   - **Kind**: Username with password (DockerHub)
3. Create a Pipeline job pointing at your GitHub repo.
4. Use the provided `Jenkinsfile` (Linux agent recommended).
5. On each commit: **build → docker build → push**.

## 🚀 Free Hosting Options (as of Aug 2025)
- **Render** free instances for web services (deploy from GitHub via Dockerfile). See official docs. 
- **Koyeb** free instance type for small services (deploy from Docker image).

> Free tiers change often; check their docs before relying on them.

### Deploy on Render (Dockerfile from GitHub)
1. Push this project to GitHub.
2. Create a **Web Service** on Render.
3. Connect your GitHub repo, it will detect the Dockerfile.
4. Choose **Free** instance type.
5. Set port to **8080** if asked (we expose 8080 in Dockerfile).
6. Deploy → open the URL.

### Deploy on Koyeb (from DockerHub image)
1. After Jenkins pushes to DockerHub, go to Koyeb → Create Service.
2. Choose **Container** → `docker.io/your-dockerhub-username/bankofbaddecisions:latest`.
3. Runtime environment: Port **8080**.
4. Choose **free** instance type.
5. Deploy.

## 📂 Project Structure
```
BankOfBadDecisions/
  Controllers/
  Data/
  Models/
  Services/
  Views/
  wwwroot/
  Dockerfile
  Jenkinsfile
  appsettings.json
  Program.cs
  BankOfBadDecisions.csproj
```

## ✅ Notes
- Single demo user is seeded. Add more users via DB to play with Leaderboard.
- This app is for **education & fun**—not real finance advice. 😄
