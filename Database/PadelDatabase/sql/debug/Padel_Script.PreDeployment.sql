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

