
    PRAGMA foreign_keys = OFF

    drop table if exists Roles

    drop table if exists Categorias

    drop table if exists Equipos

    drop table if exists EquipoToCategorias

    drop table if exists Grupos

    drop table if exists Jornadas

    drop table if exists Partidos

    drop table if exists Torneos

    drop table if exists Usuarios

    drop table if exists RoleToUsuario

    PRAGMA foreign_keys = ON

    create table Roles (
        RoleId  integer primary key autoincrement,
       Name TEXT
    )

    create table Categorias (
        CategoriaId  integer primary key autoincrement,
       FechaInicio DATETIME,
       FechaFin DATETIME,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME,
       TorneoId INT,
       GanadorId INT,
       constraint FK9AD976725A0103E1 foreign key (TorneoId) references Torneos,
       constraint FK9AD97672CAD1EE9A foreign key (GanadorId) references Equipos
    )

    create table Equipos (
        EquipoId  integer primary key autoincrement,
       Nombre TEXT,
       TipoEquipo TEXT,
       JugadorAVerificado BOOL,
       JugadorBVerificado BOOL,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME,
       JugadorAId INT,
       JugadorBId INT,
       constraint FKAA629BEFACB3D91A foreign key (JugadorAId) references Usuarios,
       constraint FKAA629BEFA82D91A foreign key (JugadorBId) references Usuarios
    )

    create table EquipoToCategorias (
        EquipoToCategoriaId  integer primary key autoincrement,
       JugadorA TEXT,
       JugadorB TEXT,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME,
       EquipoId INT,
       CategoriaId INT,
       constraint FKB5A322EE149E41EB foreign key (EquipoId) references Equipos,
       constraint FKB5A322EE363F8A7F foreign key (CategoriaId) references Categorias
    )

    create table Grupos (
        GrupoId  integer primary key autoincrement,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME,
       CategoriaId INT,
       constraint FK19033393363F8A7F foreign key (CategoriaId) references Categorias
    )

    create table Jornadas (
        JornadaId  integer primary key autoincrement,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME,
       GrupoId INT,
       constraint FK3A8C15D498DABC1A foreign key (GrupoId) references Grupos
    )

    create table Partidos (
        PartidoId  integer primary key autoincrement,
       Set1A INT,
       Set2A INT,
       Set3A INT,
       Set1B INT,
       Set2B INT,
       Set3B INT,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME,
       JornadaId INT,
       EquipoAId INT,
       EquipoBId INT,
       GanadorId INT,
       constraint FKDAD4D238DC753F7D foreign key (JornadaId) references Jornadas,
       constraint FKDAD4D23814375479 foreign key (EquipoAId) references Equipos,
       constraint FKDAD4D238B6DC29E2 foreign key (EquipoBId) references Equipos,
       constraint FKDAD4D238CAD1EE9A foreign key (GanadorId) references Equipos
    )

    create table Torneos (
        TorneoId  integer primary key autoincrement,
       Name TEXT,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME
    )

    create table Usuarios (
        UsuarioId  integer primary key autoincrement,
       Nombre TEXT,
       Sexo TEXT,
       TelefonoMovil INT,
       Email TEXT,
       Password TEXT,
       FechaCreacion DATETIME,
       FechaModificacion DATETIME
    )

    create table RoleToUsuario (
        UsuarioId INT not null,
       RoleId INT not null,
       constraint FK670ADBED3E3915FC foreign key (RoleId) references Roles,
       constraint FK670ADBED108215A5 foreign key (UsuarioId) references Usuarios
    )
