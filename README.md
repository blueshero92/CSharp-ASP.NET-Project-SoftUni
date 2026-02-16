# 🚀 Gaming Zone App

A web-based game management application built with ASP.NET MVC, .NET 8, and SQL Server. 
Users can create, edit, and delete their own games, as well as add other users’ games to a personalized favorites list.

![.NET Version](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Features](#features)
- [Usage](#usage)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## 📖 About the Project

This project is a web-based Game Management application built with ASP.NET MVC on .NET 8, using SQL Server as the database.
The application allows users to manage their own game collection by creating, editing, and deleting games. Feautres for rating a game and soft delete will be added in the future.
In addition, users can browse games created by others and add them to a personalized favorites list for easy access.
The project follows the MVC (Model-View-Controller) architectural pattern and uses Entity Framework Core for database interaction and data management. 
It demonstrates full CRUD functionality, relational data handling, and user-based features within a structured and scalable .NET web application environment.
---

## 🛠️ Technologies Used

| Technology            | Version  | Purpose                          |
|-----------------------|----------|----------------------------------|
| ASP.NET Core MVC      | 8.22     | Web framework                    |
| Entity Framework Core | 8.22     | ORM / Database access            |
| SQL Server / SQLite   | -        | Database                         |
| Bootstrap             | 5.3      | Frontend styling                 |
| Razor Pages / Views   | -        | Server-side HTML rendering       |

---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/blueshero92/CSharp-ASP.NET-Project-SoftUni
CSharp-ASP.NET-Project-SoftUni
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Apply database migrations

```bash
dotnet ef database update
```

### 4. Run the application

```bash
dotnet run
```

The app will be available at `https://localhost:7038`

---

## 📁 Project Structure

```
GamingZoneApp/
│
├── Controllers/                          # MVC Controllers (Razor + MVC hybrid)
│   ├── `BaseController.cs`               # Base controller with common helpers (GetUserId)
│   ├── `HomeController.cs`               # Home, Privacy, Error actions
│   ├── `GamesController.cs`              # All game-related UI actions (Index, Add/Edit/Delete, Favorites)
│   ├── `DevelopersController.cs`         # Developers list and developer-specific games
│   └── `PublishersController.cs`         # Publishers list and publisher-specific games
│
├── Data/                                 # EF Core DbContext, configurations & migrations
│   ├── `GamingZoneDbContext.cs`          # Identity DbContext + OnModelCreating seeds
│   ├── Configuration/                     # IEntityTypeConfiguration implementations & seed data
│   │   └── `DeveloperEntityTypeConfiguraton.cs`
│   ├── Migrations/                        # EF Core generated migrations & model snapshot
│   │   ├── `GamingZoneDbContextModelSnapshot.cs`
│   │   └── `2026xxxx_UpdateSeededData.cs` (example migration)
│   └── Models/                            # Domain entities used by DbContext
│       ├── `Game.cs`
│       ├── `Developer.cs`
│       ├── `Publisher.cs`
│       ├── `ApplicationUser.cs`
│       └── `ApplicationUserGame.cs`
│
├── Services/                             # Business logic / service layer
│   ├── Core/                             # Implementation namespace
│   │   ├── `GameService.cs`
│   │   ├── `DeveloperService.cs`
│   │   └── `PublisherService.cs`
│   └── Interfaces/                       # Service contracts
│       ├── `IGameService.cs`
│       ├── `IDeveloperService.cs`
│       └── `IPublisherService.cs`
│
├── ViewModels/                           # View models and input models for views
│   ├── `ErrorViewModel.cs`
│   ├── Game/
│   │   ├── `GameInputModel.cs`
│   │   ├── `AllGamesViewModel.cs`
│   │   └── `DeleteGameViewModel.cs`
│   ├── Developer/
│   │   ├── `AllDevelopersViewModel.cs`
│   │   └── `AddGameDeveloperViewModel.cs`
│   └── Publisher/
│       ├── `AllPublishersViewModel.cs`
│       └── `AddGamePublisherViewModel.cs`
│
├── Views/                                # Razor Views (server-rendered)
│   ├── Shared/
│   │   ├── `_Layout.cshtml`              # Main layout (head, scripts, footer)
│   │   ├── `_LoginPartial.cshtml`
│   │   └── `_ViewImports.cshtml`
│   ├── Home/
│   │   ├── `Index.cshtml`
│   │   └── `Privacy.cshtml`
│   ├── Games/
│   │   ├── `Index.cshtml`
│   │   ├── `GameDetails.cshtml`
│   │   ├── `AddGame.cshtml` / `EditGame.cshtml`
│   │   └── `DeleteGame.cshtml`
│   ├── Developers/
│   │   └── `Index.cshtml`, `DeveloperGames.cshtml`
│   └── Publishers/
│       └── `Index.cshtml`, `PublisherGames.cshtml`
│
├── Areas/
│   └── Identity/                         # ASP.NET Identity UI (Razor Pages)
│       ├── Pages/
│       │   └── Account/ (Login/Register/Manage pages)
│       └── `_ViewStart.cshtml`           # Sets layout for Identity area
│
├── wwwroot/                              # Static files served directly
│   ├── css/
│   │   └── `site.css`                    # Main custom CSS (theme + utilities)
│   ├── js/
│   │   └── `site.js`
│   ├── images/
│   │   ├── `developer.png`
│   │   ├── `publisher.png`
│   │   └── `game/gameDefault.png`
│   ├── lib/                              # Client libraries (bootstrap, jquery, jquery-validation)
│   └── `favicon.ico`
│
├── appsettings.json                      # Production/default configuration (not in source? check)
├── appsettings.Development.json          # Local dev configuration (connection string + Identity options)
│
├── Program.cs                            # App entry point, DI registration, middleware & routing
├── README.md                             # Project README
├── LICENSE                               # MIT license
└── `GamingZoneApp.csproj`                # Project file (target: .NET 8)
```

---

## ✨ Features

- [x] User registration and login (ASP.NET Identity)  
  - Identity is present and seeded (see `AspNetUsers` seed in migrations / `ApplicationUser`).
- [x] CRUD operations for `Game` entity  
  - Game views and actions exist (e.g. `GameDetails.cshtml`, routes for `AddGame`, `EditGame`, `DeleteGame`) and seeded games are present in migrations.
- [x] Input validation (server-side & client-side)  
  - Server-side validation is supported via model validation / data annotations (typical in Razor projects). Client-side validation libraries are included (`wwwroot/lib/jquery-validation`).
- [x] Responsive UI with Bootstrap  
  - Bootstrap is referenced in the layout (`_Layout.cshtml`) and the views use Bootstrap classes for responsive layout.

---

## 💻 Usage

```
Follow these steps after the app is running locally to use the main features.

Prerequisites
- Ensure the database is created/migrated: run `dotnet ef database update` (or use Visual Studio __Package Manager Console__ with __Update-Database__).
- Start the app: `dotnet run` or press Run in Visual Studio. Open the URL shown in the console (e.g. `https://localhost:7038`).

Authentication
1. Register a new account: navigate to `/Identity/Account/Register`.
2. Log in: navigate to `/Identity/Account/Login`.

Seeded admin (development)
- Username: `admin`  
- Email: `admin@example.com`  
- Password: `Admin123!`  
(You can register another account for testing the functionality.)

Core workflows
1. Browse games
   - Open `GET /Games` to see the catalog. Click a title to open `GET /Games/GameDetails?id={gameId}`.

2. Add a game (authenticated)
   - Go to `GET /Games/AddGame`.
   - Fill the form: `Title`, `ReleaseDate`, `Genre` (select), `Description`, `ImageUrl` (absolute URL or site path starting with `/images/...`), `Developer` (dropdown), `Publisher` (dropdown).
   - Submit the form (POST to `/Games/AddGame`). New games are associated with your account.

3. Edit / Delete a game (creator only)
   - On a game details page, use `Edit` (`GET /Games/EditGame?id={gameId}` / `POST /Games/EditGame`) or `Delete` (`GET /Games/DeleteGame?id={gameId}` / `POST /Games/DeleteGame`). The app enforces that only the creator can edit or delete.

4. My Games & Favorites
   - `GET /Games/MyGames` — list games you created.
   - `GET /Games/MyFavoriteGames` — list games you added to favorites.
   - Add to favorites (POST) — action invoked from game details (controller handles `AddToFavorites` and `RemoveFromFavorites`).

5. Developers & Publishers
   - `GET /Developers` — list of seeded developers. Click a developer to view their games (`GET /Developers/DeveloperGames?developerId={id}`).
   - `GET /Publishers` — list of seeded publishers. Click a publisher games (`GET /Publishers/PublisherGames?publisherId={id}`).

Notes & tips
- Navigation links for authenticated features (My Games / Favorites / Add Game) appear in the nav when signed in.
- Image files live under `wwwroot/images/`. When seeding, `ImageUrl` values commonly use site-root paths such as `/images/game/gameDefault.png`.
- If you change seed data, create a migration via __Add-Migration__ and apply it via __Update-Database__ (or use the `dotnet ef` CLI).
- If the favicon or other static assets don't update, hard-refresh the browser (Ctrl+F5) or open an Incognito/private window.
```


---

## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach.

Connection string is configured in `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=GamingZoneApp;Trusted_Connection=True;Encrypt=False"
}
```

To create and seed the database:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## ⚙️ Configuration

Key settings in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "GamingZoneDbConnection": "Server=.;Database=GamingZoneApp;Trusted_Connection=True;Encrypt=False"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "IdentityOptions": {
    "SignIn": {
      "RequireConfirmedAccount": false,
      "RequireConfirmedEmail": false,
      "RequireConfirmedPhoneNumber": false
    },
    "User": {
      "RequireUniqueEmail": true
    },
    "Password": {
      "RequireDigit": true,
      "RequireLowercase": true,
      "RequireNonAlphanumeric": false,
      "RequireUppercase": false,
      "RequiredLength": 6
    },
    "Lockout": {
      "DefaultLockoutTimeSpan": "00:01:00",
      "MaxFailedAccessAttempts": 10
    }
```

> ⚠️ **Never commit sensitive data** (passwords, API keys) to source control. Use `appsettings.Development.json` or environment variables for local secrets.

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add some feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 📬 Contact

**Deyan Dimitrov** – https://github.com/blueshero92

Project Link: https://github.com/blueshero92/CSharp-ASP.NET-Project-SoftUni
---

*Built as part of the **ASP.NET Fundamentals** course.*
