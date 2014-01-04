-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint FKDAD4D23814375479 
        foreign key (EquipoAId) 
        references Equipos