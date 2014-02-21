-- =============================================
-- Script Template
-- =============================================
alter table Grupos 
        add constraint Grupos_Categoria_FK 
        foreign key (CategoriaId) 
        references Categorias