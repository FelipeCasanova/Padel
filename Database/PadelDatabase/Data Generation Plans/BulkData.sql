-- =============================================
-- Script Template
-- =============================================
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[Roles] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Roles]([RoleId], [Nombre])
SELECT 1, N'Administrador' UNION ALL
SELECT 2, N'Jugador'
COMMIT;
RAISERROR (N'[dbo].[Roles]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Roles] OFF;


SET IDENTITY_INSERT [dbo].[Usuarios] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Usuarios]([UsuarioId], [Nombre], [Sexo], [TelefonoMovil], [Email], [Password], [PuntosExperiencia], [AplicacionExperiencia], [DineroFicticio], [FechaCreacion], [FechaModificacion])
SELECT 1, N'Felipe Casanova', N'Hombre', 636547424, N'felipe.casanova.ua@gmail.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 10, 12, 67.000, '20131217 17:23:50.000', '20131217 17:23:50.000' UNION ALL
SELECT 2, N'Alfonso Juan Grueso', N'Hombre', 635560144, N'alfonsoalicante@gmail.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 10, 12, 65.000, '20131217 17:31:18.000', '20131217 17:31:18.000' UNION ALL
SELECT 3, N'Alberto Juan', N'Hombre', 646205244, N'albertojuan@gmail.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 0, 0, 0.000, '20140128 17:08:13.000', '20140128 17:08:13.000' UNION ALL
SELECT 4, N'Hombre1', N'Hombre', 111111111, N'hombre1@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 1, 0, 0.000, '20131217 17:36:14.000', '20131217 17:36:14.000' UNION ALL
SELECT 5, N'Mujer1', N'Mujer', 111111112, N'mujer1@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 2, 0, 0.000, '20131217 17:42:25.000', '20131217 17:42:25.000' UNION ALL
SELECT 6, N'Hombre2', N'Hombre', 222222221, N'hombre2@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 3, 0, 0.000, '20131217 17:44:06.000', '20131217 17:44:06.000' UNION ALL
SELECT 7, N'Mujer2', N'Mujer', 222222222, N'mujer2@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 4, 0, 0.000, '20131217 17:45:02.000', '20131217 17:45:02.000' UNION ALL
SELECT 8, N'Hombre3', N'Hombre', 333333331, N'hombre3@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 5, 0, 0.000, '20131217 17:49:07.000', '20131217 17:49:07.000' UNION ALL
SELECT 9, N'Mujer3', N'Mujer', 333333332, N'mujer3@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 6, 0, 0.000, '20131217 17:57:46.000', '20131217 17:57:46.000' UNION ALL
SELECT 10, N'Hombre4', N'Hombre', 444444441, N'hombre4@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 7, 0, 0.000, '20131217 17:58:29.000', '20131217 17:58:29.000' UNION ALL
SELECT 11, N'Mujer4', N'Mujer', 444444442, N'mujer4@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 8, 0, 0.000, '20131217 18:03:56.000', '20131217 18:03:56.000' UNION ALL
SELECT 12, N'Hombre5', N'Hombre', 555555551, N'hombre5@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 9, 0, 0.000, '20131217 18:04:25.000', '20131217 18:04:25.000' UNION ALL
SELECT 13, N'Mujer5', N'Mujer', 555555525, N'mujer5@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 9, 0, 0.000, '20131217 18:04:25.000', '20131217 18:04:25.000' UNION ALL
SELECT 14, N'Hombre uno', N'Hombre', 100000001, N'hombreuno@test.com', N'a8f8431fe5fec3c8ddcd3ab95d8051f9', 0, 0, 0.000, '20140219 14:32:29.000', '20140219 14:32:29.000'
COMMIT;
RAISERROR (N'[dbo].[Usuarios]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Usuarios] OFF;


BEGIN TRANSACTION;
INSERT INTO [dbo].[RoleToUsuario]([UsuarioId], [RoleId])
SELECT 1, 1 UNION ALL
SELECT 2, 1 UNION ALL
SELECT 3, 1 UNION ALL
SELECT 4, 2 UNION ALL
SELECT 5, 2 UNION ALL
SELECT 6, 2 UNION ALL
SELECT 7, 2 UNION ALL
SELECT 8, 2 UNION ALL
SELECT 9, 2 UNION ALL
SELECT 10, 2 UNION ALL
SELECT 11, 2 UNION ALL
SELECT 12, 2 UNION ALL
SELECT 13, 2 UNION ALL
SELECT 14, 2
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
INSERT INTO [dbo].[Categorias]([CategoriaId], [Nombre], [NivelMinExp], [NivelMaxExp], [Estado], [TipoEquipo], [Precio], [FechaInicio], [FechaFin], [FechaCreacion], [FechaModificacion], [TorneoId], [GanadorId])
SELECT 1, N'4A', 70, 99, N'Progreso', N'Hombre', 20, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 2, N'4B', 40, 69, N'Pendiente', N'Hombre', 20,'20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 3, N'4C', 0, 39, N'Pendiente', N'Hombre', 20,'20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 4, N'4A', 70, 99, N'Progreso', N'Mujer', 15, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 5, N'4A', 70, 99, N'Pendiente', N'Mixto', 10, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 6, N'3A', 130, 199, N'Pendiente', N'Mixto', 10, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 7, N'3B', 70, 129, N'Pendiente', N'Mixto', 10, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 1, NULL UNION ALL
SELECT 8, N'3C', 0, 69, N'Pendiente', N'Mixto', 10, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 2, NULL UNION ALL
SELECT 9, N'Super Juanjo', 0, 599, N'Pendiente', N'Hombre', 20, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 3, NULL UNION ALL
SELECT 10, N'Super Juanjo', 0, 599, N'Pendiente', N'Mujer', 15, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 3, NULL UNION ALL
SELECT 11, N'Super Juanjo', 0, 599, N'Pendiente', N'Mixto', 10, '20140410 17:23:50.000', '20140710 17:23:50.000', '20140110 17:23:50.000', '20140110 17:23:50.000', 3, NULL
COMMIT;
RAISERROR (N'[dbo].[Categorias]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Categorias] OFF;


SET IDENTITY_INSERT [dbo].[Operacions] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Operacions]([OperacionId], [Mensaje], [Accion], [OperacionTipo], [FechaCreacion], [FechaModificacion], [UsuarioId])
SELECT 1, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 1 UNION ALL
SELECT 2, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 2 UNION ALL
SELECT 3, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 3 UNION ALL
SELECT 4, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 4 UNION ALL
SELECT 5, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 5 UNION ALL
SELECT 6, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 6 UNION ALL
SELECT 7, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 7 UNION ALL
SELECT 8, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 8 UNION ALL
SELECT 9, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 9 UNION ALL
SELECT 10, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 10 UNION ALL
SELECT 11, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 11 UNION ALL
SELECT 12, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 12 UNION ALL
SELECT 13, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 13 UNION ALL
SELECT 14, N'Garcias por su registro.', N'Registrar', N'UsuarioOperacion', '20140218 15:44:54.000', '20140218 15:44:54.000', 14 UNION ALL
SELECT 15, N'Has entrado por última vez el miércoles, 19 de febrero de 2014', N'Entrar', N'UsuarioOperacion', '20140219 14:34:11.000', '20140219 14:34:11.000', 1
COMMIT;
RAISERROR (N'[dbo].[Operacions]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Operacions] OFF;


