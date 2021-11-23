CREATE TABLE [dbo].[RendezVous]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [DateRendezVous] DATETIME NOT NULL, 
    [Medecin] NVARCHAR(50) NOT NULL, 
    [IdPatient] INT NOT NULL,
    
    CONSTRAINT [FK_RendezVous_ToPatient] FOREIGN KEY (IdPatient) REFERENCES [Patient]([Id]),


)
