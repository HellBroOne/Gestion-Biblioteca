CREATE TABLE [dbo].[Lector] (
    [Id_Lector] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]    NVARCHAR (MAX) NOT NULL,
    [APaterno]  NVARCHAR (50)  NOT NULL,
    [AMaterno]  NVARCHAR (50)  NULL,
    [Telefono]  NVARCHAR (50)  NULL,
    [Correo]    NVARCHAR (MAX) NULL,
    [Domicilio] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id_Lector] ASC)
);

