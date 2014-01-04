-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint FKDAD4D238CAD1EE9A 
        foreign key (GanadorId) 
        references Equipos