-- =============================================
-- Script Template
-- =============================================
create table Categorias (
       CategoriaId INT IDENTITY NOT NULL,
	   Nombre NVARCHAR(255) null,
	   Estado NVARCHAR(255) null,
	   TipoEquipo NVARCHAR(255) null,
       FechaInicio DATETIME null,
       FechaFin DATETIME null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       TorneoId INT null,
       GanadorId INT null,
       primary key (CategoriaId)
    )