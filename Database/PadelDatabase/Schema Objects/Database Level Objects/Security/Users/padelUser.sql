-- =============================================
-- Script Template
-- =============================================
CREATE USER [padel] FOR LOGIN [padel];
ALTER ROLE [db_owner] ADD MEMBER [Padel];
ALTER ROLE [db_accessadmin] ADD MEMBER [Padel];
ALTER ROLE [db_datareader] ADD MEMBER [Padel];
ALTER ROLE [db_datawriter] ADD MEMBER [Padel];