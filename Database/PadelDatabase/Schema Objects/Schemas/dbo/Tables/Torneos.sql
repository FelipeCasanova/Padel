-- =============================================
-- Script Template
-- =============================================
create table Torneos (
       TorneoId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
	   Name2 NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       primary key (TorneoId)
    )