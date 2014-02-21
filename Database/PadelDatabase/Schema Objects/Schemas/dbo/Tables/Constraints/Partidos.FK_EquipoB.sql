-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint Partidos_EquipoB_FK 
        foreign key (EquipoBId) 
        references Equipos