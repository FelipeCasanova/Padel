-- =============================================
-- Script Template
-- =============================================
alter table Jornadas 
        add constraint Jornadas_Grupo_FK 
        foreign key (GrupoId) 
        references Grupos