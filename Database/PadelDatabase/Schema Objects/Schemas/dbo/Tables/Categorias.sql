-- =============================================
-- Script Template
-- =============================================
create table Categorias (
        CategoriaId INT IDENTITY NOT NULL,
       FechaInicio DATETIME null,
       FechaFin DATETIME null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       TorneoId INT null,
       GanadorId INT null,
       primary key (CategoriaId)
    )