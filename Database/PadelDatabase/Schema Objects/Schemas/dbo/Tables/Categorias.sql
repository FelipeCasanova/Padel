-- =============================================
-- Script Template
-- =============================================
create table Categorias (
       CategoriaId INT IDENTITY NOT NULL,
	   Nombre NVARCHAR(255) null,
	   NivelMinExp INT not null,
	   NivelMin as (NivelMinExp / 100),
	   NivelMaxExp INT not null,
	   NivelMax as (NivelMaxExp / 100),
	   Estado NVARCHAR(255) not null,
	   TipoEquipo NVARCHAR(255) not null,
	   Precio DECIMAL(10,3) not null,
       FechaInicio DATETIME not null,
       FechaFin DATETIME null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       TorneoId INT null,
       GanadorId INT null,
       primary key (CategoriaId)
    )