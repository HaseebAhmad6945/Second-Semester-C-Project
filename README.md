# Hospital Management System (Second-Semester C# Project)

This project is a Hospital Management System (HMS) developed using C# (Windows Forms) that demonstrates Object-Oriented Programming (OOP) principles and a layered architecture. It provides both a GUI (Windows Forms) and a Console interface and uses SQL Server for persistent storage.

---

## Project Snapshot

### Project Logo
![Hospital logo](images/hospital.svg)

### Quick Highlights
![Features icon](images/features.svg) ![Architecture icon](images/architecture.svg)

---

## Table of Contents

- [Overview](#overview)
- [Motivation & Problem Statement](#motivation--problem-statement)
- [Key Features](#key-features)
- [Architecture & Design](#architecture--design)
  - [Four-tier / Layered Architecture](#four-tier--layered-architecture)
  - [User Interface (UI) Layer](#user-interface-ui-layer)
  - [Business Logic (BL) Layer](#business-logic-bl-layer)
  - [Service Layer](#service-layer)
  - [Data Access Layer (DL)](#data-access-layer-dl)
- [Domain Model & Modules](#domain-model--modules)
- [Database Design](#database-design)
- [System Diagrams](#system-diagrams)
- [Test Cases / Screenshots (Examples)](#test-cases--screenshots-examples)
- [Requirements & Setup](#requirements--setup)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

The Hospital Management System automates the daily operations of a hospital to eliminate manual paperwork, reduce errors, and improve data accessibility. It centralizes patient records, appointments, billing, staff management, and inventory in a secure, role-based system.

## Motivation & Problem Statement

Manual hospital record-keeping causes:
- Loss or duplication of patient records
- Incorrect billing calculations
- Difficulty in managing appointments
- No centralized patient history
- Unauthorized data access
- Inefficient staff management

Consequences include poor decision-making, reduced operational efficiency, increased administrative workload, and patient dissatisfaction. This HMS aims to address these issues through automation, validation, and secure data storage.

## Key Features

- User authentication with role-based access control (Admin, Doctor, Receptionist, Nurse, Pharmacist, Lab Technician)
- Patient record management (create, read, update, delete)
- Doctor and staff management
- Appointment scheduling and tracking
- Billing and payment processing with transaction handling
- Prescription and inventory management
- Secure storage using SQL Server
- Dual interface: GUI (Windows Forms) and Console
- Business rules and validation to maintain data integrity

---

## Architecture & Design

This project follows a layered design to separate concerns and improve maintainability and testability.

### Four-tier / Layered Architecture
![Architecture icon](images/architecture.svg)

- UI (User Interface) Layer
- Business Logic (BL) Layer
- Service Layer (prevents unauthorized direct DB access, coordinates services)
- Data Access Layer (DL)

### User Interface (UI) Layer
Purpose: Present data and accept user input via Windows Forms and a Console interface.
Responsibilities:
- Display login, dashboards, forms
- Route user actions to BL
- Show validation messages and results
Note: UI never communicates directly with the database; it uses BL / Service layer.

### Business Logic (BL) Layer
Purpose: Implements hospital rules and workflows.
Responsibilities:
- Validate user credentials and enforce role-based access
- Enforce business rules (e.g., only assigned doctors see appointments)
- Coordinate data flows between UI and DL
- Perform calculations (billing totals, inventory updates)

### Service Layer
Purpose: Acts as a gatekeeper for database operations, performing authorization checks and preventing unauthorized DB access. Coordinates complex operations that may span BL and DL.

### Data Access Layer (DL)
Purpose: All SQL Server database interaction; uses parameterized queries and transactions.
Responsibilities:
- CRUD operations
- Transaction handling (billing, inventory)
- Mapping database records to application objects
- Prevent SQL injection (parameterized queries)

Security measures include parameterized queries, controlled DB access, and transaction management for critical operations.

---

## Domain Model & Modules

Main modules include:

1. User Management
2. Patient Management
3. Doctor Management
4. Appointment Management
5. Billing Management
6. Prescriptions & Inventory

(See earlier sections for details.)

---

## Database Design

The database follows relational modeling and normalization rules. Example main tables: Users, Patients, Doctors, Appointments, Prescriptions, Bills, Inventory.

---

## System Diagrams

Below are the main design diagrams for the project. The images are included in the repository under `images/diagrams/`:

- Programmer Diagram (images/diagrams/programmer_diagram.png) — shows OOP concepts (encapsulation, inheritance, polymorphism) and class responsibilities.
- Layered Architecture Diagram (images/diagrams/layered_architecture.png) — shows UI, BL, Service Layer, and DL interactions.
- Flow Diagram (images/diagrams/flow_diagram.png) — shows the login → role check → dashboards → save data flow.

Programmer Diagram (Image 1)  
![Programmer Diagram](images/diagrams/programmer_diagram.png)

Layered Architecture Diagram (Image 2)  
![Layered Architecture](images/diagrams/layered_architecture.png)

Flow Diagram (Image 3)  
![Flow Diagram](images/diagrams/flow_diagram.png)

(If the images do not display, make sure the files are present at the paths above. If you prefer the images placed in `docs/diagrams/` instead, I can move them.)

---

## Test Cases / Screenshots (Examples)

- Sign Up / Registration (email uniqueness, password length)
- Manage Patients (add/update/delete, validation)
- Manage Appointments (link patient and doctor, prevent double-booking)
- Billing (generate after appointment, transactional updates)

Include screenshots of forms and dashboards under `/docs/screenshots` if available.

---

## Requirements & Setup

- Visual Studio (Windows Forms support)
- .NET / .NET Framework (as used by project)
- SQL Server (Express or full)
- Update connection string in app settings
- Restore DB using scripts in /database or /scripts if included

Basic steps:
1. Restore the SQL Server DB using provided scripts.
2. Update connection string.
3. Build the solution.
4. Run GUI or Console and log in with seeded credentials (if provided).

---

## Contributing

- Fork → feature branch → pull request
- Add tests and follow coding style

---

## License

Add a LICENSE file (e.g., MIT) or specify institution-specific terms.

