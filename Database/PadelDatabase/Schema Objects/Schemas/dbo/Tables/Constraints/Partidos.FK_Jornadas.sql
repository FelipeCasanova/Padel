-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint FKDAD4D238DC753F7D 
        foreign key (JornadaId) 
        references Jornadas