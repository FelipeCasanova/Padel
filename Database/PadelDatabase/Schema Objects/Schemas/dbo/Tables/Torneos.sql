-- =============================================
-- Script Template
-- =============================================
create table Torneos (
       TorneoId INT IDENTITY NOT NULL,
       Nombre NVARCHAR(255) null,
	   Tipo NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       primary key (TorneoId)
    )