-- =============================================
-- Script Template
-- =============================================
create table Partidos (
       PartidoId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       JornadaId INT null,
       EquipoAId INT null,
       EquipoBId INT null,
	   ResultadoId INT null,
       primary key (PartidoId)
    )