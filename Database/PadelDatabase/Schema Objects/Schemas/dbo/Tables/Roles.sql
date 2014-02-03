-- =============================================
-- Script Template
-- =============================================
create table Roles (
       RoleId INT IDENTITY NOT NULL,
       Nombre NVARCHAR(255) null,
       primary key (RoleId)
    )