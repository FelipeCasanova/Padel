-- =============================================
-- Script Template
-- =============================================
alter table Equipos 
        add constraint Equipos_JugadorB_FK 
        foreign key (JugadorBId) 
        references Usuarios