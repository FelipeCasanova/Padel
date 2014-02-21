-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint Partidos_Jornada_FK 
        foreign key (JornadaId) 
        references Jornadas