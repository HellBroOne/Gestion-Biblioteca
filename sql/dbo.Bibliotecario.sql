CREATE TABLE [dbo].[Bibliotecario] (
    [Id_Bibliotecario] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]           NVARCHAR (MAX) NULL,
    [APaterno]         NVARCHAR (50)  NULL,
    [AMaterno]         NVARCHAR (50)  NULL,
    [Telefono]         NVARCHAR (10)  NULL,
    [Correo]           NVARCHAR (50)  NULL,
    [RFC]              NVARCHAR (13)  NULL,
    PRIMARY KEY CLUSTERED ([Id_Bibliotecario] ASC)
);

