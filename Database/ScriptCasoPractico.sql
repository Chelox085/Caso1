USE master;
GO
IF DB_ID(N'CASO_PRACTICO_RESERVACIONES') IS NOT NULL
BEGIN
    ALTER DATABASE CASO_PRACTICO_RESERVACIONES SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CASO_PRACTICO_RESERVACIONES;
END
GO

CREATE DATABASE CASO_PRACTICO_RESERVACIONES;
GO

USE CASO_PRACTICO_RESERVACIONES;
GO

CREATE TABLE HABITACIONES (
    Id int identity (1,1) primary key not null,
    CodigoDeHabitacion varchar(7) not null,
    NombreDeHabitacion varchar(30) not null,
    CantidadDeHuespedesPermitidos int not null,
    CantidadDeCamas int not null,
    CantidadDeBanos int not null,
    Ubicacion varchar(10) not null,
    EncargadoDeLimpieza varchar(100) not null,
    TipoDeHabitacion int not null,
    CostoDeLimpieza decimal(18,2) not null,
    CostoDeReserva decimal(18,2) not null,
    FechaDeRegistro datetime not null,
    FechaDeModificacion datetime null,
    Estado bit not null
);
GO
CREATE TABLE RESERVACIONES (
    Id int identity (1,1) primary key not null,
    NombreDeLaPersona varchar(150) not null,
    Identificacion varchar(30) not null,
    Telefono varchar(10) not null,
    Correo varchar(50) not null,
    FechaNacimiento datetime not null,
    Direccion varchar(200) not null,
    MontoTotal decimal(18,2) not null,
    FechaInicioReserva datetime not null,
    FechaFinReserva datetime not null,
    FechaDeRegistro datetime not null,
    IdHabitacion int not null,
    CONSTRAINT FK_Reservacion_Habitacion FOREIGN KEY (IdHabitacion) REFERENCES HABITACIONES(Id)
);
GO
CREATE TABLE dbo.AspNetRoles
(
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    Name NVARCHAR(256) NULL,
    NormalizedName NVARCHAR(256) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL
);
GO
CREATE UNIQUE INDEX RoleNameIndex ON dbo.AspNetRoles (NormalizedName) WHERE NormalizedName IS NOT NULL;
GO
CREATE TABLE dbo.AspNetUsers
(
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    UserName NVARCHAR(256) NULL,
    NormalizedUserName NVARCHAR(256) NULL,
    Email NVARCHAR(256) NULL,
    NormalizedEmail NVARCHAR(256) NULL,
    EmailConfirmed BIT NOT NULL,
    PasswordHash NVARCHAR(MAX) NULL,
    SecurityStamp NVARCHAR(MAX) NULL,
    ConcurrencyStamp NVARCHAR(MAX) NULL,
    PhoneNumber NVARCHAR(MAX) NULL,
    PhoneNumberConfirmed BIT NOT NULL,
    TwoFactorEnabled BIT NOT NULL,
    LockoutEnd DATETIMEOFFSET NULL,
    LockoutEnabled BIT NOT NULL,
    AccessFailedCount INT NOT NULL
);
GO
CREATE UNIQUE INDEX UserNameIndex ON dbo.AspNetUsers (NormalizedUserName) WHERE NormalizedUserName IS NOT NULL;
CREATE INDEX EmailIndex ON dbo.AspNetUsers (NormalizedEmail);
GO
CREATE TABLE dbo.AspNetRoleClaims
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    RoleId NVARCHAR(450) NOT NULL,
    ClaimType NVARCHAR(MAX) NULL,
    ClaimValue NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AspNetRoleClaims_AspNetRoles_RoleId FOREIGN KEY (RoleId) REFERENCES dbo.AspNetRoles (Id) ON DELETE CASCADE
);
GO
CREATE INDEX IX_AspNetRoleClaims_RoleId ON dbo.AspNetRoleClaims (RoleId);
GO
CREATE TABLE dbo.AspNetUserClaims
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    ClaimType NVARCHAR(MAX) NULL,
    ClaimValue NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AspNetUserClaims_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE
);
GO
CREATE INDEX IX_AspNetUserClaims_UserId ON dbo.AspNetUserClaims (UserId);
GO
CREATE TABLE dbo.AspNetUserLogins
(
    LoginProvider NVARCHAR(450) NOT NULL,
    ProviderKey NVARCHAR(450) NOT NULL,
    ProviderDisplayName NVARCHAR(MAX) NULL,
    UserId NVARCHAR(450) NOT NULL,
    CONSTRAINT PK_AspNetUserLogins PRIMARY KEY (LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE
);
GO
CREATE INDEX IX_AspNetUserLogins_UserId ON dbo.AspNetUserLogins (UserId);
GO
CREATE TABLE dbo.AspNetUserRoles
(
    UserId NVARCHAR(450) NOT NULL,
    RoleId NVARCHAR(450) NOT NULL,
    CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_AspNetRoles_RoleId FOREIGN KEY (RoleId) REFERENCES dbo.AspNetRoles (Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE
);
GO
CREATE INDEX IX_AspNetUserRoles_RoleId ON dbo.AspNetUserRoles (RoleId);
GO
CREATE TABLE dbo.AspNetUserTokens
(
    UserId NVARCHAR(450) NOT NULL,
    LoginProvider NVARCHAR(450) NOT NULL,
    Name NVARCHAR(450) NOT NULL,
    Value NVARCHAR(MAX) NULL,
    CONSTRAINT PK_AspNetUserTokens PRIMARY KEY (UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_AspNetUsers_UserId FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE
);
GO
INSERT INTO HABITACIONES (CodigoDeHabitacion, NombreDeHabitacion, CantidadDeHuespedesPermitidos, CantidadDeCamas, CantidadDeBanos, Ubicacion, EncargadoDeLimpieza, TipoDeHabitacion, CostoDeLimpieza, CostoDeReserva, FechaDeRegistro, Estado)
VALUES 
('JUN-001', 'Junior Vista Mar', 2, 1, 1, 'Piso 1', 'María Rojas', 1, 15000.00, 45000.00, GETDATE(), 1),
('SUP-002', 'Superior Balcón', 4, 2, 1, 'Piso 2', 'Carlos Pérez', 2, 20000.00, 65000.00, GETDATE(), 1),
('SUI-003', 'Suite Presidencial', 6, 3, 2, 'Penthouse', 'Ana Gómez', 3, 35000.00, 120000.00, GETDATE(), 1);
GO
INSERT INTO RESERVACIONES (NombreDeLaPersona, Identificacion, Telefono, Correo, FechaNacimiento, Direccion, MontoTotal, FechaInicioReserva, FechaFinReserva, FechaDeRegistro, IdHabitacion)
VALUES
('Juan Carlos Bodoque', '101110111', '88888888', 'juan@correo.com', '1990-05-15', 'San José, Centro', 105000.00, '2026-07-01', '2026-07-03', GETDATE(), 1);
GO
ALTER TABLE dbo.RESERVACIONES ADD UserId NVARCHAR(450) NULL;
GO
ALTER TABLE dbo.RESERVACIONES ADD CONSTRAINT FK_RESERVACIONES_AspNetUsers_UserId
    FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE SET NULL;
GO
ALTER TABLE dbo.RESERVACIONES ADD CantidadDePersonas INT NULL;
GO
UPDATE dbo.RESERVACIONES SET CantidadDePersonas = 1 WHERE CantidadDePersonas IS NULL;
GO
ALTER TABLE dbo.RESERVACIONES ALTER COLUMN CantidadDePersonas INT NOT NULL;
GO

