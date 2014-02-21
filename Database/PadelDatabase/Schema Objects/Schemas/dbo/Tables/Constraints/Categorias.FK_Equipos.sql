-- =============================================
-- Script Template
-- =============================================
alter table Categorias 
        add constraint Categorias_Ganador_FK 
        foreign key (GanadorId) 
        references Equipos