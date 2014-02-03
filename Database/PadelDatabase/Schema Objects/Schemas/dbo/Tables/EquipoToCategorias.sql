-- =============================================
-- Script Template
-- =============================================
create table EquipoToCategorias (
	   EquipoToCategoriaId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       EquipoId INT not null,
       CategoriaId INT not null,
	   primary key (EquipoToCategoriaId)
    )