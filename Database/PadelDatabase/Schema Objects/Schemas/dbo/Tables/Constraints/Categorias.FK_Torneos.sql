-- =============================================
-- Script Template
-- =============================================
alter table Categorias 
        add constraint Categorias_Torneo_FK 
        foreign key (TorneoId) 
        references Torneos
