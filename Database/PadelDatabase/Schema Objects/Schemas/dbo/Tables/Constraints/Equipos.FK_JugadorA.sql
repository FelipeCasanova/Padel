-- =============================================
-- Script Template
-- =============================================
alter table Equipos 
        add constraint Equipos_JugadorA_FK
        foreign key (JugadorAId) 
        references Usuarios