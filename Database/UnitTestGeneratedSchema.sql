
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9AD976725A0103E1]') AND parent_object_id = OBJECT_ID('Categorias'))
alter table Categorias  drop constraint FK9AD976725A0103E1


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9AD97672CAD1EE9A]') AND parent_object_id = OBJECT_ID('Categorias'))
alter table Categorias  drop constraint FK9AD97672CAD1EE9A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAA629BEFACB3D91A]') AND parent_object_id = OBJECT_ID('Equipos'))
alter table Equipos  drop constraint FKAA629BEFACB3D91A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAA629BEFA82D91A]') AND parent_object_id = OBJECT_ID('Equipos'))
alter table Equipos  drop constraint FKAA629BEFA82D91A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5F6C6AB149E41EB]') AND parent_object_id = OBJECT_ID('EquipoToCategoria'))
alter table EquiposToCategorias  drop constraint FK5F6C6AB149E41EB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5F6C6AB363F8A7F]') AND parent_object_id = OBJECT_ID('EquipoToCategoria'))
alter table EquiposToCategorias  drop constraint FK5F6C6AB363F8A7F


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

    create table Roles (
        RoleId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       primary key (RoleId)
    )

    create table Categorias (
        CategoriaId INT IDENTITY NOT NULL,
       FechaInicio DATETIME null,
       FechaFin DATETIME null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       TorneoId INT null,
       GanadorId INT null,
       primary key (CategoriaId)
    )

    create table Equipos (
        EquipoId INT IDENTITY NOT NULL,
       Nombre NVARCHAR(255) null,
       TipoEquipo NVARCHAR(255) null,
       JugadorAVerificado BIT null,
       JugadorBVerificado BIT null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       JugadorAId INT null,
       JugadorBId INT null,
       primary key (EquipoId)
    )

    create table EquiposToCategorias (
        EquiposToCategoriasId INT IDENTITY NOT NULL,
       JugadorA NVARCHAR(255) null,
       JugadorB NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       EquipoId INT null,
       CategoriaId INT null,
       primary key (EquiposToCategoriasId)
    )

    create table Grupos (
        GrupoId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       CategoriaId INT null,
       primary key (GrupoId)
    )

    create table Jornadas (
        JornadaId INT IDENTITY NOT NULL,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       GrupoId INT null,
       primary key (JornadaId)
    )

    create table Partidos (
        PartidoId INT IDENTITY NOT NULL,
       Set1A INT null,
       Set2A INT null,
       Set3A INT null,
       Set1B INT null,
       Set2B INT null,
       Set3B INT null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       JornadaId INT null,
       EquipoAId INT null,
       EquipoBId INT null,
       GanadorId INT null,
       primary key (PartidoId)
    )

    create table Torneos (
        TorneoId INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       primary key (TorneoId)
    )

    create table Usuarios (
        UsuarioId INT IDENTITY NOT NULL,
       Nombre NVARCHAR(255) null,
       Sexo NVARCHAR(255) null,
       TelefonoMovil INT null,
       Email NVARCHAR(255) null,
       Password NVARCHAR(255) null,
       FechaCreacion DATETIME null,
       FechaModificacion DATETIME null,
       primary key (UsuarioId)
    )

    create table RoleToUsuario (
        UsuarioId INT not null,
       RoleId INT not null
    )

    alter table Categorias 
        add constraint FK9AD976725A0103E1 
        foreign key (TorneoId) 
        references Torneos

    alter table Categorias 
        add constraint FK9AD97672CAD1EE9A 
        foreign key (GanadorId) 
        references Equipos

    alter table Equipos 
        add constraint FKAA629BEFACB3D91A 
        foreign key (JugadorAId) 
        references Usuarios

    alter table Equipos 
        add constraint FKAA629BEFA82D91A 
        foreign key (JugadorBId) 
        references Usuarios

    alter table EquipoToCategoria
        add constraint FK5F6C6AB149E41EB 
        foreign key (EquipoId) 
        references Equipos

    alter table EquipoToCategoria 
        add constraint FK5F6C6AB363F8A7F 
        foreign key (CategoriaId) 
        references Categorias

    alter table Grupos 
        add constraint FK19033393363F8A7F 
        foreign key (CategoriaId) 
        references Categorias

    alter table Jornadas 
        add constraint FK3A8C15D498DABC1A 
        foreign key (GrupoId) 
        references Grupos

    alter table Partidos 
        add constraint FKDAD4D238DC753F7D 
        foreign key (JornadaId) 
        references Jornadas

    alter table Partidos 
        add constraint FKDAD4D23814375479 
        foreign key (EquipoAId) 
        references Equipos

    alter table Partidos 
        add constraint FKDAD4D238B6DC29E2 
        foreign key (EquipoBId) 
        references Equipos

    alter table Partidos 
        add constraint FKDAD4D238CAD1EE9A 
        foreign key (GanadorId) 
        references Equipos

    alter table RoleToUsuario 
        add constraint FK670ADBED3E3915FC 
        foreign key (RoleId) 
        references Roles

    alter table RoleToUsuario 
        add constraint FK670ADBED108215A5 
        foreign key (UsuarioId) 
        references Usuarios
