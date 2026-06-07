CREATE TABLE [dbo].[Libro] (
    [Id_Libro]         INT            IDENTITY (1, 1) NOT NULL,
    [Titulo]           NVARCHAR (100) NULL,
    [Genero]           NVARCHAR (50)  NULL,
    [Autor]            NVARCHAR (200) NULL,
    [Editorial]        NVARCHAR (50)  NULL,
    [Cantidad_Paginas] NVARCHAR (5)   NULL,
    [Cantidad]         INT            NULL,
    PRIMARY KEY CLUSTERED ([Id_Libro] ASC)
);

