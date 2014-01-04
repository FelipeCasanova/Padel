/*
Deployment script for Padel
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Padel"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL)
	BEGIN
		DECLARE @rc      int,                       -- return code
				@fn      nvarchar(4000),            -- file name to back up to
				@dir     nvarchar(4000)             -- backup directory

		EXEC @rc = [master].[dbo].[xp_instance_regread] N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'BackupDirectory', @dir output, 'no_output'

		IF (@dir IS NULL)
		BEGIN 
			EXEC @rc = [master].[dbo].[xp_instance_regread] N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultData', @dir output, 'no_output'
		END

		IF (@dir IS NULL)
		BEGIN
			EXEC @rc = [master].[dbo].[xp_instance_regread] N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\Setup', N'SQLDataRoot', @dir output, 'no_output'
			SELECT @dir = @dir + N'\Backup'
		END

		SELECT  @fn = @dir + N'\' + N'$(DatabaseName)' + N'-' + 
				CONVERT(nchar(8), GETDATE(), 112) + N'-' + 
				RIGHT(N'0' + RTRIM(CONVERT(nchar(2), DATEPART(hh, GETDATE()))), 2) + 
				RIGHT(N'0' + RTRIM(CONVERT(nchar(2), DATEPART(mi, getdate()))), 2) + 
				RIGHT(N'0' + RTRIM(CONVERT(nchar(2), DATEPART(ss, getdate()))), 2) + 
				N'.bak' 
				BACKUP DATABASE [$(DatabaseName)] TO DISK = @fn
	END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target FELIPE-PC\SQLEXPRESS. The database for which this script was built, Padel, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'FELIPE-PC\SQLEXPRESS')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'FELIPE-PC\SQLEXPRESS',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]
GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9AD976725A0103E1]') AND parent_object_id = OBJECT_ID('Categorias'))
alter table Categorias  drop constraint FK9AD976725A0103E1


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9AD97672CAD1EE9A]') AND parent_object_id = OBJECT_ID('Categorias'))
alter table Categorias  drop constraint FK9AD97672CAD1EE9A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAA629BEFACB3D91A]') AND parent_object_id = OBJECT_ID('Equipos'))
alter table Equipos  drop constraint FKAA629BEFACB3D91A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAA629BEFA82D91A]') AND parent_object_id = OBJECT_ID('Equipos'))
alter table Equipos  drop constraint FKAA629BEFA82D91A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5F6C6AB149E41EB]') AND parent_object_id = OBJECT_ID('EquipoToCategoria'))
alter table EquipoToCategoria  drop constraint FK5F6C6AB149E41EB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5F6C6AB363F8A7F]') AND parent_object_id = OBJECT_ID('EquipoToCategoria'))
alter table EquipoToCategoria  drop constraint FK5F6C6AB363F8A7F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK19033393363F8A7F]') AND parent_object_id = OBJECT_ID('Grupos'))
alter table Grupos  drop constraint FK19033393363F8A7F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3A8C15D498DABC1A]') AND parent_object_id = OBJECT_ID('Jornadas'))
alter table Jornadas  drop constraint FK3A8C15D498DABC1A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDAD4D238DC753F7D]') AND parent_object_id = OBJECT_ID('Partidos'))
alter table Partidos  drop constraint FKDAD4D238DC753F7D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDAD4D23814375479]') AND parent_object_id = OBJECT_ID('Partidos'))
alter table Partidos  drop constraint FKDAD4D23814375479


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDAD4D238B6DC29E2]') AND parent_object_id = OBJECT_ID('Partidos'))
alter table Partidos  drop constraint FKDAD4D238B6DC29E2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDAD4D238CAD1EE9A]') AND parent_object_id = OBJECT_ID('Partidos'))
alter table Partidos  drop constraint FKDAD4D238CAD1EE9A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK670ADBED3E3915FC]') AND parent_object_id = OBJECT_ID('RoleToUsuario'))
alter table RoleToUsuario  drop constraint FK670ADBED3E3915FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK670ADBED108215A5]') AND parent_object_id = OBJECT_ID('RoleToUsuario'))
alter table RoleToUsuario  drop constraint FK670ADBED108215A5


    if exists (select * from dbo.sysobjects where id = object_id(N'Roles') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Roles

    if exists (select * from dbo.sysobjects where id = object_id(N'Categorias') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Categorias

    if exists (select * from dbo.sysobjects where id = object_id(N'Equipos') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Equipos

    if exists (select * from dbo.sysobjects where id = object_id(N'EquipoToCategoria') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table EquipoToCategoria

    if exists (select * from dbo.sysobjects where id = object_id(N'Grupos') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Grupos

    if exists (select * from dbo.sysobjects where id = object_id(N'Jornadas') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Jornadas

    if exists (select * from dbo.sysobjects where id = object_id(N'Partidos') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Partidos

    if exists (select * from dbo.sysobjects where id = object_id(N'Torneos') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Torneos

    if exists (select * from dbo.sysobjects where id = object_id(N'Usuarios') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Usuarios

    if exists (select * from dbo.sysobjects where id = object_id(N'RoleToUsuario') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RoleToUsuario


GO
PRINT N'Starting rebuilding table [dbo].[Torneos]...';


GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

BEGIN TRANSACTION;

CREATE TABLE [dbo].[tmp_ms_xx_Torneos] (
    [TorneoId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (255) NULL,
    [Name2]             NVARCHAR (255) NULL,
    [FechaCreacion]     DATETIME       NULL,
    [FechaModificacion] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([TorneoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

IF EXISTS (SELECT TOP 1 1
           FROM   [dbo].[Torneos])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Torneos] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Torneos] ([TorneoId], [Name], [FechaCreacion], [FechaModificacion])
        SELECT   [TorneoId],
                 [Name],
                 [FechaCreacion],
                 [FechaModificacion]
        FROM     [dbo].[Torneos]
        ORDER BY [TorneoId] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Torneos] OFF;
    END

DROP TABLE [dbo].[Torneos];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Torneos]', N'Torneos';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating FK9AD976725A0103E1...';


GO
ALTER TABLE [dbo].[Categorias] WITH NOCHECK
    ADD CONSTRAINT [FK9AD976725A0103E1] FOREIGN KEY ([TorneoId]) REFERENCES [dbo].[Torneos] ([TorneoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK9AD97672CAD1EE9A...';


GO
ALTER TABLE [dbo].[Categorias] WITH NOCHECK
    ADD CONSTRAINT [FK9AD97672CAD1EE9A] FOREIGN KEY ([GanadorId]) REFERENCES [dbo].[Equipos] ([EquipoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FKAA629BEFA82D91A...';


GO
ALTER TABLE [dbo].[Equipos] WITH NOCHECK
    ADD CONSTRAINT [FKAA629BEFA82D91A] FOREIGN KEY ([JugadorBId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FKAA629BEFACB3D91A...';


GO
ALTER TABLE [dbo].[Equipos] WITH NOCHECK
    ADD CONSTRAINT [FKAA629BEFACB3D91A] FOREIGN KEY ([JugadorAId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Categorias] WITH CHECK CHECK CONSTRAINT [FK9AD976725A0103E1];

ALTER TABLE [dbo].[Categorias] WITH CHECK CHECK CONSTRAINT [FK9AD97672CAD1EE9A];

ALTER TABLE [dbo].[Equipos] WITH CHECK CHECK CONSTRAINT [FKAA629BEFA82D91A];

ALTER TABLE [dbo].[Equipos] WITH CHECK CHECK CONSTRAINT [FKAA629BEFACB3D91A];


GO
