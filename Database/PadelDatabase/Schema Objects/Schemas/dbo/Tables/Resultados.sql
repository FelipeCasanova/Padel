-- =============================================
-- Script Template
-- =============================================
create table Resultados (
       ResultadoId INT IDENTITY NOT NULL,
       Set1A INT null,
       Set2A INT null,
       Set3A INT null,
       Set1B INT null,
       Set2B INT null,
       Set3B INT null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       GanadorId INT null,
       primary key (ResultadoId)
    )