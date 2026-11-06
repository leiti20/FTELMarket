# FTELMarket

API REST en C# (.NET 10) pour une place de marché de jeux vidéo (type Leboncoin ou Vinted).
Elle gère un catalogue de produits classés par catégorie, avec un CRUD complet
et une persistance PostgreSQL via Entity Framework Core (Code First).

TP « Installation et CRUD », Université de Rouen, FTEL Web & Digital.

## Technologies

- .NET 10, ASP.NET Core (contrôleurs)
- Entity Framework Core 10.0.11 (Code First, Fluent API)
- PostgreSQL 16 (Docker), connecteur Npgsql 10.0.3
- Front fourni : [`http://localhost:3000`](https://github.com/chrisfarin2/FTEL-MARKET-FRONT)

## Prérequis

- Visual Studio 2022 ou 2026 (charge de travail Développement web et ASP.NET)
- SDK .NET 10
- Docker Desktop
- DBeaver (facultatif)

## Installation et lancement

### 1. Lancer la base de données

```bash
docker run --name ftelmarket-db -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=ftelmarket -p 5433:5432 -d postgres:16
```

Les identifiants correspondent à la chaîne de connexion `FtelMarket` de `appsettings.json`
(hôte `localhost`, port `5433`, base `ftelmarket`, utilisateur `postgres`).

### 2. Appliquer les migrations

Dans le dossier du projet (celui qui contient `FTELMarket.csproj`) :

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

Les catégories et 8 produits de départ sont insérés automatiquement (seed).

### 3. Lancer l'API

Depuis Visual Studio (profil `http`) ou :

```bash
dotnet run
```

L'API écoute sur `http://localhost:5082`.

### 4. Lancer le front

Le front est accessible sur `http://localhost:3000` et appelle l'API
(le CORS autorise cette origine).

## Routes de l'API

| Méthode | Route | Description | Succès |
|---|---|---|---|
| GET | `/api/products` | Liste des produits | 200 |
| POST | `/api/products` | Crée un produit | 200 |
| PUT | `/api/products/{id}` | Modifie un produit | 200 (404 si absent) |
| DELETE | `/api/products/{id}` | Supprime un produit | 204 (404 si absent) |
| GET | `/api/categories` | Liste des catégories | 200 |


