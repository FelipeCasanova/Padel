﻿-- =============================================
-- Script Template
-- =============================================
create table Operacions (
       OperacionId INT IDENTITY not null,
       Mensaje NVARCHAR(255) not null,
	   Accion NVARCHAR(255) not null,
       OperacionTipo NVARCHAR(255) not null,
       FechaCreacion DATETIME not null,
       FechaModificacion DATETIME not null,
	   UsuarioId INT not null,
	   EquipoId INT null,
	   TorneoId INT null,
	   PartidoId INT null,
	   ResultadoId INT null,
       primary key NONCLUSTERED (OperacionId)
    )
