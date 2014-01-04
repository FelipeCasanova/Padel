-- =============================================
-- Script Template
-- =============================================
alter table Categorias 
        add constraint FK9AD976725A0103E1 
        foreign key (TorneoId) 
        references Torneos
