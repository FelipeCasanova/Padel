-- =============================================
-- Script Template
-- =============================================
create table EquipoToCategoria (
       EquipoToCategoriaId INT IDENTITY NOT NULL,
       JugadorA NVARCHAR(255) null,
       JugadorB NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       EquipoId INT null,
       CategoriaId INT null,
       primary key (EquipoToCategoriaId)
    )