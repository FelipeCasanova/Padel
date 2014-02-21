-- =============================================
-- Script Template
-- =============================================
alter table EquipoToCategorias
        add constraint EquipoToCategorias_Equipo_FK 
        foreign key (EquipoId) 
        references Equipos