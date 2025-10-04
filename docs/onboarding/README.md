# PlayFab Systems — Onboarding & Architecture Guide

Welcome to **PlayFab Systems**, a scalable game backend architecture built for Unity clients, PlayFab CloudScript servers, and modular CI/CD infrastructure.  
This document helps you understand the project layout, setup steps, and design philosophy.

---

## 🚀 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/YourOrg/playfab-systems.git
cd playfab-systems
```

### 2. Install dependencies
#### Unity side
- Unity 2022.3+ (LTS)
- TextMeshPro, Addressables
- PlayFab SDK (via Unity Package Manager)

#### Server side
- Node.js 18+
- PlayFab CloudScript SDK

#### Ops tools
- Docker (for local testing)
- GitHub Actions (optional)
- AWS CLI / PlayFab CLI (for deployment)

---

## 🧱 Project Structure

```
playfab-systems/
│
├── core/                  # Engine-agnostic domain and usecases
│   ├── domain/            # Entities, value objects, aggregates
│   ├── usecase/           # Application logic, services, commands
│   └── shared/            # Common utilities, error handling, events
│
├── infra/                 # Infrastructure adapters and integrations
│   ├── playfab/           # API clients, auth, player data sync
│   ├── storage/           # Cloud save, inventory, leaderboard
│   └── analytics/         # Telemetry and A/B testing tools
│
├── client-unity/          # Unity-specific presentation and glue layer
│   ├── scripts/           # Monobehaviours, presenters, DI containers
│   ├── ui/                # UI canvases, prefabs, scenes
│   ├── resources/         # ScriptableObjects, configs
│   └── bootstrap/         # Startup, dependency injection, lifecycle
│
├── server-functions/      # PlayFab CloudScript & backend logic
│   ├── functions/         # Individual server endpoints
│   ├── shared/            # Common logic reused between functions
│   └── tests/             # Unit tests for CloudScript
│
├── ops/                   # CI/CD and infrastructure
│   ├── github/            # GitHub Actions pipelines
│   ├── docker/            # Dockerfiles and local stacks
│   └── scripts/           # Automation tools and deployment scripts
│
├── docs/                  # Documentation, diagrams, onboarding
│
└── README.md
```

---

## 🧩 Architecture Overview

This repository follows a **clean, layered architecture** designed to isolate business logic from frameworks.

### Layer Dependencies
```
domain → usecase → infra → client
```

### Layer Responsibilities

#### **core/**
- Pure business logic.  
- No Unity or network dependencies.  
- Defines domain models (Player, Inventory, Quest).  
- Contains services like `LoginUseCase`, `GrantRewardUseCase`.

#### **infra/**
- Implements interfaces defined in `core/`.  
- Connects domain to external systems (PlayFab, storage, telemetry).  
- Example: `PlayFabAuthRepository` implements `IAuthRepository`.

#### **client-unity/**
- Presentation and input layer.  
- Scene logic, UI, and game loops.  
- Injects dependencies and runs gameplay using `core` logic.

#### **server-functions/**
- Stateless PlayFab CloudScript endpoints.  
- Wraps core logic for backend execution.  
- Handles leaderboard updates, server rewards, match data, etc.

#### **ops/**
- Build and deploy automation.  
- Manages CI/CD pipelines, Docker images, and environment configuration.

---

## 🧠 Example Flow

1. **Login Scene** initializes `PlayFabAuthRepository` (infra).  
2. Repository injected into `LoginUseCase` (core).  
3. `LoginUseCase` runs validation, triggers PlayFab API call.  
4. Response updates player domain entity.  
5. `UnityPresenter` displays the result on screen.  
6. CloudScript updates remote data asynchronously.

---

## 🧰 Tech Stack

| Layer | Tech | Description |
|-------|------|-------------|
| Core | C# | Engine-agnostic domain logic |
| Client | Unity 2022 LTS | UI & game client |
| Infra | PlayFab SDK, REST | Backend integrations |
| Server | Node.js + PlayFab CloudScript | Server logic |
| Ops | Docker, GitHub Actions | CI/CD automation |

---

## ⚙️ Development Workflow

1. **Implement domain logic** in `core/domain` or `core/usecase`.
2. **Add infra adapters** to connect real services (PlayFab, analytics, etc.).
3. **Bind in Unity** via dependency injection (Zenject/Extenject recommended).
4. **Test locally** using PlayFab’s local server or mock adapters.
5. **Deploy CloudScript** using `ops/scripts/deploy_cloudscript.sh`.
6. **Push to main branch** — GitHub Actions runs build and deploy.

---

## 🧪 Testing

### Unit Tests
Run all tests:
```bash
dotnet test core/
```

### CloudScript Tests
```bash
cd server-functions
npm test
```

---

## 🚀 Deployment

### To PlayFab CloudScript
```bash
cd ops/scripts
./deploy_cloudscript.sh
```

### To Unity Cloud Build
- Configure build targets in Unity Cloud Dashboard.
- Link repo to Unity Cloud Build.
- Define environment variables for PlayFab title ID and keys.
- Enable automatic deployment from main branch.

---

## 📦 CI/CD

GitHub Actions automates builds and deployment.

**Example workflow:**
```yaml
name: PlayFab CI

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Build Unity Client
        run: ./ops/scripts/build_unity.sh
      - name: Deploy CloudScript
        run: ./ops/scripts/deploy_cloudscript.sh
```

---

## 🧭 Design Goals

- ✅ Full separation between domain, infrastructure, and client.
- ✅ Easy to extend for new platforms or services.
- ✅ Automated deployment for fast iteration.
- ✅ Testable core logic (independent of Unity).
- ✅ Consistent coding standards and architecture enforcement.

---

## 👥 Team Setup

| Role | Responsibility |
|------|----------------|
| Game Developer | Unity client implementation |
| Backend Engineer | PlayFab CloudScript + infra logic |
| DevOps | CI/CD, environments, deployment |
| Designer | UI/UX in Unity scenes |
| QA | Testing across client and server layers |

---

## 🧭 Summary

**PlayFab Systems** provides a modular foundation for scalable online games.  
By following the `domain → usecase → infra → client` pattern, it ensures maintainability, testability, and future-proof design for both Unity and PlayFab environments.
