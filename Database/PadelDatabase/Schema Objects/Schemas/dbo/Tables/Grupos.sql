-- =============================================
-- Script Template
-- =============================================
create table Grupos (
       GrupoId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       CategoriaId INT null,
       primary key (GrupoId)
    )