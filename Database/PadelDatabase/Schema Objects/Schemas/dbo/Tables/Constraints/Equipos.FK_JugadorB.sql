-- =============================================
-- Script Template
-- =============================================
alter table Equipos 
        add constraint FKAA629BEFA82D91A 
        foreign key (JugadorBId) 
        references Usuarios