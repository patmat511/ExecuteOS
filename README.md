<div align="center">

# <img width="1700" height="460" alt="github-header-banner (2)" src="https://github.com/user-attachments/assets/7a0cd499-90f1-48c1-aea5-1dbb058c2353" />



**Modern • Scalable • Production-Ready**

[Features](#features) • [Architecture](#-architecture) • [Getting Started](#-getting-started) • [Tech Stack](#-tech-stack)

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-19-DD0031?style=flat&logo=angular)](https://angular.io/)
[![EF Core](https://img.shields.io/badge/EF_Core-9.0-512BD4?style=flat&logo=nuget)](https://learn.microsoft.com/ef/core/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.0-3178C6?style=flat&logo=typescript)](https://www.typescriptlang.org/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

</div>

---

## About

**ExecuteOS** is a full-stack productivity and time management system built with **ASP.NET Core 8** and **Angular 19**. 
The project demonstrates enterprise-grade architecture patterns including **Modular Monolith**, **Vertical Slice Architecture**, and a clean separation of concerns. It is designed as a portfolio-grade project aligned with real-world backend development standards.

---

## 🏗️ Architecture

ExecuteOS follows a **Modular Monolith** architecture with **Vertical Slice** organization.

### System Flow

<img width="625" height="766" alt="image" src="https://github.com/user-attachments/assets/fe947de3-25bc-4d9b-8029-af466c80bc8a" />

## Architectural Principles
* Feature-based Vertical Slices: Keeping related code together.
* Repository Pattern: Abstraction over Entity Framework Core.
* Explicit Service Layer: For complex business logic.
* DTOs: For clean API contracts and data isolation.
* Async-first Design: Full use of CancellationToken for performance.
* Dependency Injection: Throughout the entire application.
---

## ✨ Features
* **Full CRUD operations** for tasks.
* **Status workflow:** New, InProgress, Completed.
* **Priority levels:** Low, Medium, High.
* **Time Tracking:** Estimated vs actual time tracking.
* **Deadlines:** Optional deadlines and filtering by status.

### ⏱️ Time Tracking (Planned)
* Manual time entries and aggregated reports.
* Task-based tracking sessions.

### 📈 Analytics (Planned)
* Productivity metrics and completion statistics.
* Time distribution insights.

---

## 🛠️ Tech Stack
Backend
* .NET 9 (ASP.NET Core Web API)
* Entity Framework Core 9
* SQL Server
* Swagger / OpenAPI 

Frontend
* Angular 19
* TypeScript & RxJs
* Angular Material

---

## 🚀 Getting Started
### Prerequisites
- .NET SDK (latest LTS)
- Node.js + npm
- SQL Server (local or containerized)
- EF Core CLI

**1 .Clone & Setup**

```Bash

git clone https://github.com/patmat511/ExecuteOS.git
cd ExecuteOS

```
**2. Backend Setup**
```Bash
cd ExecuteOS.Server
dotnet restore
dotnet ef database update
dotnet run
```

**3. Frontend Setup**
```Bash
cd executeos.client
npm install
npm start
```
