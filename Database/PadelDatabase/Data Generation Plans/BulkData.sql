﻿-- =============================================
-- Script Template
-- =============================================
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[Roles] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Roles]([RoleId], [Name])
SELECT 1, N'Administrador' UNION ALL
SELECT 2, N'Jugador'
COMMIT;
RAISERROR (N'[dbo].[Roles]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Roles] OFF;


SET IDENTITY_INSERT [dbo].[Usuarios] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Usuarios]([UsuarioId], [Nombre], [Sexo], [TelefonoMovil], [Email], [Password], [FechaCreacion], [FechaModificacion])
SELECT 1, N'Felipe Casanova', N'Hombre', 636547424, N'felipe.casanova.ua@gmail.com', N'7e04da88cbb8cc933c7b89fbfe121cca', '20131217 17:23:50.000', '20131217 17:23:50.000' UNION ALL
SELECT 2, N'Alfonso Juan Grueso', N'Hombre', 635560144, N'alfonsoalicante@gmail.com', N'3f128e570b3a9009d7b52a0523af43dd', '20131217 17:31:18.000', '20131217 17:31:18.000' UNION ALL
SELECT 3, N'Test1', N'Hombre', 111111111, NULL, N'5a105e8b9d40e1329780d62ea2265d8a', '20131217 17:36:14.000', '20131217 17:36:14.000' UNION ALL
SELECT 4, N'Test2', N'Mujer', 222222222, NULL, N'ad0234829205b9033196ba818f7a872b', '20131217 17:42:25.000', '20131217 17:42:25.000' UNION ALL
SELECT 5, N'Test3', N'Hombre', 333333333, NULL, N'8ad8757baa8564dc136c1e07507f4a98', '20131217 17:44:06.000', '20131217 17:44:06.000' UNION ALL
SELECT 6, N'Test4', N'Mujer', 444444444, NULL, N'86985e105f79b95d6bc918fb45ec7727', '20131217 17:45:02.000', '20131217 17:45:02.000' UNION ALL
SELECT 7, N'Test5', N'Hombre', 555555555, NULL, N'e3d704f3542b44a621ebed70dc0efe13', '20131217 17:49:07.000', '20131217 17:49:07.000' UNION ALL
SELECT 8, N'Test6', N'Mujer', 666666666, NULL, N'4cfad7076129962ee70c36839a1e3e15', '20131217 17:57:46.000', '20131217 17:57:46.000' UNION ALL
SELECT 9, N'Test7', N'Hombre', 777777777, NULL, N'b04083e53e242626595e2b8ea327e525', '20131217 17:58:29.000', '20131217 17:58:29.000' UNION ALL
SELECT 10, N'Test8', N'Mujer', 888888888, NULL, N'5e40d09fa0529781afd1254a42913847', '20131217 18:03:56.000', '20131217 18:03:56.000' UNION ALL
SELECT 11, N'Test9', N'Hombre', 999999999, NULL, N'739969b53246b2c727850dbb3490ede6', '20131217 18:04:25.000', '20131217 18:04:25.000'
COMMIT;
RAISERROR (N'[dbo].[Usuarios]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Usuarios] OFF;


BEGIN TRANSACTION;
INSERT INTO [dbo].[RoleToUsuario]([UsuarioId], [RoleId])
SELECT 1, 1 UNION ALL
SELECT 2, 1 UNION ALL
SELECT 3, 2 UNION ALL
SELECT 4, 2 UNION ALL
SELECT 5, 2 UNION ALL
SELECT 6, 2 UNION ALL
SELECT 7, 2 UNION ALL
SELECT 8, 2 UNION ALL
SELECT 9, 2 UNION ALL
SELECT 10, 2 UNION ALL
SELECT 11, 2
COMMIT;
RAISERROR (N'[dbo].[RoleToUsuario]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Torneos] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Torneos]([TorneoId], [Nombre], [Tipo], [FechaCreacion], [FechaModificacion])
SELECT 1, N'Alicante', N'Estandar', '20140110 17:23:50.000', '20140110 17:23:50.000' UNION ALL
SELECT 2, N'Mini Alicante', N'Mini', '20140110 17:23:50.000', '20140110 17:23:50.000' UNION ALL
SELECT 3, N'Urba Juanjo', N'Privado', '20140110 17:23:50.000', '20140110 17:23:50.000'
COMMIT;
RAISERROR (N'[dbo].[Torneos]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Torneos] OFF;


SET IDENTITY_INSERT [dbo].[Categorias] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Categorias]([CategoriaId], [Nombre], [Estado], [TipoEquipo], [FechaInicio], [FechaFin], [FechaCreacion], [FechaModificacion], [TorneoId], [GanadorId])
SELECT 1, N'4A', N'Pendiente', N'Hombre', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 2, N'4B', N'Pendiente', N'Hombre', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 3, N'4C', N'Pendiente', N'Hombre', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 4, N'4A', N'Pendiente', N'Mujer', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 5, N'3A', N'Pendiente', N'Mixto', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 6, N'3B', N'Pendiente', N'Mixto', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 7, N'3C', N'Pendiente', N'Mixto', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 8, N'4A', N'Pendiente', N'Mixto', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 2, NULL UNION ALL
SELECT 9, N'Super', N'Pendiente', N'Hombre', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 3, NULL UNION ALL
SELECT 10, N'Super', N'Pendiente', N'Mujer', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 3, NULL UNION ALL
SELECT 11, N'Super', N'Pendiente', N'Mixto', '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 3, NULL
COMMIT;
RAISERROR (N'[dbo].[Categorias]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Categorias] OFF;
