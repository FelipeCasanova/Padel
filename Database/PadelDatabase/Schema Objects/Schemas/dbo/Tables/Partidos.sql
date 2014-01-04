-- =============================================
-- Script Template
-- =============================================
create table Partidos (
       PartidoId INT IDENTITY NOT NULL,
       Set1A INT null,
       Set2A INT null,
       Set3A INT null,
       Set1B INT null,
       Set2B INT null,
       Set3B INT null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       JornadaId INT null,
       EquipoAId INT null,
       EquipoBId INT null,
       GanadorId INT null,
       primary key (PartidoId)
    )