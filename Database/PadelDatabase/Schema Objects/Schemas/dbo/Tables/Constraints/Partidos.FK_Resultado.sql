-- =============================================
-- Script Template
-- =============================================
alter table Partidos 
        add constraint Partidos_Resultado_FK 
        foreign key (ResultadoId) 
        references Resultados