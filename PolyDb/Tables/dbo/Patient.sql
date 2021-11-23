CREATE TABLE [dbo].[Patient]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Nom] NVARCHAR(50) NOT NULL, 
    [Prenom] NVARCHAR(50) NOT NULL, 
    [DateNaissance] DATE NOT NULL,

)
