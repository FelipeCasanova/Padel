-- =============================================
-- Script Template
-- =============================================
create table Jornadas (
        JornadaId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       GrupoId INT null,
       primary key (JornadaId)
    )