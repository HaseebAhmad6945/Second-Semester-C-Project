CREATE DATABASE HospitalManagementSystem;
GO

USE HospitalManagementSystem;
GO

USE HospitalManagementSystem;
GO

DROP TABLE IF EXISTS Prescriptions;
DROP TABLE IF EXISTS Bills;
DROP TABLE IF EXISTS Appointments;
DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS Pharmacists;
DROP TABLE IF EXISTS LabTechnicians;
DROP TABLE IF EXISTS Nurses;
DROP TABLE IF EXISTS Patients;
DROP TABLE IF EXISTS Doctors;
DROP TABLE IF EXISTS Users;
GO

-- Create Users table first (no dependencies)
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) CHECK 
    (Role IN ('Admin','Doctor','Patient','Nurse','LabTechnician','Pharmacist')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create Doctors table (depends on Users)
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT UNIQUE,
    Specialization NVARCHAR(100) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create Patients table (depends on Users)
CREATE TABLE Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT UNIQUE,
    BloodGroup NVARCHAR(5),
    DateOfBirth DATE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create Nurses table
CREATE TABLE Nurses (
    NurseID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT UNIQUE NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    Shift NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create LabTechnicians table
CREATE TABLE LabTechnicians (
    LabTechnicianID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT UNIQUE NOT NULL,
    LabType NVARCHAR(100) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create Pharmacists table
CREATE TABLE Pharmacists (
    PharmacistID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT UNIQUE NOT NULL,
    LicenseNumber NVARCHAR(50) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create Appointments table
CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT,
    DoctorID INT,
    AppointmentDate DATETIME NOT NULL,
    Status NVARCHAR(50) CHECK (Status IN ('Pending','Completed','Cancelled')),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

-- Create Bills table
CREATE TABLE Bills (
    BillID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    AppointmentID INT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaidAmount DECIMAL(10,2) DEFAULT 0,
    BalanceAmount AS (TotalAmount - PaidAmount),
    PaymentStatus NVARCHAR(20) CHECK 
        (PaymentStatus IN ('Pending', 'Partial', 'Paid')) NOT NULL,
    PaymentMethod NVARCHAR(50) CHECK
        (PaymentMethod IN ('Cash', 'Card', 'Online')),
    BillingDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID)
);

-- Create Prescriptions table
CREATE TABLE Prescriptions (
    PrescriptionID INT IDENTITY(1,1) PRIMARY KEY,
    AppointmentID INT NOT NULL,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    MedicineName NVARCHAR(200) NOT NULL,
    Dosage NVARCHAR(100) NOT NULL,
    Duration NVARCHAR(100) NOT NULL,
    Instructions NVARCHAR(MAX),
    PrescribedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID),
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

-- Create Inventory table
CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    MedicineName NVARCHAR(200) NOT NULL,
    Category NVARCHAR(100),
    Manufacturer NVARCHAR(200),
    UnitPrice DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    ExpiryDate DATE,
    ReorderLevel INT DEFAULT 10,
    LastRestocked DATETIME DEFAULT GETDATE(),
    CreatedAt DATETIME DEFAULT GETDATE()
);
