-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint FKDAD4D238B6DC29E2 
        foreign key (EquipoBId) 
        references Equipos