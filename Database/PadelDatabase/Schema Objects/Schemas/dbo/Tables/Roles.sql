-- =============================================
-- Script Template
-- =============================================
create table Roles (
       RoleId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       primary key (RoleId)
    )