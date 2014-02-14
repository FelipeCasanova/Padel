-- =============================================
-- Script Template
-- =============================================
create table EquipoToCategorias (
	   EquipoToCategoriaId INT IDENTITY NOT NULL,
	   Estado NVARCHAR(255) not null,
	   DineroRealJugadorA DECIMAL(10,3) not null,
	   DineroFicticioJugadorA DECIMAL(10,3) not null,
	   DineroRealJugadorB DECIMAL(10,3) not null,
	   DineroFicticioJugadorB DECIMAL(10,3) not null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       EquipoId INT not null,
       CategoriaId INT not null,
	   primary key (EquipoToCategoriaId)
    )