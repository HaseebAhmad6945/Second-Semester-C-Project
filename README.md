# 💡 Hospital Management System (Second-Semester C# Project)

A clean, user-friendly Hospital Management System (HMS) built with C# (Windows Forms) and a simple Console interface. This project demonstrates Object-Oriented Programming (OOP) principles, layered architecture, and real-world features required to run basic hospital operations.

---

📌 Project Snapshot

- Name: Hospital Management System (HMS)
- Technology: C#, Windows Forms, .NET / .NET Framework
- Database: SQL Server (scripts included if present)
- Interfaces: GUI (Windows Forms) and Console

---

🧭 Table of Contents

- [Overview](#overview)
- [Motivation & Problem Statement](#motivation--problem-statement)
- [Key Features](#key-features)
- [Architecture & Design](#architecture--design)
- [Domain Model & Modules](#domain-model--modules)
- [Database Design & Setup](#database-design--setup)
- [Installation & Setup](#installation--setup)
- [Usage & Common Workflows](#usage--common-workflows)
- [Configuration (Connection String & Seeds)](#configuration-connection-string--seeds)
- [Testing & Screenshots](#testing--screenshots)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

This Hospital Management System centralizes core hospital operations: patient records, appointments, billing, prescriptions, inventory, and user role management. It reduces manual paperwork and helps standardize workflows across staff roles (Admin, Doctor, Receptionist, Nurse, Pharmacist, Lab Technician).

The application is split into layered components for maintainability and testability. The UI layer is implemented with Windows Forms for a desktop experience and a minimal Console interface for quick interactions.

---

## Motivation & Problem Statement

Manual hospital systems suffer from:
- Lost or duplicated patient records
- Billing errors and inconsistent invoices
- Inefficient appointment handling and double-bookings
- Hard-to-track prescriptions and inventory
- Unauthorized access to sensitive data

This project aims to provide a lightweight, secure, and extendable HMS suitable for learning and small-scale deployments.

---

## Key Features

- 🔐 Role-based authentication (Admin, Doctor, Receptionist, Nurse, Pharmacist, Lab Technician)
- 🧾 Patient record management (CRUD with validations)
- 🩺 Doctor and staff management
- 📅 Appointment scheduling with conflict prevention
- 💳 Billing and transactional payment processing
- 💊 Prescription generation and basic inventory management
- 🗃️ SQL Server persistence with parameterized queries to prevent SQL injection
- 🧩 Modular layered design (UI, Business Logic, Service, Data Access)

---

## Architecture & Design

This project follows a layered architecture to keep responsibilities separated and to simplify testing and maintenance.

- UI (Windows Forms / Console) — Presents data and collects user input
- Business Logic (BL) — Enforces rules, validations, and domain workflows
- Service Layer — Coordinates complex operations and authorization checks
- Data Access Layer (DAL) — Encapsulates SQL Server interactions (CRUD, transactions)

Security highlights:
- Parameterized queries to prevent SQL injection
- Transaction handling for billing and inventory updates
- Role checks in service/business layers before sensitive operations

---

## Domain Model & Modules

Main modules and responsibilities:
1. User Management — authentication, roles, profile management
2. Patient Management — demographics, medical history, notes
3. Doctor Management — doctors, specialties, availability
4. Appointment Management — schedule, reschedule, prevent double-booking
5. Billing Management — generate bills, payments, transactional safety
6. Prescriptions & Inventory — prescribe medication, update stock

Each module has its own set of classes in the corresponding layer (BL/DAL) and unit responsibilities are kept small and testable.

---

## Database Design & Setup

Primary tables (example): Users, Patients, Doctors, Appointments, Prescriptions, Bills, Inventory.

- Keep schema normalized (avoid duplicate patient info across tables)
- Use appropriate indexes for lookup fields (e.g., PatientID, AppointmentDate, DoctorID)
- Use transactions for multi-step operations (billing + inventory update)

If DB scripts exist in the repository (look under `/database` or `/scripts`), run those to restore the schema and seed data.

---

## Installation & Setup

Prerequisites:
- Windows 10/11 (or compatible)
- Visual Studio with Windows Forms workload
- .NET Framework or .NET SDK matching the solution (check project files)
- SQL Server (Express or full)

Steps:
1. Clone the repository:
   git clone https://github.com/HaseebAhmad6945/Second-Semester-C-Project.git
2. Open the solution (.sln) in Visual Studio and restore NuGet packages.
3. Restore or create the SQL Server database using provided scripts (if present).
4. Update the connection string in the appropriate appsettings / config file (see Configuration section below).
5. Build the solution and run the GUI or Console app.

---

## Configuration (Connection String & Seeds)

- Connection string: update to your SQL Server instance in the project configuration (App.config / appsettings.json / settings file depending on the project). Example:
  Server=YOUR_SERVER;Database=HMS_DB;Trusted_Connection=True;MultipleActiveResultSets=true;

- Seed accounts (example): The project may seed default users (Admin/Doctor). If not, create an Admin user directly in the DB or via the registration workflow.

---

## Usage & Common Workflows

Common tasks and where to find them in the UI:
- Login: Use seeded credentials or create an account; roles determine access.
- Add Patient: Navigate to Patients → New Patient → fill details and save.
- Schedule Appointment: Appointments → New → Select Patient & Doctor → Choose time (system prevents double-booking).
- Billing: Generate bill after appointment; transactions update inventory and billing tables atomically.
- Prescriptions: From an appointment or doctor dashboard, create prescription and adjust inventory.

## Developer Notes

- Code style: follow single-responsibility for classes and keep UI code thin (push logic to BL/Services).
- Logging: add logging for critical flows (authentication, billing) to help debugging.
- Extendability: add unit tests for BL and DAL using a test framework (NUnit/xUnit) and use a test database or in-memory provider where possible.

---

## Contributing

1. Fork the repo, create a feature branch, make changes, and open a pull request.
2. Add unit tests where appropriate and document behavior changes.
3. Keep commits focused and descriptive.

---

## License
If this is for coursework, include a note about usage and attribution. Otherwise, add an open-source license (e.g., MIT) by adding a LICENSE file.



