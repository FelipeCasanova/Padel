-- =============================================
-- Script Template
-- =============================================
create table EquipoToCategoria (
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       EquipoId INT not null,
       CategoriaId INT not null
    )