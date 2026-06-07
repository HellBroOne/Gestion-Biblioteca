CREATE TABLE [dbo].[Prestamo] (
    [Id_Prestamo]      INT        IDENTITY (1, 1) NOT NULL,
    [Fecha_Inicial]    DATE       NOT NULL,
    [Duracion_Dias]    NCHAR (10) NULL,
    [Fecha_Final]      DATE       NULL,
    [Id_Lector]        INT        NULL,
    [Id_Libro]         INT        NULL,
    [Id_Bibliotecario] INT        NULL,
    PRIMARY KEY CLUSTERED ([Id_Prestamo] ASC),
    CONSTRAINT [FK_Lector_toTable] FOREIGN KEY ([Id_Lector]) REFERENCES [dbo].[Lector] ([Id_Lector]),
    CONSTRAINT [FK_Libro_toTable] FOREIGN KEY ([Id_Libro]) REFERENCES [dbo].[Libro] ([Id_Libro])
);

