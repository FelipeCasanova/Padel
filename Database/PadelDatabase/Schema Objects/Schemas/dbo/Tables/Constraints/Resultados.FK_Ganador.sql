-- =============================================
-- Script Template
-- =============================================
alter table Resultados 
        add constraint Resultados_Ganador_FK 
        foreign key (GanadorId) 
        references Equipos