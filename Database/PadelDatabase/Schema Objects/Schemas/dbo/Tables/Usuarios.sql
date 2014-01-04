-- =============================================
-- Script Template
-- =============================================
create table Usuarios (
       UsuarioId INT IDENTITY NOT NULL,
       Nombre NVARCHAR(255) null,
       Sexo NVARCHAR(255) null,
       TelefonoMovil INT null,
       Email NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       primary key (UsuarioId)
    )