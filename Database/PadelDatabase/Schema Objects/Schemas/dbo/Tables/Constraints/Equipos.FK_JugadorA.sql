-- =============================================
-- Script Template
-- =============================================
alter table Equipos 
        add constraint FKAA629BEFACB3D91A 
        foreign key (JugadorAId) 
        references Usuarios