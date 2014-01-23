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
	   PuntosExperiencia INT not null,
	   Nivel as (PuntosExperiencia / 100),
	   AplicacionExperiencia INT not null,
	   DineroFicticio DECIMAL(10,3) not null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       primary key (UsuarioId)
    )

