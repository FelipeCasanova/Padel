﻿-- =============================================
-- Script Template
-- =============================================
alter table Notificacions 
        add constraint Notificacions_Usuario_FK 
        foreign key (UsuarioId) 
        references Usuarios