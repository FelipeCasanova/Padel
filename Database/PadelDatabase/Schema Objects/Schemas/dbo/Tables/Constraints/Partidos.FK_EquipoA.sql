-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint Partidos_EquipoA_FK 
        foreign key (EquipoAId) 
        references Equipos