-- =============================================
-- Script Template
-- =============================================
create table Operacions (
       OperacionId INT IDENTITY NOT NULL,
       Mensaje NVARCHAR(255) null,
	   Accion NVARCHAR(255) not null,
       OperacionTipo NVARCHAR(255) not null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
	   UsuarioId INT null,
       primary key (OperacionId)
    )
