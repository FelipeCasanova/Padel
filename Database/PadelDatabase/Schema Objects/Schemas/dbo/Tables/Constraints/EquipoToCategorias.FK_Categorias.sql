-- =============================================
-- Script Template
-- =============================================
alter table EquipoToCategorias 
        add constraint EquipoToCategorias_Categoria_FK 
        foreign key (CategoriaId) 
        references Categorias