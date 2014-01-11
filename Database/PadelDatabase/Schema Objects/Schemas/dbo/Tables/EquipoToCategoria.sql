-- =============================================
-- Script Template
-- =============================================
create table EquipoToCategoria (
       EquipoToCategoriaId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       EquipoId INT null,
       CategoriaId INT null,
       primary key (EquipoToCategoriaId)
    )