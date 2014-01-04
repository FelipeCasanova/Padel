-- =============================================
-- Script Template
-- =============================================
alter table RoleToUsuario 
        add constraint FK670ADBED108215A5 
        foreign key (UsuarioId) 
        references Usuarios