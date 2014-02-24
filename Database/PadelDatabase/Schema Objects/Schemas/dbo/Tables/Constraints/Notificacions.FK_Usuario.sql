-- =============================================
-- Script Template
-- =============================================
alter table Operacions 
        add constraint Operacions_Usuario_FK 
        foreign key (UsuarioId) 
        references Usuarios