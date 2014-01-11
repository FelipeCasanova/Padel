-- =============================================
-- Script Template
-- =============================================
create table Equipos (
       EquipoId INT IDENTITY NOT NULL,
       Nombre NVARCHAR(255) null,
       TipoEquipo NVARCHAR(255) null,
       JugadorAVerificado BIT null,
       JugadorBVerificado BIT null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       JugadorAId INT null,
       JugadorBId INT null,
       primary key (EquipoId)
    )
