﻿/*
Deployment script for Padel
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Padel"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

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
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [Padel], FILENAME = N'$(DefaultDataPath)Padel.mdf')
    LOG ON (NAME = [Padel_log], FILENAME = N'$(DefaultLogPath)Padel_log.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]
GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


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

GO
PRINT N'Creating [dbo].[Categorias]...';


GO
CREATE TABLE [dbo].[Categorias] (
    [CategoriaId]       INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (255)  NULL,
    [NivelMinExp]       INT             NOT NULL,
    [NivelMin]          AS              (NivelMinExp / 100),
    [NivelMaxExp]       INT             NOT NULL,
    [NivelMax]          AS              (NivelMaxExp / 100),
    [Estado]            NVARCHAR (255)  NOT NULL,
    [TipoEquipo]        NVARCHAR (255)  NOT NULL,
    [Precio]            DECIMAL (10, 3) NOT NULL,
    [FechaInicio]       DATETIME        NOT NULL,
    [FechaFin]          DATETIME        NULL,
    [FechaCreacion]     DATETIME        NULL,
    [FechaModificacion] DATETIME        NULL,
    [TorneoId]          INT             NULL,
    [GanadorId]         INT             NULL,
    PRIMARY KEY CLUSTERED ([CategoriaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Categorias].[Categorias_Ganador_IX]...';


GO
CREATE NONCLUSTERED INDEX [Categorias_Ganador_IX]
    ON [dbo].[Categorias]([GanadorId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Categorias].[Categorias_Torneo_IX]...';


GO
CREATE NONCLUSTERED INDEX [Categorias_Torneo_IX]
    ON [dbo].[Categorias]([TorneoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Equipos]...';


GO
CREATE TABLE [dbo].[Equipos] (
    [EquipoId]           INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]             NVARCHAR (255) NULL,
    [Estado]             NVARCHAR (255) NOT NULL,
    [TipoEquipo]         NVARCHAR (255) NULL,
    [JugadorAVerificado] BIT            NULL,
    [JugadorBVerificado] BIT            NULL,
    [FechaCreacion]      DATETIME       NULL,
    [FechaModificacion]  DATETIME       NULL,
    [JugadorAId]         INT            NULL,
    [JugadorBId]         INT            NULL,
    PRIMARY KEY CLUSTERED ([EquipoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Equipos].[Equipos_JugadorA_IX]...';


GO
CREATE NONCLUSTERED INDEX [Equipos_JugadorA_IX]
    ON [dbo].[Equipos]([JugadorAId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Equipos].[Equipos_JugadorB_IX]...';


GO
CREATE NONCLUSTERED INDEX [Equipos_JugadorB_IX]
    ON [dbo].[Equipos]([JugadorBId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[EquipoToCategorias]...';


GO
CREATE TABLE [dbo].[EquipoToCategorias] (
    [EquipoToCategoriaId]    INT             IDENTITY (1, 1) NOT NULL,
    [Estado]                 NVARCHAR (255)  NOT NULL,
    [DineroRealJugadorA]     DECIMAL (10, 3) NOT NULL,
    [DineroFicticioJugadorA] DECIMAL (10, 3) NOT NULL,
    [DineroRealJugadorB]     DECIMAL (10, 3) NOT NULL,
    [DineroFicticioJugadorB] DECIMAL (10, 3) NOT NULL,
    [FechaCreacion]          DATETIME        NULL,
    [FechaModificacion]      DATETIME        NULL,
    [EquipoId]               INT             NOT NULL,
    [CategoriaId]            INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([EquipoToCategoriaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[EquipoToCategorias].[EquipoToCategorias_Categoria_IX]...';


GO
CREATE NONCLUSTERED INDEX [EquipoToCategorias_Categoria_IX]
    ON [dbo].[EquipoToCategorias]([CategoriaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[EquipoToCategorias].[EquipoToCategorias_Equipo_IX]...';


GO
CREATE NONCLUSTERED INDEX [EquipoToCategorias_Equipo_IX]
    ON [dbo].[EquipoToCategorias]([EquipoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Grupos]...';


GO
CREATE TABLE [dbo].[Grupos] (
    [GrupoId]           INT      IDENTITY (1, 1) NOT NULL,
    [FechaCreacion]     DATETIME NULL,
    [FechaModificacion] DATETIME NULL,
    [CategoriaId]       INT      NULL,
    PRIMARY KEY CLUSTERED ([GrupoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Grupos].[Grupos_Categoria_IX]...';


GO
CREATE NONCLUSTERED INDEX [Grupos_Categoria_IX]
    ON [dbo].[Grupos]([CategoriaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Jornadas]...';


GO
CREATE TABLE [dbo].[Jornadas] (
    [JornadaId]         INT      IDENTITY (1, 1) NOT NULL,
    [FechaCreacion]     DATETIME NULL,
    [FechaModificacion] DATETIME NULL,
    [GrupoId]           INT      NULL,
    PRIMARY KEY CLUSTERED ([JornadaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Jornadas].[Jornadas_Grupo_IX]...';


GO
CREATE NONCLUSTERED INDEX [Jornadas_Grupo_IX]
    ON [dbo].[Jornadas]([GrupoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Notificacions]...';


GO
CREATE TABLE [dbo].[Notificacions] (
    [NotificacionId]    INT            IDENTITY (1, 1) NOT NULL,
    [Mensaje]           NVARCHAR (255) NOT NULL,
    [Accion]            NVARCHAR (255) NOT NULL,
    [NotificacionTipo]  NVARCHAR (255) NOT NULL,
    [FechaCreacion]     DATETIME       NOT NULL,
    [FechaModificacion] DATETIME       NOT NULL,
    [UsuarioId]         INT            NOT NULL,
    [EquipoId]          INT            NULL,
    [TorneoId]          INT            NULL,
    [PartidoId]         INT            NULL,
    [ResultadoId]       INT            NULL,
    [CantidadPuntos]    INT            NULL,
    PRIMARY KEY NONCLUSTERED ([NotificacionId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Notificacions].[Notificacions_Cluster_IX]...';


GO
CREATE UNIQUE CLUSTERED INDEX [Notificacions_Cluster_IX]
    ON [dbo].[Notificacions]([UsuarioId] ASC, [FechaCreacion] ASC, [NotificacionTipo] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Operacions]...';


GO
CREATE TABLE [dbo].[Operacions] (
    [OperacionId]       INT            IDENTITY (1, 1) NOT NULL,
    [Mensaje]           NVARCHAR (255) NOT NULL,
    [Accion]            NVARCHAR (255) NOT NULL,
    [OperacionTipo]     NVARCHAR (255) NOT NULL,
    [FechaCreacion]     DATETIME       NOT NULL,
    [FechaModificacion] DATETIME       NOT NULL,
    [UsuarioId]         INT            NOT NULL,
    [EquipoId]          INT            NULL,
    [TorneoId]          INT            NULL,
    [PartidoId]         INT            NULL,
    [ResultadoId]       INT            NULL,
    PRIMARY KEY NONCLUSTERED ([OperacionId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Operacions].[Operacions_Cluster_IX]...';


GO
CREATE UNIQUE CLUSTERED INDEX [Operacions_Cluster_IX]
    ON [dbo].[Operacions]([UsuarioId] ASC, [FechaCreacion] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Partidos]...';


GO
CREATE TABLE [dbo].[Partidos] (
    [PartidoId]         INT      IDENTITY (1, 1) NOT NULL,
    [FechaCreacion]     DATETIME NULL,
    [FechaModificacion] DATETIME NULL,
    [JornadaId]         INT      NULL,
    [EquipoAId]         INT      NULL,
    [EquipoBId]         INT      NULL,
    [ResultadoId]       INT      NULL,
    PRIMARY KEY CLUSTERED ([PartidoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Partidos].[Partidos_EquipoA_IX]...';


GO
CREATE NONCLUSTERED INDEX [Partidos_EquipoA_IX]
    ON [dbo].[Partidos]([EquipoAId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Partidos].[Partidos_EquipoB_IX]...';


GO
CREATE NONCLUSTERED INDEX [Partidos_EquipoB_IX]
    ON [dbo].[Partidos]([EquipoBId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Partidos].[Partidos_Jornada_IX]...';


GO
CREATE NONCLUSTERED INDEX [Partidos_Jornada_IX]
    ON [dbo].[Partidos]([JornadaId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Partidos].[Partidos_Resultado_IX]...';


GO
CREATE NONCLUSTERED INDEX [Partidos_Resultado_IX]
    ON [dbo].[Partidos]([ResultadoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Resultados]...';


GO
CREATE TABLE [dbo].[Resultados] (
    [ResultadoId]       INT      IDENTITY (1, 1) NOT NULL,
    [Set1A]             INT      NULL,
    [Set2A]             INT      NULL,
    [Set3A]             INT      NULL,
    [Set1B]             INT      NULL,
    [Set2B]             INT      NULL,
    [Set3B]             INT      NULL,
    [FechaCreacion]     DATETIME NULL,
    [FechaModificacion] DATETIME NULL,
    [GanadorId]         INT      NULL,
    PRIMARY KEY CLUSTERED ([ResultadoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Resultados].[Resultados_Ganador_IX]...';


GO
CREATE NONCLUSTERED INDEX [Resultados_Ganador_IX]
    ON [dbo].[Resultados]([GanadorId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [RoleId] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[RoleToUsuario]...';


GO
CREATE TABLE [dbo].[RoleToUsuario] (
    [UsuarioId] INT NOT NULL,
    [RoleId]    INT NOT NULL
);


GO
PRINT N'Creating [dbo].[Torneos]...';


GO
CREATE TABLE [dbo].[Torneos] (
    [TorneoId]          INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (255) NULL,
    [Tipo]              NVARCHAR (255) NULL,
    [FechaCreacion]     DATETIME       NULL,
    [FechaModificacion] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([TorneoId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Usuarios]...';


GO
CREATE TABLE [dbo].[Usuarios] (
    [UsuarioId]             INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]                NVARCHAR (255)  NULL,
    [Sexo]                  NVARCHAR (255)  NULL,
    [TelefonoMovil]         INT             NOT NULL,
    [Email]                 NVARCHAR (255)  NOT NULL,
    [Password]              NVARCHAR (255)  NULL,
    [PuntosExperiencia]     INT             NOT NULL,
    [Nivel]                 AS              (PuntosExperiencia / 100),
    [AplicacionExperiencia] INT             NOT NULL,
    [DineroFicticio]        DECIMAL (10, 3) NOT NULL,
    [Ip]                    NVARCHAR (20)   NULL,
    [FechaCreacion]         DATETIME        NULL,
    [FechaModificacion]     DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([UsuarioId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[Usuarios].[Usuarios_Email_IX]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [Usuarios_Email_IX]
    ON [dbo].[Usuarios]([Email] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Usuarios].[Usuarios_TelefonoMovil_IX]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [Usuarios_TelefonoMovil_IX]
    ON [dbo].[Usuarios]([TelefonoMovil] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating Categorias_Ganador_FK...';


GO
ALTER TABLE [dbo].[Categorias] WITH NOCHECK
    ADD CONSTRAINT [Categorias_Ganador_FK] FOREIGN KEY ([GanadorId]) REFERENCES [dbo].[Equipos] ([EquipoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Categorias_Torneo_FK...';


GO
ALTER TABLE [dbo].[Categorias] WITH NOCHECK
    ADD CONSTRAINT [Categorias_Torneo_FK] FOREIGN KEY ([TorneoId]) REFERENCES [dbo].[Torneos] ([TorneoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Equipos_JugadorA_FK...';


GO
ALTER TABLE [dbo].[Equipos] WITH NOCHECK
    ADD CONSTRAINT [Equipos_JugadorA_FK] FOREIGN KEY ([JugadorAId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Equipos_JugadorB_FK...';


GO
ALTER TABLE [dbo].[Equipos] WITH NOCHECK
    ADD CONSTRAINT [Equipos_JugadorB_FK] FOREIGN KEY ([JugadorBId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating EquipoToCategorias_Categoria_FK...';


GO
ALTER TABLE [dbo].[EquipoToCategorias] WITH NOCHECK
    ADD CONSTRAINT [EquipoToCategorias_Categoria_FK] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categorias] ([CategoriaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating EquipoToCategorias_Equipo_FK...';


GO
ALTER TABLE [dbo].[EquipoToCategorias] WITH NOCHECK
    ADD CONSTRAINT [EquipoToCategorias_Equipo_FK] FOREIGN KEY ([EquipoId]) REFERENCES [dbo].[Equipos] ([EquipoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Grupos_Categoria_FK...';


GO
ALTER TABLE [dbo].[Grupos] WITH NOCHECK
    ADD CONSTRAINT [Grupos_Categoria_FK] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categorias] ([CategoriaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Jornadas_Grupo_FK...';


GO
ALTER TABLE [dbo].[Jornadas] WITH NOCHECK
    ADD CONSTRAINT [Jornadas_Grupo_FK] FOREIGN KEY ([GrupoId]) REFERENCES [dbo].[Grupos] ([GrupoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Notificacions_Usuario_FK...';


GO
ALTER TABLE [dbo].[Notificacions] WITH NOCHECK
    ADD CONSTRAINT [Notificacions_Usuario_FK] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Operacions_Usuario_FK...';


GO
ALTER TABLE [dbo].[Operacions] WITH NOCHECK
    ADD CONSTRAINT [Operacions_Usuario_FK] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Partidos_EquipoA_FK...';


GO
ALTER TABLE [dbo].[Partidos] WITH NOCHECK
    ADD CONSTRAINT [Partidos_EquipoA_FK] FOREIGN KEY ([EquipoAId]) REFERENCES [dbo].[Equipos] ([EquipoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Partidos_EquipoB_FK...';


GO
ALTER TABLE [dbo].[Partidos] WITH NOCHECK
    ADD CONSTRAINT [Partidos_EquipoB_FK] FOREIGN KEY ([EquipoBId]) REFERENCES [dbo].[Equipos] ([EquipoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Partidos_Jornada_FK...';


GO
ALTER TABLE [dbo].[Partidos] WITH NOCHECK
    ADD CONSTRAINT [Partidos_Jornada_FK] FOREIGN KEY ([JornadaId]) REFERENCES [dbo].[Jornadas] ([JornadaId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Partidos_Resultado_FK...';


GO
ALTER TABLE [dbo].[Partidos] WITH NOCHECK
    ADD CONSTRAINT [Partidos_Resultado_FK] FOREIGN KEY ([ResultadoId]) REFERENCES [dbo].[Resultados] ([ResultadoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating Resultados_Ganador_FK...';


GO
ALTER TABLE [dbo].[Resultados] WITH NOCHECK
    ADD CONSTRAINT [Resultados_Ganador_FK] FOREIGN KEY ([GanadorId]) REFERENCES [dbo].[Equipos] ([EquipoId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK670ADBED108215A5...';


GO
ALTER TABLE [dbo].[RoleToUsuario] WITH NOCHECK
    ADD CONSTRAINT [FK670ADBED108215A5] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([UsuarioId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK670ADBED3E3915FC...';


GO
ALTER TABLE [dbo].[RoleToUsuario] WITH NOCHECK
    ADD CONSTRAINT [FK670ADBED3E3915FC] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

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
ALTER TABLE [dbo].[Categorias] WITH CHECK CHECK CONSTRAINT [Categorias_Ganador_FK];

ALTER TABLE [dbo].[Categorias] WITH CHECK CHECK CONSTRAINT [Categorias_Torneo_FK];

ALTER TABLE [dbo].[Equipos] WITH CHECK CHECK CONSTRAINT [Equipos_JugadorA_FK];

ALTER TABLE [dbo].[Equipos] WITH CHECK CHECK CONSTRAINT [Equipos_JugadorB_FK];

ALTER TABLE [dbo].[EquipoToCategorias] WITH CHECK CHECK CONSTRAINT [EquipoToCategorias_Categoria_FK];

ALTER TABLE [dbo].[EquipoToCategorias] WITH CHECK CHECK CONSTRAINT [EquipoToCategorias_Equipo_FK];

ALTER TABLE [dbo].[Grupos] WITH CHECK CHECK CONSTRAINT [Grupos_Categoria_FK];

ALTER TABLE [dbo].[Jornadas] WITH CHECK CHECK CONSTRAINT [Jornadas_Grupo_FK];

ALTER TABLE [dbo].[Notificacions] WITH CHECK CHECK CONSTRAINT [Notificacions_Usuario_FK];

ALTER TABLE [dbo].[Operacions] WITH CHECK CHECK CONSTRAINT [Operacions_Usuario_FK];

ALTER TABLE [dbo].[Partidos] WITH CHECK CHECK CONSTRAINT [Partidos_EquipoA_FK];

ALTER TABLE [dbo].[Partidos] WITH CHECK CHECK CONSTRAINT [Partidos_EquipoB_FK];

ALTER TABLE [dbo].[Partidos] WITH CHECK CHECK CONSTRAINT [Partidos_Jornada_FK];

ALTER TABLE [dbo].[Partidos] WITH CHECK CHECK CONSTRAINT [Partidos_Resultado_FK];

ALTER TABLE [dbo].[Resultados] WITH CHECK CHECK CONSTRAINT [Resultados_Ganador_FK];

ALTER TABLE [dbo].[RoleToUsuario] WITH CHECK CHECK CONSTRAINT [FK670ADBED108215A5];

ALTER TABLE [dbo].[RoleToUsuario] WITH CHECK CHECK CONSTRAINT [FK670ADBED3E3915FC];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
